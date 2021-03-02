using NLog;
using Olimpia.Entidades;
using Olimpia.Entidades.Common;
using Olimpia.FacturaElectronica.Business.Interfaces;
using Olimpia.Utilidades;
using System.Web.Script.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Olimpia.FacturaElectronica.Business.Implementations
{
    public class FacturaBusiness : IFacturaBusiness
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IValidacionesFactura validacionesFactura;

        public FacturaBusiness(IValidacionesFactura validacionesFactura)
        {
            this.validacionesFactura = validacionesFactura;
        }

        /// <summary>
        /// Metodo que valida los campos de una sola factura
        /// </summary>
        /// <param name="factura"><see cref="Olimpia.Entidades.Factura"/>Objeto con los datos de la factura</param>
        /// <returns><see cref="Olimpia.Entidades.Common.Result{Factura}"/></returns>
        public Result<decimal> ValidacionCampos(Factura factura)
        {
            Result<decimal> result = new Result<decimal>();

            if (factura == null)
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.Data = 0;
                result.Message = $"No se ha suministrado una Factura.";

                return result;
            }

            string json = new JavaScriptSerializer().Serialize(factura);

            string requiredFields = JsonValidations.IsValidJson<Factura>(json);

            if (JsonValidations.DataImcompleta(requiredFields))
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.Data = 0;
                result.Message = $"Los siguientes campos son requeridos [ {requiredFields} ], por favor digitelos para poder continuar.";

                return result;
            }

            string validaciones = validacionesFactura.ValidarCamposFactura(factura);

            if (!string.IsNullOrEmpty(validaciones))
            {
                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.Data = 0;
                result.Message = $"La Factura {factura.Id}, no cumplio las siguientes validaciones: {validaciones}";

                return result;
            }

            result.StatusCode = System.Net.HttpStatusCode.OK;
            result.Data = factura.ValorTotal;
            result.Message = $"La Factura {factura.Id}, paso todas las validaciones exitosamente!";

            return result;
        }

        /// <summary>
        /// Calcular el valor del Iva de acuerdo a el valor total. Retorna el valor del IVA no el total del producto más Iva
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        public Result<decimal> CalcularIVA(Factura factura)
        {
            Result<decimal> result = new Result<decimal>();

            decimal iva = factura.ValorTotal * (factura.PorcentIva / 100);
            result.Data = iva;

            return result;
        }

        public List<Result<decimal>> RecibirFacturas(Factura[] facturas)
        {
            List<Result<decimal>> result = new List<Result<decimal>>();
            bool repetidas = false;

            var q = from x in facturas.Select(f => f.Id).ToList()
                group x by x into g
                let count = g.Count()
                orderby count descending
                select new { Value = g.Key, Count = count };

            foreach (var x in q)
            {
                System.Diagnostics.Debug.WriteLine($"Value {x.Value} Count: {x.Count}");

                if (x.Count > 1)
                    repetidas = true;
            }

            if (repetidas)
            {
                result.Add(new Result<decimal>() { 
                    StatusCode = System.Net.HttpStatusCode.Ambiguous, 
                    Data = -1 ,
                    Message = "Existen número de Facturas repetidas, No se puede procesar el cargue!"
                });

                return result;
            }

            foreach (Factura factura in facturas)
                result.Add(ValidacionCampos(factura));

            return result;
        }


    }
}
