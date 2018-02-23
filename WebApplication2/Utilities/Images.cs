using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Utilities
{
    /// <summary>
    /// Héctor de León
    /// Clase que realiza funciones de utilidad en imagenes
    /// 08/12/2013
    /// </summary>
    public class Images
    {
        public static byte[] imageToByteArray(System.Drawing.Image imageIn, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, format);
            return ms.ToArray();
        }


        /// <summary>
        /// metodo que realiza una conversion de imagen a matriz de bytes
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        /// <summary>
        /// metodo que convierte una matriz de bytes a imagen
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// metodo que redimensiona una imagen
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        /// <summary>
        /// metodo que redimensiona una imagen por un porcentaje
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image resizeImage(Image imgToResize, int porcentaje)
        {
            //atributos
            Size size = new Size();
            int widthNuevo = 0;
            int heigthNuevo = 0;

            //obtener el porcentaje para los nuevos valores
            widthNuevo = (imgToResize.Width * porcentaje) / 100;
            heigthNuevo = (imgToResize.Height * porcentaje) / 100;

            size.Width = widthNuevo;
            size.Height = heigthNuevo;

            return (Image)(new Bitmap(imgToResize, size));
        }

    }
}