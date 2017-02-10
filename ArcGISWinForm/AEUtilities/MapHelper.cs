using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace SeanShen.AEUtilities
{
    public class MapHelper
    {
        #region 文档操作
        /// <summary>
        /// 加载Mxd
        /// </summary>
        /// <param name="mapControlDefault">当前视图mapcontrol</param>
        /// <param name="strFilePath">文件路径-可以为相对路径</param>
        /// <param name="mapNameOrIndex">地图名称或者索引，默认为空</param>
        /// <param name="password">文档密码，默认为空</param>
        public static void Load(IMapControlDefault mapControlDefault, string strFilePath, object mapNameOrIndex = null, object password = null)
        {
            if (mapNameOrIndex == null)
            {
                mapNameOrIndex = Type.Missing;
            }
            if (password == null)
            {
                password = Type.Missing;
            }
            if (mapControlDefault.CheckMxFile(strFilePath))
            {
                mapControlDefault.LoadMxFile(strFilePath, mapNameOrIndex, password);
            }
        }
        /// <summary>
        /// 确定之后将文档进行默认保存
        /// </summary>
        /// <param name="mapControlDefault">当前视图mapcontrol</param>
        /// <returns></returns>
        public static bool SaveAsMxd(string saveAsPath, IMapControlDefault mapControlDefault,bool useRelativePath)
        {
            IMxdContents pMxdContents;
            pMxdContents = mapControlDefault.Map as IMxdContents;
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(mapControlDefault.DocumentFilename, "");
            pMapDocument.ReplaceContents(pMxdContents);

            pMapDocument.SaveAs(saveAsPath,useRelativePath, false);
            pMapDocument.Close();
            return true;
        }
        /// <summary>
        /// 返回当前map的地图单位（中文）
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string GetMapUnit(ESRI.ArcGIS.esriSystem.esriUnits unit)
        {
            string mapUnit = "未知";
            switch (unit)
            {
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMillimeters:
                    mapUnit = "毫米";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriCentimeters:
                    mapUnit = "厘米";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimeters:
                    mapUnit = "分米";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMeters:
                    mapUnit = "米";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriDecimalDegrees://WGS1984默认坐标，十进制度 decimal degree=度＋分/60＋秒/3600
                    mapUnit = "度";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriFeet:
                    mapUnit = "英尺";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriInches:
                    mapUnit = "英寸";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriKilometers:
                    mapUnit = "千米";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriMiles:
                    mapUnit = "英里";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriNauticalMiles:
                    mapUnit = "海里";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriPoints:
                    mapUnit = "点";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriUnitsLast:
                    mapUnit = "内部使用";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriUnknownUnits:
                    mapUnit = "未知";
                    break;
                case ESRI.ArcGIS.esriSystem.esriUnits.esriYards:
                    mapUnit = "码";
                    break;
                default:
                    break;
            }
            return mapUnit;
        }
        #endregion

        #region 屏幕距离转换地图距离

        /// <summary>
        /// 屏幕距离转换为地图距离
        /// </summary>
        /// <param name="pScreenDisplay"></param>
        /// <param name="pixel">像素距离</param>
        /// <returns></returns>
        public static double ConvertPixelToMapUnit(IMap pMap, int pixel)
        {
            IActiveView pActiveView = pMap as IActiveView;
            if (pActiveView == null) return 0;

            return ConvertPixelToMapUnit(pActiveView.ScreenDisplay, pixel);
        }

        /// <summary>
        /// 屏幕距离转换为地图距离,用地图可视范围的宽度/设备宽，得到每个像素代表的地图长度
        /// </summary>
        /// <param name="pScreenDisplay"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public static double ConvertPixelToMapUnit(IScreenDisplay pScreenDisplay, int pixel)
        {
            if (pScreenDisplay == null) return 0;

            IDisplayTransformation pDisplayTransformation = pScreenDisplay.DisplayTransformation;

            tagRECT rectDevice = pDisplayTransformation.get_DeviceFrame();
            IEnvelope pMapBoundary = pDisplayTransformation.VisibleBounds;

            int nDeviceWidth = rectDevice.right - rectDevice.left;
            double dMapWidth = pMapBoundary.Width;

            return pixel * (dMapWidth / nDeviceWidth);
        }
        #endregion

        /// <summary>
        /// 清除当前地图的选中
        /// </summary>
        /// <param name="mapcontrol4"></param>
        public static void ClearSelection(IMapControl4 mapcontrol4)
        {
            mapcontrol4.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
            mapcontrol4.Map.ClearSelection();
            mapcontrol4.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
    }
}
