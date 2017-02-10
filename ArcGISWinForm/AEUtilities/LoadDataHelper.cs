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
//using ESRI.ArcGIS.esriSystem;
using System.Data;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
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
            ESRI.ArcGIS.esriSystem.IName name = (ESRI.ArcGIS.esriSystem.IName)cadDatasetName;
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

        #region OleDb加载Excel
        //通过OleDb加载excel数据
        public static DataSet LoadDataFromExcel(string filePath)
        {
            string connStr = "";
            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            if (fileType == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
            string sql_F = "Select * FROM [{0}]";

            OleDbConnection conn = null;
            OleDbDataAdapter da = null;
            DataTable dtSheetName = null;

            DataSet ds = new DataSet();
            try
            {
                // 初始化连接，并打开
                conn = new OleDbConnection(connStr);
                conn.Open();

                // 获取数据源的表定义元数据                        
                string SheetName = "";
                dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                // 初始化适配器
                da = new OleDbDataAdapter();
                for (int i = 0; i < dtSheetName.Rows.Count; i++)
                {
                    SheetName = (string)dtSheetName.Rows[i]["TABLE_NAME"];

                    if (SheetName.Contains("$") && !SheetName.Replace("'", "").EndsWith("$"))
                    {
                        continue;
                    }

                    da.SelectCommand = new OleDbCommand(String.Format(sql_F, SheetName), conn);
                    DataSet dsItem = new DataSet();
                    da.Fill(dsItem, SheetName);

                    ds.Tables.Add(dsItem.Tables[0].Copy());
                }
            }
            catch (Exception ex)
            {
                ds = null;
                throw ex;
            }
            finally
            {
                // 关闭连接
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    da.Dispose();
                    conn.Dispose();
                }
            }
            return ds;
        }

        #endregion

        #region NPOI加载Excel
        /// <summary>
        /// 使用NPOI读取EXCEL数据--导入空间数据
        /// </summary>
        /// <param name="filePath">文件路经</param>
        /// <param name="typeIndex">类型参数；0-xlsx；1-xls</param>
        ///<param name="dt">填充的table</param>
        /// <returns>返回数据集</returns>
        public static DataTable LoadDataFromExcelByNPOI(string filePath, short typeIndex, DataTable dt)
        {
            HSSFWorkbook hssfworkbook;
            XSSFWorkbook xssfworkbook;

            IList<int> datetimeIndexList = new List<int>();//日期格式的数据的列索引集合
            if (typeIndex == 0)//xls
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                for (int sheetCount = 0; sheetCount < hssfworkbook.NumberOfSheets; sheetCount++)
                {
                    ISheet curSheet = hssfworkbook.GetSheetAt(sheetCount);
                    System.Collections.IEnumerator rows = curSheet.GetRowEnumerator();
                    //rows.MoveNext();
                    if (rows.MoveNext())//第一行是字段名数据
                    {
                        HSSFRow row = (HSSFRow)rows.Current;
                        if (row.PhysicalNumberOfCells != dt.Columns.Count)
                            return null;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            string strColumnName = curCell.ToString();
                            if (strColumnName.Contains("时间") || strColumnName.Contains("日期"))
                            {
                                datetimeIndexList.Add(j);//获得日期格式的列名
                            }
                        }
                    }
                    while (rows.MoveNext())
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        HSSFRow row = (HSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            if (curCell != null)
                            {
                                if (datetimeIndexList.Contains(j))
                                {
                                    string strValue = curCell.DateCellValue.ToString();
                                    dr[j] = strValue;
                                }
                                else
                                {
                                    string strValue = curCell.ToString();
                                    dr[j] = strValue;
                                }
                            }
                            else
                            {
                                dr[j] = "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            else if (typeIndex == 1)//xlsx
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                }
                for (int sheetCount = 0; sheetCount < xssfworkbook.NumberOfSheets; sheetCount++)
                {
                    ISheet curSheet = xssfworkbook.GetSheetAt(sheetCount);
                    System.Collections.IEnumerator rows = curSheet.GetRowEnumerator();
                    if (rows.MoveNext())//第一行是字段名数据
                    {
                        XSSFRow row = (XSSFRow)rows.Current;
                        if (row.PhysicalNumberOfCells != dt.Columns.Count)
                            return null;//如果列的行号不对应，那么返回null
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            string strColumnName = curCell.ToString();
                            if (strColumnName.Contains("时间") || strColumnName.Contains("日期"))
                            {
                                datetimeIndexList.Add(j);//获得日期格式的列名
                            }
                        }
                    }
                    while (rows.MoveNext())
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        XSSFRow row = (XSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            if (curCell != null)
                            {
                                if (datetimeIndexList.Contains(j))
                                {
                                    string strValue = curCell.DateCellValue.ToString();
                                    dr[j] = strValue;
                                }
                                else
                                {
                                    string strValue = curCell.ToString();
                                    dr[j] = strValue;
                                }
                            }
                            else
                            {
                                dr[j] = "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 使用NPOI读取EXCEL数据--导入地名地址
        /// </summary>
        /// <param name="filePath">文件路经</param>
        /// <param name="typeIndex">类型参数；0-xlsx；1-xls</param>
        /// <returns>返回数据集</returns>
        public static DataSet LoadDataFromExcelByNPOI(string filePath, short typeIndex)
        {
            HSSFWorkbook hssfworkbook;
            XSSFWorkbook xssfworkbook;
            DataSet ds = new DataSet();
            IList<int> datetimeIndexList = new List<int>();//日期格式的数据的列索引集合
            if (typeIndex == 0)//xls
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
                for (int sheetCount = 0; sheetCount < hssfworkbook.NumberOfSheets; sheetCount++)
                {
                    DataTable dt = new DataTable();
                    ISheet curSheet = hssfworkbook.GetSheetAt(sheetCount);
                    System.Collections.IEnumerator rows = curSheet.GetRowEnumerator();
                    if (rows.MoveNext())//第一行是字段名数据
                    {
                        HSSFRow row = (HSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            string strColumnName = curCell.ToString();
                            dt.Columns.Add(strColumnName, Type.GetType("System.String"));
                        }
                    }
                    while (rows.MoveNext())
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        HSSFRow row = (HSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            if (curCell != null)
                            {
                                if (datetimeIndexList.Contains(j))
                                {
                                    string strValue = curCell.DateCellValue.ToString();
                                    dr[j] = strValue;
                                }
                                else
                                {
                                    string strValue = curCell.ToString();
                                    dr[j] = strValue;
                                }
                            }
                            else
                            {
                                dr[j] = "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            else if (typeIndex == 1)//xlsx
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    xssfworkbook = new XSSFWorkbook(file);
                }
                for (int sheetCount = 0; sheetCount < xssfworkbook.NumberOfSheets; sheetCount++)
                {
                    DataTable dt = new DataTable();
                    ISheet curSheet = xssfworkbook.GetSheetAt(sheetCount);
                    System.Collections.IEnumerator rows = curSheet.GetRowEnumerator();
                    if (rows.MoveNext())//第一行是字段名数据
                    {
                        XSSFRow row = (XSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            string strColumnName = curCell.ToString();
                            if (strColumnName.Contains("时间") || strColumnName.Contains("日期"))
                            {
                                datetimeIndexList.Add(j);
                            }
                            dt.Columns.Add(strColumnName, Type.GetType("System.String"));
                        }
                    }
                    DateTime dTime = new DateTime(1900, 1, 1, 0, 0, 0);
                    while (rows.MoveNext())
                    {
                        System.Data.DataRow dr = dt.NewRow();
                        XSSFRow row = (XSSFRow)rows.Current;
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            ICell curCell = row.GetCell(j);
                            if (curCell != null)
                            {
                                if (datetimeIndexList.Contains(j))
                                {
                                    string strValue = curCell.DateCellValue.ToString();
                                    dr[j] = strValue;
                                }
                                else
                                {
                                    string strValue = curCell.ToString();
                                    dr[j] = strValue;
                                }
                            }
                            else
                            {
                                dr[j] = "";
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }
        #endregion
    }


}
