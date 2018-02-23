using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace WebApplication2.Utilities

{
    public class Archivo 
    {
        /// <summary>
        /// obtiene el contenido de un archivo en string
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        public static string GetStringOfFile(string pathFile)
        {
            try
            {
                string contenido = File.ReadAllText(pathFile);

                return contenido;
            }
            catch
            {
                return "";
            }
        }
    }
}
