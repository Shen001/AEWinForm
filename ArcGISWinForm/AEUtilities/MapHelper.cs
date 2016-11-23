using System;
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
        #endregion
    }
}
