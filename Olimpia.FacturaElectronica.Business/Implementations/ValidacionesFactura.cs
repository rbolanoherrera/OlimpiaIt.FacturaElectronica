using NLog;
using Olimpia.Entidades;
using Olimpia.FacturaElectronica.Business.Interfaces;
using Olimpia.Utilidades;

namespace Olimpia.FacturaElectronica.Business.Implementations
{
    public class ValidacionesFactura : IValidacionesFactura
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Validar los campos de la entidad Factura según requerimientos
        /// </summary>
        /// <param name="factura"><see cref="Factura"/></param>
        /// <returns>Mensaje de validación</returns>
        public string ValidarCamposFactura(Factura factura)
        {
            string validaciones = string.Empty;

            //El Id es un valor entero positivo.
            if (factura.Id <= 0)
                validaciones += "El Id debe ser un valor entero positivo;";

            //El NIT debe contener sólo valores numéricos
            if (!RegularExpressionsValidations.SoloNumeros(factura.Nit.ToString()))
                validaciones += "El Nit debe contener solo valores númericos, sin puntos ni comas;";

            //El Valor Total debe ser positivo.
            if (!RegularExpressionsValidations.ValoresDecimales(factura.ValorTotal.ToString()))
                validaciones += "Formato incorrecto para el campo ValorTotal;";

            if (factura.ValorTotal <= 0)
                validaciones += "El ValorTotal debe ser positivo;";

            //El Valor del Iva es un valor entre 0(%) y 100(%)
            if (!RegularExpressionsValidations.ValoresDecimales(factura.PorcentIva.ToString()))
                validaciones += "Formato incorrecto para el campo Porcentaje de Iva;";

            if (factura.PorcentIva < 0 || factura.PorcentIva > 100)
                validaciones += "El Porcentaje del Iva debe ser un valor entre 0 y 100";

            if (!string.IsNullOrEmpty(validaciones))
                log.Error($"Errors de Validaciones de la Factura: {validaciones}");

            return validaciones;
        }
    }
}
