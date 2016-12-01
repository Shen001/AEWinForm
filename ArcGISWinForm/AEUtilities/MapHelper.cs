﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

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
        public static bool SaveInMxd(IMapControlDefault mapControlDefault)
        {
            IMxdContents pMxdContents;
            pMxdContents = mapControlDefault.Map as IMxdContents;
            IMapDocument pMapDocument = new MapDocumentClass();
            pMapDocument.Open(mapControlDefault.DocumentFilename, "");
            pMapDocument.ReplaceContents(pMxdContents);

            pMapDocument.Save(true, true);
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
    }
}
