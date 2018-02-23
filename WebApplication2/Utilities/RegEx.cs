using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication2.Utilities
{
    public class RegEX
    {
        public static string Limpiar(string Cadena)
        {


            if (Cadena == null) return null;

            string Cadena2 = Cadena.Replace("\r", " ").Replace("\n", " ");
            Regex regex = new Regex("\\s{2,}");
            string result = regex.Replace(Cadena2.Trim(), " ");
            return result;
        }

        public static bool isAlphabet(String Entrada, int Min, int Max)
        {
            Regex rx = new Regex("^[a-zA-Z]{" + (Min.ToString()) + "," + (Max.ToString()) + "}$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La cadena " + Entrada + " no es válida.");
                return false;
            }
        }

        public static bool isNumber(String Entrada, int Min, int Max)
        {
            Regex rx = new Regex("^[0-9]{" + (Min.ToString()) + "," + (Max.ToString()) + "}$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El número " + Entrada + " no es válido.");
                return false;
            }
        }
        /// <summary>
        /// solo numeros
        /// </summary>
        /// <param name="Entrada"></param>
        /// <returns></returns>
        public static bool isNumber(String Entrada)
        {
            Regex rx = new Regex("^[0-9]+$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El número " + Entrada + " no es válido.");
                return false;
            }
        }


        public static bool isCustom(string Entrada, string Expresion = "")
        {
            Regex rx = new Regex(Expresion);
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La expresión " + Entrada + " revisada bajo [" + Expresion + "] no es válido.");
                return false;
            }
        }

        public static bool isDateTime(string Entrada)
        {
            Regex rx = new Regex(@"^(([0-9]{4})-(1[0-2]|0[0-9])-(([0-2][0-9])|(3[0-1]))T((2[0-3]|[0-1][0-9])):([0-5][0-9]):([0-5][0-9]))?$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                //System.Console.WriteLine("La fecha/hora " + Entrada + " no es válida.");
                return false;
            }
        }
        public static bool isDate(string Entrada)
        {
            Regex rx = new Regex(@"^(([0-9]{4})-(1[0-2]|0[0-9])-(([0-2][0-9])|(3[0-1])))$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La fecha " + Entrada + " no es válida.");
                return false;
            }
        }

        public static bool isDecimal(string Entrada, int Posiciones)
        {
            Regex rx = new Regex(@"^[0-9]+.([0-9]{" + Posiciones.ToString() + "})?$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El numero " + Entrada + " no es un decimal de " + Posiciones.ToString() + " válido.");
                return false;
            }
        }

        public static bool isRFC(string Entrada)
        {
            Regex rx = new Regex(@"^[A-Za-z]{3,4}[0-9]{6}[A-Za-z0-9]{3}$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El RFC " + Entrada + " no es un RFC válido.");
                return false;
            }
        }

        public static bool isRFC2(string Entrada)
        {
            Regex rx = new Regex(@"^[A-Za-zÑñ&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Za-z0-9]{2}[0-9Aa]$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El RFC " + Entrada + " no es un RFC válido.");
                return false;
            }
        }
        public static bool isDecimal(string Entrada)
        {
            Regex rx = new Regex(@"^(-)?[0-9]+(.[0-9]+)?$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El numero " + Entrada + " no es un decimal válido.");
                return false;
            }
        }

        public static bool isUUID(string Entrada)
        {
            Regex rx = new Regex(@"^[a-f0-9A-F]{8}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{12}$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El código UUID {" + Entrada + "} no es un UUID válido.");
                return false;
            }
        }
        public static bool isEmail(string Entrada)
        {
            // Regex rx = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$"); //linea vieja
            Regex rx = new Regex(@"^[_a-z0-9-]+(\.[\-_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$");
            // Check each test string against the regular expression.
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El Email {" + Entrada + "} no es válido.");
                return false;
            }
        }

        public static bool isCURP(string Entrada)
        {
            Regex rx = new Regex(@"^[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La Curp " + Entrada + " no es una CURP válida.");
                return false;
            }
        }

        /// <summary>
        /// Evalua si es valido el dato como CLABE Interbancaria
        /// </summary>
        /// <param name="Entrada"></param>
        /// <returns></returns>
        public static bool isCLABE(string Entrada)
        {
            Regex rx = new Regex(@"^[0-9]{18}$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La CLABE " + Entrada + " no es una CLABE válida.");
                return false;
            }
        }

        /// <summary>
        /// Evalua si es un registro patronal valido
        /// </summary>
        /// <param name="Entrada"></param>
        /// <returns></returns>
        public static bool isRegistroPatronal(string Entrada)
        {
            Regex rx = new Regex(@"^([A-Z]|[a-z]|[0-9]|Ñ|ñ|!|&quot;|%|&amp;|&apos;|´|-|:|;|&gt;|=|&lt;|@|_|,|\{|\}|`|~|á|é|í|ó|ú|Á|É|Í|Ó|Ú|ü|Ü){1,20}$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El registro patronal " + Entrada + " no es válido.");
                return false;
            }
        }

        /// <summary>
        /// Evalua si es un registro patronal valido
        /// </summary>
        /// <param name="Entrada"></param>
        /// <returns></returns>
        public static bool isNumeroSeguridadSocial(string Entrada)
        {
            Regex rx = new Regex(@"^[0-9]{1,15}$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("El numero de seguridad social " + Entrada + " no es una válido.");
                return false;
            }
        }

        /// <summary>
        /// Evalua si es IP 4.0 valida
        /// </summary>
        /// <param name="Entrada"></param>
        /// <returns></returns>
        public static bool isIP(string Entrada)
        {
            Regex rx = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            if (Entrada != null && rx.IsMatch(Entrada))
                return true;
            else
            {
                System.Console.WriteLine("La ip " + Entrada + " no es una válida.");
                return false;
            }
        }

    }
}