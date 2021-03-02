using NLog;
using Olimpia.Entidades;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Olimpia.Utilidades
{
    /// <summary>
    /// Clase que hace validaciones sobre un Factura
    /// </summary>
    public static class JsonValidations
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Extension que valida si un Json o Entidad tiene campos obligatorios
        /// </summary>
        /// <typeparam name="TSchema">schema</typeparam>
        /// <param name="value">json string</param>
        /// <returns>is valid?</returns>
        public static string IsValidJson<TSchema>(this string value)
            where TSchema : new()
        {
            string returnData = string.Empty;
            try
            {
                //this is a .net object look for it in msdn
                JavaScriptSerializer ser = new JavaScriptSerializer();
                //first serialize the string to object.
                var obj = ser.Deserialize<TSchema>(value);


                //get all properties of schema object
                var properties = typeof(TSchema).GetProperties();
                //iterate on all properties and test.
                foreach (PropertyInfo info in properties)
                {
                    // i went on if null value then json string isnt schema complient but you can do what ever test you like her.
                    var valueOfProp = obj.GetType().GetProperty(info.Name).GetValue(obj, null);

                    var required = info.GetCustomAttributes(typeof(RequiredAttribute), false);

                    if (required.Length > 0)
                    {
                        if (valueOfProp == null || string.IsNullOrEmpty(valueOfProp.ToString()))
                        {
                            returnData = returnData + info.Name + ";";
                        }
                    }
                }

                return returnData;
            }
            catch (Exception ex)
            {
                log.Error(ex, "IsValidJson");
                return returnData;
            }

        }

        /// <summary>
        /// Valida si un Json o Entidad tiene datos imcompletos
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static bool DataImcompleta(string fields)
        {
            return fields != null && !String.IsNullOrEmpty(fields);
        }

    }
}
