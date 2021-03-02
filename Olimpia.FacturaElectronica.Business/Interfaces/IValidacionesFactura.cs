using Olimpia.Entidades;

namespace Olimpia.FacturaElectronica.Business.Interfaces
{
    public interface IValidacionesFactura
    {
        /// <summary>
        /// Validar los campos de la entidad Factura según requerimientos
        /// </summary>
        /// <param name="factura"><see cref="Factura"/></param>
        /// <returns>Mensaje de validación</returns>
        string ValidarCamposFactura(Factura factura);
    }
}
