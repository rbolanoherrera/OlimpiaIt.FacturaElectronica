using System;
using System.IO;

namespace Olimpia.Multiplos3
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            string line;
            double suma = 0;
            double numero;
            string esMultiplo = string.Empty;
            string pathEntrada = @"D:\WORKs\OlimpiaIT\Olimpia.PruebaTecnica\Olimpia.Multiplos3\archivoEntrada-01.txt";
            string pathSalida = @"D:\WORKs\OlimpiaIT\Olimpia.PruebaTecnica\Olimpia.Multiplos3\Salida_Prueba_Olimpia.txt";
            System.IO.StreamReader file=null;

            //Si no existe el archivo de salida se crea y si existe se blanquea
            try
            {
                File.Create(pathSalida).Dispose();
            }
            catch(Exception)
            {
                Console.WriteLine("Se genero un error al tratar de generar el archivo de salida. Verifique la ruta y el nombre del archivo antes de correr el proceso");
                FinProceso();
                return;
            }

            //leemos el archivo de entrada
            try
            {
                file = new System.IO.StreamReader(pathEntrada);

                Console.WriteLine("<<<----Inicio del Proceso--->>>\n\n");
                Console.WriteLine("<<<---------------- Lineas del archivo de Entrada ------------------>>>");

                while ((line = file.ReadLine()) != null)
                {
                    suma = 0;
                    for (int i = 0; i < line.Length; i++)
                    {
                        numero = Convert.ToDouble(line.Substring(i, 1));
                        suma = suma + numero;
                    }

                    //verificamos si la linea leida es multiplo de 3 (se suma de cada uno de sus digitos y si el residuo al dividir por 3 es cero es multiplo de 3)
                    if (suma % 3 == 0)
                        esMultiplo = "SI";
                    else
                        esMultiplo = "NO";

                    File.AppendAllText(pathSalida, esMultiplo + Environment.NewLine);

                    System.Console.WriteLine(line);
                    counter++;
                }

            }catch(Exception ex)
            {
                Console.WriteLine("Se genero un error al tratar de leer el archivo de Entrada. Verifique la ruta y el nombre del archivo antes de correr el proceso");
                Console.WriteLine("\n\n{0}", ex.Message);
                FinProceso();
                return;
            }
            finally
            {
                if(file != null)
                    file.Close();
            }

            Console.WriteLine("<<<---------------- Fin Lineas del archivo de Entrada ------------------>>>");
            Console.WriteLine("\n\n<<<----- Fin del Proceso---->>>");

            System.Console.WriteLine("\nNumero de Lineas Cargadas: {0}", counter);
            System.Console.WriteLine("\nVerifique la respuesta en el archivo:");
            System.Console.WriteLine(pathSalida);

            FinProceso();
            
        }

        /// <summary>
        /// Función que imprime o indica que todo el proceso a finalizado
        /// </summary>
        private static void FinProceso()
        {
            System.Console.WriteLine("\n\nPresione cualquier tecla para salir...");
            System.Console.ReadKey();
        }
    }
}
