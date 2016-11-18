using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace SeanShen.AEUtilities
{
    public class AutoInitializer
    {

        //静态私有化对象
        private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
        //初始化证书
        public static void InitialLicense()
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork });
        }
        //路径
        public static string Path
        {
            get
            {
                if (m_AOLicenseInitializer.GetCurrentRuntimeInfo() == null)//这里如果失败了应该记录日志
                    return null;
                return m_AOLicenseInitializer.GetCurrentRuntimeInfo().Path;
            }
        }
        //Product
        public static string Product
        {
            get
            {
                if (m_AOLicenseInitializer.GetCurrentRuntimeInfo() == null)
                    return null;
                ProductCode code = m_AOLicenseInitializer.GetCurrentRuntimeInfo().Product;
                string product = Enum.GetName(typeof(ProductCode), code);
                return product;
            }
        }
        //版本
        public static string Version
        {
            get
            {
                if (m_AOLicenseInitializer.GetCurrentRuntimeInfo() == null)
                    return null;
                return m_AOLicenseInitializer.GetCurrentRuntimeInfo().Version;
            }
        }

    }
}
