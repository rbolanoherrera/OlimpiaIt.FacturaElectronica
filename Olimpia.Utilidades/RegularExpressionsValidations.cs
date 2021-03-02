using System.Text.RegularExpressions;

namespace Olimpia.Utilidades
{
    public static class RegularExpressionsValidations
    {
        /// <summary>
        /// Valida si una cadena contine solo numeros
        /// </summary>
        /// <param name="stringNumber"></param>
        /// <returns></returns>
        public static bool SoloNumeros(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber))
                return false;

            Regex rgx = new Regex(@"^[0-9]+$");
            bool s = rgx.IsMatch(stringNumber);

            return s;
        }

        /// <summary>
        /// Validar que valor número tenga un puto de separador de decimales
        /// </summary>
        /// <param name="stringNumber">Ej: 2.3</param>
        /// <returns></returns>
        public static bool ValoresDecimales(string stringNumber)
        {
            if (string.IsNullOrEmpty(stringNumber))
                return false;

            Regex rgx = new Regex(@"^-?\d+(?:,\d+)?$");
            bool s = rgx.IsMatch(stringNumber);

            return s;
        }
    }
}
