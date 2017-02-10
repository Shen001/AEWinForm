using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.AOFileManageCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/02/09,周四 13:22:36
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  d5cc7d89-a3ff-43cf-82a2-d2af68ff0d25
    ** 描述： 新建地图空白文档
    *******************************/
    using SeanShen.Framework;
    using System.Windows.Forms;
    using ESRI.ArcGIS.SystemUI;
    using ESRI.ArcGIS.Controls;
    using ESRI.ArcGIS.Carto;
    public class CreateNewMapDocumentCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public CreateNewMapDocumentCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Share.Consts.SMALLIMAGE_16, "CreateNewMapDocument.png");
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

        public override string Tooltip { get { return "新建空白MXD"; } }
        public override void Run()
        {
            base.Run();
            CreateNewMapDocument();
        }
        # endregion


        #region ISeanResource成员

        public override Guid UID { get { return Guid.Parse("d5cc7d89-a3ff-43cf-82a2-d2af68ff0d25"); } }//抽象成员一定要重实现

        public override string Name { get { return "CreateNewMapDocumentCommand"; } }

        public override string Caption
        {
            get { return "新建MXD"; }
        }
        #endregion

        # region 私有方法
        private void CreateNewMapDocument()
        {
            //确定当前的mxd是否需要保存
            DialogResult res = MessageBox.Show("需要保存当前文档的修改?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                ICommand command = new ControlsSaveAsDocCommand();
                command.OnCreate(m_MapControl);
                command.OnClick();
            }

            IMap pNewMap = new Map();
            pNewMap.Name = "地图";
            m_Application.DocumentFileName = null;

            m_MapControl.Map = pNewMap;
            m_MapControl.ActiveView.Refresh();
        }
             
        #endregion
    }
}
