using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;

namespace SeanShen.AEUtilities
{
/*
Time: 21/11/2016 10:49 PM 周一
Author: shenxin
Description: 自动初始化证书
Modify:
*/
			
    public class AutoInitializer
    {

        //静态私有化对象
        private static LicenseInitializer m_AOLicenseInitializer = new LicenseInitializer();
       /// <summary>
       /// 初始化证书
       /// </summary>
        public static void InitialLicense()
        {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication(new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeEngineGeoDB },
            new esriLicenseExtensionCode[] { esriLicenseExtensionCode.esriLicenseExtensionCodeNetwork, esriLicenseExtensionCode.esriLicenseExtensionCodeDataInteroperability });
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public static void ShutdownApplication()
        {
            m_AOLicenseInitializer.ShutdownApplication();
        }
        /// <summary>
        /// AO的路径
        /// </summary>
        public static string Path
        {
            get
            {
                if (m_AOLicenseInitializer.GetCurrentRuntimeInfo() == null)//这里如果失败了应该记录日志
                    return null;
                return m_AOLicenseInitializer.GetCurrentRuntimeInfo().Path;
            }
        }
        /// <summary>
        /// 产品名
        /// </summary>
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
        /// <summary>
        /// 版本号
        /// </summary>
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
