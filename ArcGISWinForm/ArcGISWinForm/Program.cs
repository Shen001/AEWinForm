using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;

using SeanShen.AEUtilities;
namespace ArcGISWinForm
{
    static class Program
    {
        private static InitialLicenseHelper aoInitial = new InitialLicenseHelper();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            aoInitial.InitialLicensesCode(new esriLicenseProductCode[]{esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB},new esriLicenseExtensionCode[]{esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork,esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability});
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            aoInitial.ShutDownApplication();
        }
    }
}
