using Olimpia.Entidades;
using Olimpia.Entidades.Common;
using Olimpia.FacturaElectronica.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Olimpia.FacturaElectronica.Controllers
{
    /// <summary>
    /// Controlador de Factura
    /// </summary>
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        private readonly IFacturaBusiness facturaBusiness;

        public FacturaController(IFacturaBusiness facturaBusiness)
        {
            this.facturaBusiness = facturaBusiness;
        }

        /// <summary>
        /// Validaciones sobre los campos de la Factura
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ValidarCampos")]
        public IHttpActionResult ValidarFactura([FromBody] Factura model)
        {
            var re = facturaBusiness.ValidacionCampos(model);

            return Content(re.StatusCode, re);
        }

        /// <summary>
        /// Calcular el valor del Iva de acuerdo a el valor total. Retorna el valor del IVA no el total del producto más Iva
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CalcularIVA")]
        public IHttpActionResult CalcularIva([FromBody] Factura model)
        {
            var re = facturaBusiness.CalcularIVA(model);

            return Content(re.StatusCode, re);
        }

        /// <summary>
        /// Recibe el listado de las Factura y realiza las validaciones respectivas. 
        /// Si las validaciones son exitosas en el campo Data se retona la suma de los Valores Totales
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RecibirFacturas")]
        public IHttpActionResult RecibirFacturas([FromBody] Factura[] model)
        {
            var re = facturaBusiness.RecibirFacturas(model);

            var repetidas = re.Where(f => f.StatusCode == System.Net.HttpStatusCode.Ambiguous).FirstOrDefault();

            if(repetidas != null)
            {
                Result<decimal> result = new Result<decimal>();

                result.StatusCode = System.Net.HttpStatusCode.BadRequest;
                result.Message = re.ElementAt(0).Message;
                result.Data = re.ElementAt(0).Data;

                return Content(result.StatusCode, result);
            }

            int totalOk = re.Where(f => f.StatusCode == System.Net.HttpStatusCode.OK).Count();
            
            if(totalOk == model.Length)
            {
                Result<decimal> result = new Result<decimal>();

                result.StatusCode = System.Net.HttpStatusCode.OK;
                result.Message = "Facturas cargadas exitosamente. La suma de los ValoresTotales se encuentra en el campo Data";
                result.Data = re.Sum(f => f.Data);

                return Content(result.StatusCode, result);
            }
            else
            {
                Result<List<string>> result = new Result<List<string>>();

                result.StatusCode = System.Net.HttpStatusCode.Accepted;
                result.Message = "Algunas de las Facturas cargadas no paso las validaciones.";
                result.Data = re.Select(f => f.Message).ToList();

                return Content(result.StatusCode, result);
            }

        }

    }
}
