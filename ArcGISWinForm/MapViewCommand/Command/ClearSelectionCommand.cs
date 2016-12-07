using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.MapViewCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2016/12/06,周二 16:20:18
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  6e70cc04-9f4b-4cb2-ad04-766ed7a2ce67
    ** 描述： 
    *******************************/
    public class ClearSelectionCommand:SeanShen.Framework.SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;

        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public ClearSelectionCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Share.Consts.SMALLIMAGE_16,"ClearSelection.png");
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
        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "地图视图工具"; } }//必须设置分类信息

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        public override string Tooltip { get { return "清除选择集"; } }
        public override void Run()
        {
            base.Run();
            ESRI.ArcGIS.SystemUI.ICommand command = new ESRI.ArcGIS.Controls.ControlsClearSelectionCommand();
            command.OnCreate(this.m_MapControl);
            command.OnClick();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("6e70cc04-9f4b-4cb2-ad04-766ed7a2ce67"); } }//抽象成员一定要重实现

        public override string Name { get { return "ClearSelectionCommand"; } }

        public override string Caption
        {
            get { return "清除选择集"; }
        }
        #endregion
    }
}
