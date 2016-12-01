using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.MapViewCommand
{
            
/*
    Time: 28/11/2016 13:52  周一
    Author: shenxin
    Description: 缩小视图
    Modify:
 */
    using SeanShen.Framework;
    public class MapZoomOutTool:SeanBaseTool
    {
           private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public MapZoomOutTool()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images\UI\small_16x16\ZoomOutTool_B_16.png");
                this.m_Bitmap = new System.Drawing.Bitmap(path);
            }
            catch
            {
                this.m_Bitmap = null;
            }
        }
        #region ISeanCommand成员
        public override System.Drawing.Bitmap Bitmap { get { return this.m_Bitmap; } }
        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "地图视图工具"; } }//必须设置分类信息

        public override string Tooltip { get { return "缩小视图"; } }

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }


        public override void Run()
        {
            base.Run();
            ESRI.ArcGIS.SystemUI.ICommand tool = new ESRI.ArcGIS.Controls.ControlsMapZoomOutTool();
            tool.OnCreate(this.m_MapControl);
            this.m_MapControl.CurrentTool = (ESRI.ArcGIS.SystemUI.ITool)tool;
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("81C2707E-DA9D-4343-96B5-607DC5B555FE"); } }//抽象成员一定要重实现

        public override string Name { get { return "MapZoomOutTool"; } }

        public override string Caption
        {
            get { return "缩小视图"; }
        }
        #endregion
    }
}
