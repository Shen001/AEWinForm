using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.AOFileManageCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/01/20,周五 13:18:30
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  b12f6c09-6003-42d5-a388-08a03a5929c1
    ** 描述： 打开mxd文档文件并同步mapcontrol与pagelayout（缺点：只针对存在一个Map的mxd文档）
    *******************************/
    using SeanShen.Framework;
    using System.Windows.Forms;
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Controls;
    public class OpenNewMapDocumentCommand : SeanShen.Framework.SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public OpenNewMapDocumentCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Share.Consts.SMALLIMAGE_16, "OpenNewMapDocument.png");
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

        public override string Tooltip { get { return "打开指定MXD"; } }
        public override void Run()
        {
            base.Run();
            OpenMapDocument();
        }
        # endregion


        #region ISeanResource成员

        public override Guid UID { get { return Guid.Parse("b12f6c09-6003-42d5-a388-08a03a5929c1"); } }//抽象成员一定要重实现

        public override string Name { get { return "OpenNewMapDocumentCommand"; } }

        public override string Caption
        {
            get { return "打开MXD"; }
        }
        #endregion

        # region 私有方法
        private void OpenMapDocument()
        {
            //launch a new OpenFile dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Map Documents (*.mxd)|*.mxd";
            dlg.Multiselect = false;
            dlg.Title = "打开地图文档";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string docName = dlg.FileName;

                AEUtilities.MapHelper.Load(m_MapControl, docName);
                base.m_Application.DocumentFileName = docName;

                IActiveView pCurrentActiveView = base.m_MapControl.ActiveView.FocusMap as IActiveView;
                pCurrentActiveView.Activate(base.m_MapControl.hWnd);
                base.m_MapControl.ActiveView.Refresh();


                //IMapDocument mapDoc = new MapDocument();
                //if (mapDoc.get_IsPresent(docName) && !mapDoc.get_IsPasswordProtected(docName))
                //{
                //    mapDoc.Open(docName, string.Empty);
                //    IMap map = mapDoc.get_Map(0);//第一个map
                //    mapDoc.SetActiveView((IActiveView)map);//指定map当前的视图

                //    mapDoc.Close();

                //    base.m_Application.DocumentFileName = docName;
                //}
            }
        }

        #endregion
    }
}
