using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.AEUtilities
{
    /*
        Time: 28/11/2016 13:18  周一
        Author: shenxin
        Description: 工作空间帮助类
        Modify:
     */
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.esriSystem;
    using ESRI.ArcGIS.DataSourcesGDB;
    public class WorkspaceHelper
    {
        /// <summary>
        /// 根据featurelayer得到workspace
        /// </summary>
        /// <param name="pFeatureLayer"></param>
        /// <returns></returns>
        public static IWorkspace GetCurrentWorkspace(IFeatureLayer pFeatureLayer)
        {
            if (pFeatureLayer == null)
                return null;
            IDataset pDataset = (IDataset)pFeatureLayer.FeatureClass;

            return pDataset.Workspace;
        }
        /// <summary>
        /// 根据map获得feaurelayer的workspace
        /// </summary>
        /// <param name="pMap"></param>
        /// <returns></returns>
        public static IWorkspace GetCurrentWorkspace(IMap pMap)
        {
            if (pMap == null) return null;

            IUID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11D0-98BE-00805F7CED21}";//uid代表featurelayer

            IEnumLayer enumLayer = pMap.get_Layers(((UID)(uid)), true);
            enumLayer.Reset();
            ESRI.ArcGIS.Carto.ILayer layer = enumLayer.Next();
            if (!(layer == null))
            {
                IFeatureLayer featureLayer = (IFeatureLayer)layer;
                return GetCurrentWorkspace(featureLayer);
            }
            return null;

        }


        /// 打开SDE空间数据库工作空间
        /// </summary>
        /// <param name="user">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="server">服务器</param>
        /// <param name="instance">实例名</param>
        /// <param name="database">数据库名称</param>
        /// <param name="version">版本</param>
        /// <returns></returns>
        public static IWorkspace OpenSdeWorkspace(string user, string password, string server, string instance, string database, string version)
        {
            try
            {
                IPropertySet propertySet = new PropertySetClass();
                propertySet.SetProperty("SERVER", server);
                propertySet.SetProperty("INSTANCE", instance);
                propertySet.SetProperty("DATABASE", database);
                propertySet.SetProperty("USER", user);
                propertySet.SetProperty("PASSWORD", password);
                propertySet.SetProperty("VERSION", version);
                IWorkspaceFactory2 workspaceFactory = new SdeWorkspaceFactoryClass();

                IWorkspace workspace = workspaceFactory.Open(propertySet, 0);
                return workspace;
            }
            catch
            {

                return null;
            }
        }
    }
}
