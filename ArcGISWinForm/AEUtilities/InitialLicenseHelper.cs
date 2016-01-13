/* ============================================================
* Filename:		InitialLicenseHelper
* Created:		1/9/2015 11:22 AM
* MachineName:  
* Author:		Shenxin
* Description: 
* Modify:       证书初始化
* ============================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS;
using ESRI.ArcGIS.esriSystem;
namespace SeanShen.AEUtilities
{
    public class InitialLicenseHelper
    {
        public event EventHandler ResolveBindingEvent;//如果绑定runtime失败时激活
        public InitialLicenseHelper()
        {
            ResolveBindingEvent += new EventHandler(BindingArcGISRuntime);
        }
        private void BindingArcGISRuntime(object sender, EventArgs e)
        {
            if (!RuntimeManager.Bind(ProductCode.Engine))
            {
                if (!RuntimeManager.Bind(ProductCode.Desktop))
                {
                    System.Windows.Forms.MessageBox.Show("本机没有安装ArcGISRuntime，请安装后启动本程序！", "警告");
                    System.Environment.Exit(0);
                }
            }
        }

        public bool InitialLicensesCode(esriLicenseProductCode[] esriProductCodes,params esriLicenseExtensionCode[] esriExtensionCodes)
        {
            if(productCodeList==null)
            productCodeList = new List<int>();//将产品码添加到list集合中，Cache product codes by enum int so can be sorted without custom sorter
            foreach (esriLicenseProductCode item in esriProductCodes)
            {
                int codeNum = Convert.ToInt32(item);
                if (!productCodeList.Contains(codeNum))
                {
                    productCodeList.Add(codeNum);
                }
            }
            AddExtensions(esriExtensionCodes);
            //验证runtime绑定
            if (RuntimeManager.ActiveRuntime == null)
            {
                EventHandler eventHandler = ResolveBindingEvent;
                if (eventHandler != null)
                {
                    eventHandler(this,new EventArgs());
                }
            }
            return InitializeProduct();
        }
        /// <summary>
        /// 初始化产品，只初始化第一个
        /// </summary>
        /// <returns></returns>
        public bool InitializeProduct()
        {
            if (RuntimeManager.ActiveRuntime == null)
                return false;
            if (productCodeList == null || extensionCodeList.Count == 0)
                return false;
            bool productInitialized = false;
            productCodeList.Sort();//对于产品集合进行排序
            if (!InitializeLowerProductFirst)
            {
                productCodeList.Reverse();//反转整个集合
            }
            aoInitialize = new AoInitializeClass();

            esriLicenseProductCode currentProductCode = new esriLicenseProductCode();
            foreach (int item in productCodeList)
            {
                esriLicenseProductCode proCode = (esriLicenseProductCode)Enum.ToObject(typeof(esriLicenseProductCode), item);
                esriLicenseStatus status = aoInitialize.IsProductCodeAvailable(proCode);
                if (status == esriLicenseStatus.esriLicenseAvailable)
                {
                    status = aoInitialize.Initialize(proCode);
                    if (status == esriLicenseStatus.esriLicenseAlreadyInitialized || status == esriLicenseStatus.esriLicenseCheckedOut)
                    { 
                        productInitialized = true;
                        currentProductCode = aoInitialize.InitializedProduct();
                    }
                }
                dic_ProductStatus.Add(proCode, status);

                if (productInitialized)
                    break;//只初始化第一个成功的产品！
            }
            this.hasInitialProduct = productInitialized;
            this.productCodeList.Clear();

            if (!productInitialized)//如果所有的产品都不成功
                return false;

            return CheckOutLicense(currentProductCode);//验证扩展
        }
        public bool AddExtensions(params esriLicenseExtensionCode[] esriExtensionCodes)
        {
            if (extensionCodeList == null)
                extensionCodeList = new List<esriLicenseExtensionCode>();
            foreach (esriLicenseExtensionCode item in esriExtensionCodes)
            {
                if (!extensionCodeList.Contains(item))
                {
                    extensionCodeList.Add(item);//将扩展编码添加到集合中去
                }
            }

            if (hasInitialProduct)//如果已经初始化了产品，那么验证产品
                return CheckOutLicense(this.InitializedProduct);

            return false;
        }
        //根据产品编码检查扩展编码
        private bool CheckOutLicense(esriLicenseProductCode currentProduct)
        {
            bool success = true;
            if (extensionCodeList.Count > 0 && currentProduct != 0)
            {
                foreach (esriLicenseExtensionCode item in extensionCodeList)
                {
                    esriLicenseStatus licenseStatus = aoInitialize.IsExtensionCodeAvailable(currentProduct, item);
                    if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)
                    {
                        if (!aoInitialize.IsExtensionCheckedOut(item))
                        {
                            licenseStatus = aoInitialize.CheckOutExtension(item);
                        }
                    }
                    success = (success && licenseStatus == esriLicenseStatus.esriLicenseCheckedOut);//如果success是false，那么一直都是false！
                    if (dic_ExtensionStatus.ContainsKey(item))//将扩展与扩展状态添加到字典中去
                    {
                        dic_ExtensionStatus[item] = licenseStatus;
                    }
                    else
                    {
                        dic_ExtensionStatus.Add(item, licenseStatus);
                    }
                }
                extensionCodeList.Clear();//清空扩展列表
            }
            return success;
        }
        //关闭
        public void ShutDownApplication()
        {
            if (this.hasShutDown)
                return;
            if (this.aoInitialize != null)
            {
                foreach (KeyValuePair<esriLicenseExtensionCode,esriLicenseStatus> item in this.dic_ExtensionStatus)
                {
                    if (item.Value == esriLicenseStatus.esriLicenseCheckedOut)
                        aoInitialize.CheckInExtension(item.Key);//迁回扩展编码
                }
                aoInitialize.Shutdown();
            }
            this.productCodeList = null;
            this.extensionCodeList = null;
            this.dic_ExtensionStatus = null;
            this.dic_ProductStatus = null;

            this.hasShutDown = true;
        }
        #region 属性
        //已经初始化的产品--只读
        public esriLicenseProductCode InitializedProduct
        {
            get
            {
                if (aoInitialize != null)
                {
                    try
                    {
                        aoInitialize.InitializedProduct();
                    }
                    catch
                    {

                        return 0;
                    }
                }
                return 0;
            }
        }
        //初始化product的顺序
        public bool InitializeLowerProductFirst
        {
            get
            {
                return productCheckOrdering;
            }
            set
            {
                productCheckOrdering = value;
            }
        } 
        #endregion




        private IAoInitialize aoInitialize;//初始化对象
        Dictionary<esriLicenseExtensionCode, esriLicenseStatus> dic_ExtensionStatus = new Dictionary<esriLicenseExtensionCode, esriLicenseStatus>();//扩展字典
        Dictionary<esriLicenseProductCode, esriLicenseStatus> dic_ProductStatus = new Dictionary<esriLicenseProductCode, esriLicenseStatus>(); //产品字典
       private List<int> productCodeList;//保存esriLicenseProductCode
       private List<esriLicenseExtensionCode> extensionCodeList;//保存扩展编码
       private bool hasInitialProduct = false;//指示是否已经初始化了产品
       private bool productCheckOrdering = true;//true-defalut (low-high)
       private bool hasShutDown = false;//是否关闭初始化对象
    }
}
