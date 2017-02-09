using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.AOFileManageCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/02/09,周四 09:28:13
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  36d528ef-c149-41d5-8193-163e2bca6c50
    ** 描述： 保存当前文档
    *******************************/
    using SeanShen.Framework;
    public class SaveMapDocumentCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public SaveMapDocumentCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Share.Consts.SMALLIMAGE_16, "SaveMapDocument.png");
                this.m_Bitmap = new System.Drawing.Bitmap(path);
            }
            catch
            {
                this.m_Bitmap = null;
            }
        }
        #region ISeanCommand成员

        public override System.Drawing.Bitmap Bitmap
        {
            get { return this.m_Bitmap; }
        }

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "AO文件工具"; } }//必须设置分类信息

        public override string Tooltip { get { return "保存当前MXD"; } }
        public override void Run()
        {
            base.Run();
            SaveMapDocument();
        }
        # endregion


        #region ISeanResource成员

        public override Guid UID { get { return Guid.Parse("36d528ef-c149-41d5-8193-163e2bca6c50"); } }//抽象成员一定要重实现

        public override string Name { get { return "SaveMapDocumentCommand"; } }

        public override string Caption
        {
            get { return "保存MXD"; }
        }
        #endregion

        # region 私有方法
        private void SaveMapDocument()
        {
            //make sure that the current MapDoc is valid first
            if (m_Application.DocumentFileName != string.Empty && m_MapControl.CheckMxFile(m_Application.DocumentFileName))
            {
                //create a new instance of a MapDocument class
                ESRI.ArcGIS.Carto.IMapDocument mapDoc = new ESRI.ArcGIS.Carto.MapDocument();
                //Open the current document into the MapDocument
                mapDoc.Open(m_Application.DocumentFileName, string.Empty);

                //Replace the map with the one of the PageLayout
                mapDoc.ReplaceContents((ESRI.ArcGIS.Carto.IMxdContents)m_MapControl);
                //save the document
                mapDoc.Save(true, false);
                mapDoc.Close();
            }
        }
             
        #endregion
    }
}
