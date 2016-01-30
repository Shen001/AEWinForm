/* ============================================================
* Filename:		LoadDataHelper
* Created:		13/9/2015 11:22 AM
* MachineName:  
* Author:		Shenxin
* Description:  加载各种空间数据
* Modify:       加载数据
* ============================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;

using System.IO;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
namespace SeanShen.AEUtilities
{
    /***命名规则
     1-类私有变量以‘m_’前缀；
     2-方法等作用域变量命名以‘p’前缀；
     3-字符串以‘str’前缀
     4-参数根据类型名命名
     ***/
    public class LoadDataHelper
    {
        //2.加载Lyr
        //3.加载Shp
        //4.打开CAD数据 4.1-ArcEngine接口，4.2-netDXF.dll--使用开源插件，主要用于导出为dxf文件

        #region 加载CAD数据

        /***4.1-ArcEngine接口***/

        /// <summary>
        /// 通过指定的本地路径打开CAD所有图层数据
        /// </summary>
        /// <param name="mapControlDefault">默认的mapcontrol最新接口</param>
        /// <param name="strPath">cad文件的路径-包括文件名及后缀名</param>
        public static void GetAllCADbyPath(IMapControlDefault mapControlDefault, string strPath)
        {
            IWorkspaceFactory pWorkspaceFactory = new CadWorkspaceFactoryClass();
            string strPathWithoutFileName = Path.GetDirectoryName(strPath);
            string strFileName = Path.GetFileName(strPath);
            string strFileNameWithoutExtention = Path.GetFileNameWithoutExtension(strPath);

            IWorkspace pCADWorkspace = pWorkspaceFactory.OpenFromFile(strPathWithoutFileName, 0);//hWnd的参数是父窗口或应用程序的窗口
            if (!(pCADWorkspace.Type == esriWorkspaceType.esriFileSystemWorkspace))
                return;
            //1-pFeatureWorkspace.OpenFeatureDataset方法--推荐：可以判断注记层
            GetALLCADbyWorkspace(mapControlDefault, pCADWorkspace, strFileName);
            //2-通过IName获取到CadDrawingDataset在cadLayer
            //GetAllCADbyCADLayer(mapControlDefault,strPathWithoutFileName,strFileName);
            //3-通过ICadDrawingWorkspace获取到CadDrawingDataset
            //GetALLCADLayer(mapControlDefault, pCADWorkspace, strFileName);

            mapControlDefault.ActiveView.Refresh();
        }
        
        
        /// <summary>
        /// 直接添加所有层--通过IFeatureClassContainer
        /// </summary>
        /// <param name="mapControlDefault">地图控件</param>
        /// <param name="pFeatureWorkspace">要素工作空间</param>
        /// <param name="strFileName">文件名，包括后缀</param>
        public static void GetALLCADbyWorkspace(IMapControlDefault mapControlDefault,IWorkspace pCADWorkspace, string strFileName)
        {
            IFeatureWorkspace pFeatureWorkspace = pCADWorkspace as IFeatureWorkspace;
            IFeatureDataset pFeatureDataset = pFeatureWorkspace.OpenFeatureDataset(strFileName);
            IFeatureClassContainer pFeatureClassContainer = pFeatureDataset as IFeatureClassContainer;
            for (int i = 0; i < pFeatureClassContainer.ClassCount; i++)
            {
                IFeatureClass pFeatureClass = pFeatureClassContainer.get_Class(i);
                IFeatureLayer pFeatureLayer = null;
                if (pFeatureClass.FeatureType == esriFeatureType.esriFTAnnotation)
                {
                    pFeatureLayer = new CadAnnotationLayerClass();
                }
                else
                {
                    pFeatureLayer = new CadFeatureLayerClass();
                }
                if (pFeatureLayer != null)
                {
                    pFeatureLayer.Name = pFeatureClass.AliasName;
                    pFeatureLayer.FeatureClass = pFeatureClass;
                    mapControlDefault.AddLayer(pFeatureLayer as ILayer, 0);
                }
            }
        }
       
        
        /// <summary>
        /// 通过ICadDrawingDataset与ICadLayer获取cad
        /// </summary>
        /// <param name="mapControlDefault">地图控件</param>
        /// <param name="strPathWithoutFileName">文件所在文件夹路径</param>
        /// <param name="strFileName">文件名，包括后缀</param>
        public static void GetAllCADbyCADLayer(IMapControlDefault mapControlDefault, string strPathWithoutFileName, string strFileName)
        {
            ICadDrawingDataset cadDrawingDataset = GetCadDataset(strPathWithoutFileName, strFileName);
            if (cadDrawingDataset == null) 
                return;
            ICadLayer cadLayer = new CadLayerClass();
            cadLayer.CadDrawingDataset = cadDrawingDataset;
            cadLayer.Name = strFileName;
            ILayer mLayer = cadLayer as ILayer;
            mapControlDefault.AddLayer(mLayer, 0);
        }
        //通过IName获取到CadDrawingDataset
        private static ICadDrawingDataset GetCadDataset(string cadWorkspacePath, string cadFileName)
        {
            //Create a WorkspaceName object
            IWorkspaceName workspaceName = new WorkspaceNameClass();
            workspaceName.WorkspaceFactoryProgID = "esriDataSourcesFile.CadWorkspaceFactory";
            workspaceName.PathName = cadWorkspacePath;

            //Create a CadDrawingName object
            IDatasetName cadDatasetName = new CadDrawingNameClass();
            cadDatasetName.Name = cadFileName;
            cadDatasetName.WorkspaceName = workspaceName;

            //Open the CAD drawing
            IName name = (IName)cadDatasetName;
            return (ICadDrawingDataset)name.Open();
        }


        /// <summary>
        /// 通过IWorkspace直接获取到CadDrawingDataset--是cad数据
        /// </summary>
        /// <param name="mapControlDefault">地图控件</param>
        /// <param name="pWorkspace">工作空间</param>
        /// <param name="strFileName"></param>
        public static void GetALLCADLayer(IMapControlDefault mapControlDefault, IWorkspace pWorkspace, string strFileName)
        {
            ICadDrawingWorkspace pCadDrawingWorkspace = pWorkspace as ICadDrawingWorkspace;
            ICadDrawingDataset pCadDrawingDataset = pCadDrawingWorkspace.OpenCadDrawingDataset(strFileName);
            ICadLayer pCadLayer = new CadLayerClass();
            pCadLayer.CadDrawingDataset = pCadDrawingDataset;
            mapControlDefault.AddLayer(pCadLayer as ILayer, 0);
        }


        /// <summary>
        /// 打开指定的CAD图层，包括点point，线polyline，多点mutilpoint，多段mutilpatch与注记（如果是注记，需要建立CadAnnotationLayerClass实例对象），添加的是空间数据对象（添加到同一个图层组中，图层组名为文件名无后缀）
        /// </summary>
        /// <param name="mapControlDefault">地图控件</param>
        /// <param name="pWorkspace">工作空间</param>
        /// <param name="strFileName">文件名，没有会追</param>
        public static void GetALLCADLayerFromGeo(IMapControlDefault mapControlDefault, IWorkspace pWorkspace, string strFileName)
        {
            IFeatureWorkspace pFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            //组图层
            IGroupLayer pGroupLayer = new GroupLayerClass();
            pGroupLayer.Name = strFileName.Substring(0, strFileName.LastIndexOf("."));

            //打开指定的的cad图层--Point
            IFeatureClass pFeatureClass_Point = pFeatureWorkspace.OpenFeatureClass(strFileName + ":point");
            if (pFeatureClass_Point != null)
            {
                IFeatureLayer pFeatureLayer_Point = new CadFeatureLayerClass();
                pFeatureLayer_Point.FeatureClass = pFeatureClass_Point;
                pFeatureLayer_Point.Name = pFeatureClass_Point.AliasName;
                pGroupLayer.Add(pFeatureLayer_Point);
            }
            //Polyline  
            string strPlineName = System.String.Concat(strFileName, ":Polyline");
            IFeatureClass pFeatClass_Polyline = pFeatureWorkspace.OpenFeatureClass(strPlineName);
            if (pFeatClass_Polyline != null)
            {
                IFeatureLayer pFeatureLayer_Polyline = new CadFeatureLayerClass();
                pFeatureLayer_Polyline.FeatureClass = pFeatClass_Polyline;
                pFeatureLayer_Polyline.Name = pFeatClass_Polyline.AliasName;
                pGroupLayer.Add(pFeatureLayer_Polyline);
                pGroupLayer.Add(pFeatureLayer_Polyline);
            }

            mapControlDefault.AddLayer(pGroupLayer as ILayer, 0);
        }




        #endregion
    }
}
