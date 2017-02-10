using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using NPOI.HPSF;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Geoprocessing;

namespace SeanShen.AEUtilities
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/02/09,周四 15:20:42
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  5c075512-4e7b-4132-91f3-3b146e915574
    ** 描述： 一些输出操作
    *******************************/
    public class ExportHelper
    {
        #region 导出图片，PDF等
        public static bool OutputJPEG(IActiveView activeView, string pathFileName)
        {
            //parameter check
            if (activeView == null || !(pathFileName.EndsWith(".jpg")))
                return false;

            IExport export = new ExportJPEGClass();
            export.ExportFileName = pathFileName;

            // Microsoft Windows default DPI resolution
            export.Resolution = 96;
            tagRECT exportRECT = activeView.ExportFrame;
            ESRI.ArcGIS.Geometry.IEnvelope envelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            export.PixelBounds = envelope;
            int hDC = export.StartExporting();
            activeView.Output(hDC, (System.Int16)export.Resolution, ref exportRECT, null, null);

            // Finish writing the export file and cleanup any intermediate files
            export.FinishExporting();
            export.Cleanup();

            return true;
        }

        public static bool OutputJPEGHiResolution(IActiveView activeView, string pathFileName)
        {
            //parameter check
            if (activeView == null || !(pathFileName.EndsWith(".jpg")))
                return false;

            IExport export = new ExportJPEGClass();
            export.ExportFileName = pathFileName;

            // Because we are exporting to a resolution that differs from screen 
            // resolution, we should assign the two values to variables for use 
            // in our sizing calculations
            System.Int32 screenResolution = 96;
            System.Int32 outputResolution = 300;

            export.Resolution = outputResolution;

            tagRECT exportRECT; // This is a structure
            exportRECT.left = 0;
            exportRECT.top = 0;
            exportRECT.right = activeView.ExportFrame.right * (outputResolution / screenResolution);
            exportRECT.bottom = activeView.ExportFrame.bottom * (outputResolution / screenResolution);

            // Set up the PixelBounds envelope to match the exportRECT
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            export.PixelBounds = envelope;

            int hDC = export.StartExporting();

            activeView.Output(hDC, (System.Int16)export.Resolution, ref exportRECT, null, null); // Explicit Cast and 'ref' keyword needed 
            export.FinishExporting();
            export.Cleanup();

            return true;
        }

        /// <summary>
        /// 导出视图为图片
        /// </summary>
        /// <param name="activeView">地图视图</param>
        /// <param name="pathFileName">导出文件名称</param>
        /// <param name="bUseHiDpi">是否使用高分辨率，默认为true</param>
        public static void ExportActiveView(IActiveView activeView, string pathFileName, bool bUseHiDpi = true)
        {
            if (activeView == null || string.IsNullOrEmpty(pathFileName))
            {
                return;
            }

            IExport export = null;
            if (pathFileName.EndsWith(".jpg"))
            {
                export = new ExportJPEGClass();
            }
            else if (pathFileName.EndsWith(".tiff"))
            {
                export = new ExportTIFFClass();
            }
            else if (pathFileName.EndsWith(".bmp"))
            {
                export = new ExportBMPClass();
            }
            else if (pathFileName.EndsWith(".emf"))
            {
                export = new ExportEMFClass();
            }
            else if (pathFileName.EndsWith(".png"))
            {
                export = new ExportPNGClass();
            }
            else if (pathFileName.EndsWith(".gif"))
            {
                export = new ExportGIFClass();
            }
            else if (pathFileName.EndsWith(".pdf"))
            {
                export = new ExportPDFClass();
            }
            //DateTime dt = DateTime.Now;
            //string strTime = string.Format("{0:yyyyMMddHHmmss}", dt);
            //string fileName = System.IO.Path.GetFileNameWithoutExtension(pathFileName);

            //string mergeStr = string.Format(System.IO.Path.GetDirectoryName(pathFileName) + "\\" + fileName + strTime+System.IO.Path.GetExtension(pathFileName));
            export.ExportFileName = pathFileName;

            // Because we are exporting to a resolution that differs from screen 
            // resolution, we should assign the two values to variables for use 
            // in our sizing calculations
            System.Int32 screenResolution = 96;
            System.Int32 outputResolution = bUseHiDpi ? 300 : 96;

            export.Resolution = outputResolution;

            tagRECT exportRECT; // This is a structure
            exportRECT.left = 0;
            exportRECT.top = 0;
            exportRECT.right = activeView.ExportFrame.right * (outputResolution / screenResolution);
            exportRECT.bottom = activeView.ExportFrame.bottom * (outputResolution / screenResolution);

            // Set up the PixelBounds envelope to match the exportRECT
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(exportRECT.left, exportRECT.top, exportRECT.right, exportRECT.bottom);
            export.PixelBounds = envelope;

            int hDC = export.StartExporting();

            activeView.Output(hDC, (System.Int16)export.Resolution, ref exportRECT, null, null); // Explicit Cast and 'ref' keyword needed 
            export.FinishExporting();
            export.Cleanup();

        }
        #endregion

        #region 空间数据库数据互相转换

        public static bool ConvertFeatureDataset(IWorkspace sourceWorkspace, IWorkspace targetWorkspace,
        string nameOfSourceFeatureDataset, string nameOfTargetFeatureDataset)
        {
            try
            {
                //create source workspace name    
                IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
                IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;
                //create source dataset name     
                IFeatureDatasetName sourceFeatureDatasetName = new FeatureDatasetNameClass();
                IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureDatasetName;
                sourceDatasetName.WorkspaceName = sourceWorkspaceName;
                sourceDatasetName.Name = nameOfSourceFeatureDataset;
                //create target workspace name     
                IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
                IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;
                //create target dataset name    
                IFeatureDatasetName targetFeatureDatasetName = new FeatureDatasetNameClass();
                IDatasetName targetDatasetName = (IDatasetName)targetFeatureDatasetName;
                targetDatasetName.WorkspaceName = targetWorkspaceName;
                targetDatasetName.Name = nameOfTargetFeatureDataset;
                //Convert feature dataset       
                IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();
                featureDataConverter.ConvertFeatureDataset(sourceFeatureDatasetName, targetFeatureDatasetName, null, "", 1000, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>  
        /// 导出FeatureClass到Shapefile文件  
        /// </summary>  
        /// <param name="sPath"></param>  
        /// <param name="apFeatureClass"></param>  
        public static bool ExportFeatureClassToShp(string sPath, IFeatureClass apFeatureClass)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string strTime = string.Format("{0:yyyyMMddHHmmss}", dt);
                string fileName = System.IO.Path.GetFileNameWithoutExtension(sPath);//文件名称
                string ExportFilePath = System.IO.Path.GetDirectoryName(sPath);//文件夹路径
                string ExportFileName = ExportFilePath.Substring(ExportFilePath.LastIndexOf("\\") + 1);//最后选择的文件夹
                //如果是c:\\这样的根目录，那么根日期建立一个导出文件夹的名称
                if (string.IsNullOrEmpty(ExportFileName))
                {
                    ExportFileName = "导出Shape文件";
                }
                else
                {
                    ExportFilePath = ExportFilePath.Substring(0, ExportFilePath.LastIndexOf("\\"));//除了导出文件夹的路径
                }
                //设置导出要素类的参数  
                IFeatureClassName pOutFeatureClassName = new FeatureClassNameClass();
                IDataset pOutDataset = (IDataset)apFeatureClass;
                pOutFeatureClassName = (IFeatureClassName)pOutDataset.FullName;
                //创建一个输出shp文件的工作空间  
                IWorkspaceFactory pShpWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                IWorkspaceName pInWorkspaceName = new WorkspaceNameClass();
                pInWorkspaceName = pShpWorkspaceFactory.Create(ExportFilePath, ExportFileName, null, 0);

                //创建一个要素集合  
                IFeatureDatasetName pInFeatureDatasetName = null;
                //创建一个要素类  
                IFeatureClassName pInFeatureClassName = new FeatureClassNameClass();
                IDatasetName pInDatasetClassName;
                pInDatasetClassName = (IDatasetName)pInFeatureClassName;
                pInDatasetClassName.Name = fileName + strTime;//作为输出参数  
                pInDatasetClassName.WorkspaceName = pInWorkspaceName;
                //通过FIELDCHECKER检查字段的合法性，为输出SHP获得字段集合  
                long iCounter;
                IFields pOutFields, pInFields;

                IField pGeoField;
                IEnumFieldError pEnumFieldError = null;

                IQueryFilter filter = null;
                string subset = "";

                pInFields = apFeatureClass.Fields;
                IFieldChecker pFieldChecker = new FieldChecker();
                pFieldChecker.Validate(pInFields, out pEnumFieldError, out pOutFields);

                //通过循环查找几何字段  
                pGeoField = null;
                for (iCounter = 0; iCounter < pOutFields.FieldCount; iCounter++)
                {
                    IField field = pOutFields.get_Field((int)iCounter);
                    if (field.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        pGeoField = field;
                    }
                    if (!IsBlob(field))
                    {
                        string fieldName = field.Name;
                        if (fieldName.Contains("_"))
                        {
                            fieldName = fieldName.Replace("_", ".");
                        }
                        subset += (fieldName + ",");
                    }
                }
                subset = subset.Substring(0, subset.LastIndexOf(","));
                filter = new QueryFilterClass();
                filter.SubFields = subset;

                //得到几何字段的几何定义  
                IGeometryDef pOutGeometryDef;
                IGeometryDefEdit pOutGeometryDefEdit;
                pOutGeometryDef = pGeoField.GeometryDef;
                //设置几何字段的空间参考和网格  
                pOutGeometryDefEdit = (IGeometryDefEdit)pOutGeometryDef;
                pOutGeometryDefEdit.GridCount_2 = 1;
                pOutGeometryDefEdit.set_GridSize(0, 1500000);

                //开始导入  
                IFeatureDataConverter pShpToClsConverter = new FeatureDataConverterClass();
                pShpToClsConverter.ConvertFeatureClass(pOutFeatureClassName, filter, pInFeatureDatasetName, pInFeatureClassName, pOutGeometryDef, pOutFields, "", 1000, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //判断是否类型为blob的字段
        private static bool IsBlob(IField field)
        {
            if (field.Type == esriFieldType.esriFieldTypeBlob)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 将layer中的要素选择集selection转换为shape文件
        /// </summary>
        /// <param name="strParaFullPath">除文件名之外的全路径</param>
        /// <param name="strTemLyrName">文件命名，用来创建shape工作空间,可以设置为空字符串</param>
        /// <param name="iParaFeaLayer">当前操作的要素层</param>
        public static bool ExportSelection2Shp(string strParaFullPath, string strTemLyrName, IFeatureLayer iParaFeaLayer)
        {
            //string ExportFilePath = System.IO.Path.GetDirectoryName(strParaFullPath);
            IFeatureClass m_InFeaClass;
            IDataset m_InDataSet;
            IDatasetName m_InDataSetName;
            IFeatureSelection m_FeaSelection;
            IWorkspaceFactory m_WorkspaceFac;
            IWorkspaceName m_OutWorkspaceName;
            IFeatureClassName m_FeaClassName;
            IDatasetName m_OutDataSetName;

            DateTime dt = DateTime.Now;
            string strTime = string.Format("{0:yyyyMMddHHmmss}", dt);
            string ExportFileName = strParaFullPath.Substring(strParaFullPath.LastIndexOf("\\") + 1);//当前文件夹名称
            string ExportFilePath = strParaFullPath;
            if (string.IsNullOrEmpty(ExportFileName))
            {
                ExportFileName = "导出Shape文件";
            }
            else
            {
                ExportFilePath = ExportFilePath.Substring(0, ExportFilePath.LastIndexOf("\\"));//除了导出文件夹的路径
            }

            m_InFeaClass = iParaFeaLayer.FeatureClass;
            m_InDataSet = m_InFeaClass as IDataset;
            m_InDataSetName = m_InDataSet.FullName as IDatasetName;

            m_FeaSelection = iParaFeaLayer as IFeatureSelection;
            ISelectionSet iSelecttionSet = m_FeaSelection.SelectionSet;

            if (strTemLyrName.Trim() == "")
                strTemLyrName = iParaFeaLayer.Name;
            m_WorkspaceFac = new ShapefileWorkspaceFactory();
            //判断指定的文件夹是否已经存在，如果存在则删除之前的文件夹
            //string path=string.Format(strParaFullPath+"\\"+strTemLyrName);
            //if(Directory.Exists(path))
            //    Directory.Delete(path,true);
            m_OutWorkspaceName = m_WorkspaceFac.Create(ExportFilePath, ExportFileName, null, 0);
            m_FeaClassName = new FeatureClassNameClass();
            m_OutDataSetName = m_FeaClassName as IDatasetName;
            m_OutDataSetName.WorkspaceName = m_OutWorkspaceName;
            m_OutDataSetName.Name = strTemLyrName + strTime;

            IQueryFilter filter = null;
            string subset = "";
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields shapefileFields = null;
            IEnumFieldError enumFieldError = null;
            fieldChecker.InputWorkspace = m_InDataSet.Workspace;
            fieldChecker.ValidateWorkspace = m_InDataSet.Workspace;
            fieldChecker.Validate(m_InFeaClass.Fields, out enumFieldError, out shapefileFields);
            int iCounter;
            IField pGeoField = null;
            for (iCounter = 0; iCounter < shapefileFields.FieldCount; iCounter++)
            {
                IField field = shapefileFields.get_Field((int)iCounter);
                if (field.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    pGeoField = field;
                }
                if (!IsBlob(field))
                {
                    string fieldName = field.Name;
                    if (fieldName.Contains("_"))
                    {
                        fieldName = fieldName.Replace("_", ".");
                    }
                    subset += (fieldName + ",");
                }
            }
            subset = subset.Substring(0, subset.LastIndexOf(","));
            filter = new QueryFilterClass();
            filter.SubFields = subset;

            //得到几何字段的几何定义  --- shape文件没有grid
            //IGeometryDef pOutGeometryDef;
            //IGeometryDefEdit pOutGeometryDefEdit;
            //pOutGeometryDef = pGeoField.GeometryDef;
            ////设置几何字段的空间参考和网格  
            //pOutGeometryDefEdit = (IGeometryDefEdit)pOutGeometryDef;
            //pOutGeometryDefEdit.GridCount_2 = 1;
            //pOutGeometryDefEdit.set_GridSize(0, 1500000);
            //pOutGeometryDefEdit.SpatialReference_2 = iParaFeaLayer.SpatialReference;iParaFeaLayer的空间参考只读

            IFeatureDataConverter2 pConvetLayer2Shape = new FeatureDataConverterClass();
            pConvetLayer2Shape.ConvertFeatureClass(m_InDataSetName, filter, iSelecttionSet, null, m_FeaClassName, null, shapefileFields, "", 1000, 0);
            return true;
        }
        #endregion

        #region GP导出到CAD中，可以多图层
        public static bool ExportLayer2Dwg(IList<IFeatureLayer> pInPutLayerList, string outputfile)
        {
            Geoprocessor GP = new Geoprocessor();
            GP.OverwriteOutput = true;//覆盖同名
            GP.SetEnvironmentValue("workspace", @"C:\temp");

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < pInPutLayerList.Count; i++)
            {
                IFeatureLayer layer = pInPutLayerList[i];

                IDataset dataset = layer.FeatureClass as IDataset;

                string layerfullname = dataset.Workspace.PathName + "\\" + dataset.Name;
                builder.Append(layerfullname + ";");
            }

            string fullfilepath = builder.ToString().Substring(0, builder.Length - 1);

            ExportCAD exportcad = new ExportCAD();
            exportcad.in_features = fullfilepath;
            exportcad.Output_Type = "DWG_R2010";
            exportcad.Output_File = outputfile;

            try
            {
                IGeoProcessorResult results = (IGeoProcessorResult)GP.Execute(exportcad, null);
                string msg = "";
                if (GP.MessageCount > 0)
                {
                    for (int i = 0; i < GP.MessageCount; i++)
                    {
                        msg += GP.GetMessage(i) + "\n";
                    }
                }
                if (msg.Contains("Successed"))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //实现了从sde中导出数据到dwg
        public static void ExportLayertoDwg(IList<IFeatureLayer> pInPutLayerList, string outputfile, ref List<IFeatureLayer> successedList, ref List<IFeatureLayer> failedList)
        {
            try
            {
                //string connectionfile = ConfigurationManager.AppSettings["ConnectionSDE"];//读取配置文件中sde的位置，如Connection to sde.sde
                string connectionfile = "";
                string _RemoteLinkDBStr = System.Windows.Forms.Application.StartupPath + "\\" + connectionfile;
                string workPath = string.Empty;//数据库连接路经
                string layerfullname = string.Empty;//数据源
                for (int i = 0; i < pInPutLayerList.Count; i++)
                {
                    IFeatureLayer layer = pInPutLayerList[i];
                    IDataset dataset = layer.FeatureClass as IDataset;
                    if (dataset.Workspace.Type == esriWorkspaceType.esriRemoteDatabaseWorkspace)
                    {
                        if (i == 0)
                        {
                            workPath = _RemoteLinkDBStr;
                            layerfullname = System.IO.Path.Combine(_RemoteLinkDBStr, dataset.Name);
                        }
                        else
                        {
                            layerfullname += string.Format(";" + dataset.Name);
                        }
                    }
                    else
                    {
                        if (workPath == string.Empty)
                        {
                            workPath = dataset.Workspace.PathName;//未设置本地...
                        }
                    }
                }
                Geoprocessor GP = new Geoprocessor();
                GP.OverwriteOutput = true;//覆盖同名
                GP.SetEnvironmentValue("workspace", workPath);
                //获取数据源全路径
                ExportCAD exportcad = new ExportCAD();
                exportcad.in_features = layerfullname;
                exportcad.Output_Type = "DWG_R2010";
                exportcad.Output_File = outputfile;

                IGeoProcessorResult results = (IGeoProcessorResult)GP.Execute(exportcad, null);

                string msg = "";
                if (GP.MessageCount > 0)
                {
                    for (int j = 0; j < GP.MessageCount; j++)
                    {
                        msg += GP.GetMessage(j) + "\n";
                    }
                }
                if (msg.Contains("Successed") || msg.Contains("成功"))
                {
                    string message = msg;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region NPOI导出到Excel
                /// <summary>
        /// 将DataTable导出到指定路径的EXCEL中，不依赖于是否安装了Office
        /// </summary>
        /// <param name="dt">需要导出的DataTable</param>
        /// <param name="savePath">导出的文件路径，全路径</param>
        /// <returns>是否成功导出</returns>
        public static bool ExportDataTable2ExcelByNPOI(DataTable dt, string savePath)
        {
            System.IO.FileStream file;
            try
            {
                file = new System.IO.FileStream(savePath, FileMode.Create);
            }
            catch {return false; }
            try
            {
                HSSFWorkbook  workbook = CreateWorkBook();
                ISheet sheetNew;
                if (dt.TableName == "")
                {
                    sheetNew = workbook.CreateSheet();
                }
                else
                {
                    sheetNew = workbook.CreateSheet(dt.TableName);
                }
                //列名行
                NPOI.SS.UserModel.IRow row = sheetNew.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                //数据行
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow pRow = sheetNew.CreateRow(i);
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string cellValue = dt.Rows[i - 1].ItemArray[k].ToString();
                        pRow.CreateCell(k).SetCellValue(cellValue);
                    }
                }
                workbook.Write(file);//如果存在同名文件夹，那么旧文件夹自动被覆盖！
                return true;
            }
            finally
            {
                file.Close();
            }
        }
        /// <summary>//原设置是为了单个或者多个datatable，但是因为HSSFWorkbook必须作为参数，而不想在引用方法的工程里面添加引用，所以只作为多个datatable的方法
        /// 将DataTable导出到指定路径的EXCEL中，不依赖于是否安装了Office
        /// </summary>
        /// <param name="dt">需要导出的DataTable</param>
        /// <param name="savePath">导出的文件路径，全路径</param>
        /// <param name="mode">文件流模式，默认为追加</param>
        /// <param name="workbook">如果是单个datatable，默认传入null，如果是多个datatable(即dataset)，则传入workbook</param>
        /// <returns>是否成功导出</returns>
        private static bool ExportDataTables2ExcelByNPOI(DataTable dt, string savePath,ref System.IO.FileStream file,HSSFWorkbook workbook=null)
        {
            try
            {
                if (workbook == null)
                    workbook = CreateWorkBook();
                ISheet sheetNew;
                if (dt.TableName == "")
                {
                    sheetNew = workbook.CreateSheet();
                }
                else
                {
                    sheetNew = workbook.CreateSheet(dt.TableName);
                }
                //列名行
                NPOI.SS.UserModel.IRow row = sheetNew.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                //数据行
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow pRow = sheetNew.CreateRow(i);
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string cellValue = dt.Rows[i - 1].ItemArray[k].ToString();
                        pRow.CreateCell(k).SetCellValue(cellValue);
                    }
                }
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 将DataSet导出到指定路经的EXCEL中，一个Table对应一个sheet，不依赖于是否安装了Office
        /// </summary>
        /// <param name="ds">需要导出的DataSet</param>
        /// <param name="savePath">导出的文件路径，全路径</param>
        /// <returns>是否成功导出</returns>
        public static bool ExportDataSet2ExcelByNPOI(DataSet ds, string savePath)
        {
            if (ds.Tables.Count == 0)
                return false;
            HSSFWorkbook workbook = CreateWorkBook();
            System.IO.FileStream file;
            try
            {
                file = new System.IO.FileStream(savePath,FileMode.Create);
            }
            catch { return false; }
            //第一次还是要创建文件流
            if (!ExportDataTables2ExcelByNPOI(ds.Tables[0], savePath, ref file, workbook))
                return false;
            if (ds.Tables.Count == 1)
            {
                workbook.Write(file);//如果存在同名文件夹，那么旧文件夹自动被覆盖！
                file.Close();
                return true;
            }
            else
            {
                file.Close();
            }
            file = new System.IO.FileStream(savePath, FileMode.Append);
            //第二次以后
            for (int i = 1; i < ds.Tables.Count; i++)
            {
                if (!ExportDataTables2ExcelByNPOI(ds.Tables[i], savePath, ref file, workbook))
                    return false;
                if (ds.Tables.Count == i + 1)
                {
                    workbook.Write(file);//如果存在同名文件夹，那么旧文件夹自动被覆盖！
                    file.Close();
                    return true;
                }
            }
            return false;
        }
        //创建workbook
        private static HSSFWorkbook CreateWorkBook()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "test";
            workbook.SummaryInformation = si;
            return workbook;
        }
        /// <summary>
        /// 导出合并单元格的Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="savePath"></param>
        /// <param name="dicMerge">合并数据集的格式</param>
        /// <returns></returns>
        public static bool ExportDataTable2MergeCellsInExcelByNPOI(DataTable dt,string savePath,Dictionary<string,Dictionary<string,int>> dicMerge)
        {
            System.IO.FileStream file;
            try
            {
                file = new System.IO.FileStream(savePath, FileMode.Create);
            }
            catch {return false; }
            try
            {
                HSSFWorkbook workbook = CreateWorkBook();
                ISheet sheetNew;
                if (dt.TableName == "")
                {
                    sheetNew = workbook.CreateSheet();
                }
                else
                {
                    sheetNew = workbook.CreateSheet(dt.TableName);
                }

                //列名行
                NPOI.SS.UserModel.IRow row = sheetNew.CreateRow(0);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                }
                //数据行
                for (int i = 1; i <= dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow pRow = sheetNew.CreateRow(i);
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        string cellValue = dt.Rows[i - 1].ItemArray[k].ToString();
                        ICell curCell = pRow.CreateCell(k);
                        curCell.SetCellValue(cellValue);
                    }
                }
                int startRow = 1;
                //合并路名列
                foreach (KeyValuePair<string,Dictionary<string,int>> item in dicMerge)
                {
                    string roadName = item.Key;
                    int sameRoadRowsCount = GetRowsFromDic(item.Value,startRow,ref sheetNew);
                    CellRangeAddress range = new CellRangeAddress(startRow, startRow+sameRoadRowsCount - 1, 0, 0);
                    sheetNew.AddMergedRegion(range);
                    //((HSSFSheet)sheetNew).SetEnclosedBorderOfRegion(range,NPOI.SS.UserModel.BorderStyle.Dotted, NPOI.HSSF.Util.HSSFColor.Red.Index);

                    startRow += sameRoadRowsCount;
                }
                ICellStyle style = workbook.CreateCellStyle();
                style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
                workbook.Write(file);//如果存在同名文件夹，那么旧文件夹自动被覆盖！
                return true;
            }
            finally
            {
                file.Close();
            }
        }
        //根据图层/行数字典获得总行数并合并图层名列
        private static int GetRowsFromDic(Dictionary<string, int> dicLayerRowsCount,int startRow,ref ISheet sheet)
        {
            int currentRoadRowCount = 0;
            int layerStartRow = startRow;
            foreach (var item in dicLayerRowsCount.Keys)
            {
                string layerName = item.ToString();
                int currentLayerRowsCount = dicLayerRowsCount[layerName];
                //合并图层名列
                CellRangeAddress range = new CellRangeAddress(layerStartRow, layerStartRow+currentLayerRowsCount - 1, 1, 1);
                sheet.AddMergedRegion(range);
               // ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(range, NPOI.SS.UserModel.BorderStyle.Dotted, NPOI.HSSF.Util.HSSFColor.Red.Index);

                layerStartRow += currentLayerRowsCount;
                currentRoadRowCount += currentLayerRowsCount;
            }
            return currentRoadRowCount;
        }
        #endregion
    }
}
