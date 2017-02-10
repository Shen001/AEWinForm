using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Display;
namespace SeanShen.AEUtilities
{
    public class RGBColorHelper
    {
        /// <summary>
        /// 根据颜色值生成IRgbColor对象
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public static IRgbColor GetRGB(int red, int green, int blue)
        {
            IRgbColor pColor = new RgbColorClass();
            pColor.Blue = blue;
            pColor.Green = green;
            pColor.Red = red;
            return pColor;
        }

        #region 使用ADF将rgbcolor与color进行转换
        /// <summary>
        /// 讲IRgbColor转换为为System.Drawing.Color
        /// </summary>
        /// <param name="rgbColor"></param>
        /// <returns></returns>
        public static System.Drawing.Color FromRgbColor(IRgbColor rgbColor)
        {
            System.Drawing.Color color = ESRI.ArcGIS.ADF.Connection.Local.Converter.FromRGBColor(rgbColor);

            return color;
        }
        /// <summary>
        /// 讲System.Drawing.Color转换为IRgbColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static IRgbColor ToRgbColor(System.Drawing.Color color)
        {
            IRgbColor rgbcolor = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToRGBColor(color);

            return rgbcolor;
        }
        /// <summary>
        /// 将System.Drawing.Font转换为stdole.IFontDisp
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static stdole.IFontDisp ToStdFont(System.Drawing.Font font)
        {
            stdole.IFontDisp fontdisp = ESRI.ArcGIS.ADF.Connection.Local.Converter.ToStdFont(font);

            return fontdisp;
        }
        /// <summary>
        /// 将System.Drawing.Font转换为stdole.IFontDisp
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static stdole.IFontDisp GetIFontDispFromFont(System.Drawing.Font font)
        {
            stdole.IFontDisp fontdisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(font) as stdole.IFontDisp;

            return fontdisp;
        }
        /// <summary>
        /// 将System.Drawing.Bitmap转换为stdole.IPictureDisp
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static stdole.IPictureDisp GetIPictureDispFromBitmap(System.Drawing.Bitmap bitmap)
        {
            stdole.IPictureDisp pictureDisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIPictureDispFromBitmap(bitmap) as stdole.IPictureDisp;

            return pictureDisp;
        }

        /// <summary>
        /// 将System.DrawingIcon转换为stdole.IPictureDisp
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static stdole.IPictureDisp GetIPictureDispFromIcon(System.Drawing.Icon icon)
        {
            stdole.IPictureDisp pictureDisp = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIPictureDispFromIcon(icon) as stdole.IPictureDisp;

            return pictureDisp;
        }

        /// <summary>
        /// 根据stdole.IPictureDisp转换为Image
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public static System.Drawing.Image TransferImageFromIPictureDisp(stdole.IPictureDisp picture)
        {
            System.Drawing.Image image = System.Drawing.Image.FromHbitmap(new IntPtr(picture.Handle));
            return image;
        }
        #endregion
             
    }
}
