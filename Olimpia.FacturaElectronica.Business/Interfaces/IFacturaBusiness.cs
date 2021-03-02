
using Olimpia.Entidades;
using Olimpia.Entidades.Common;
using System.Collections.Generic;

namespace Olimpia.FacturaElectronica.Business.Interfaces
{
    public interface IFacturaBusiness
    {
        /// <summary>
        /// Metodo que valida los campos de una sola factura
        /// </summary>
        /// <param name="factura"><see cref="Olimpia.Entidades.Factura"/>Objeto con los datos de la factura</param>
        /// <returns><see cref="Olimpia.Entidades.Common.Result{Factura}"/></returns>
        Result<decimal> ValidacionCampos(Factura factura);

        /// <summary>
        /// Calcular el valor del Iva de acuerdo a el valor total. Retorna el valor del IVA no el total del producto más Iva
        /// </summary>
        /// <param name="factura"></param>
        /// <returns></returns>
        Result<decimal> CalcularIVA(Factura factura);

        List<Result<decimal>> RecibirFacturas(Factura[] facturas);
    }
}
