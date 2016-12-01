using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.MapViewCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2016/11/30,周三 11:09:51
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  0b39e0d9-f807-4b8c-9030-32321fde32be
    ** 描述： 上一视图
    *******************************/
    using Framework;
    class ZoomToLastExtentForwardCommand:SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public ZoomToLastExtentForwardCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images\UI\small_16x16\GenericBlueLeftArrowLongTail16.png");
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
        public override string Category { get { return "地图视图工具"; } }//必须设置分类信息

        public override string Tooltip { get { return "上一视图范围"; } }
        public override void Run()
        {
            base.Run();
            ESRI.ArcGIS.SystemUI.ICommand command = new ESRI.ArcGIS.Controls.ControlsMapZoomToLastExtentForwardCommand();

            command.OnCreate(this.m_MapControl);
            command.OnClick();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("0b39e0d9-f807-4b8c-9030-32321fde32be"); } }//抽象成员一定要重实现

        public override string Name { get { return "ZoomToLastExtentForwardCommand"; } }

        public override string Caption
        {
            get { return "上一视图"; }
        }
        #endregion
    }
}
