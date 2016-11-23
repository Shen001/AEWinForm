using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapViewCommand
{

    using SeanShen.Framework;
    /// <summary>
    /// 全图command
    /// </summary>
    public class MapFullExtentCommand : SeanBaseCommand
    {
        private System.Drawing.Bitmap m_Bitmap;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public MapFullExtentCommand()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images\UI\small16x16\ZoomFullExtent16.png");
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

        public override string Tooltip { get { return "全图"; } }
        public override void Run()
        {
            base.Run();
            ESRI.ArcGIS.SystemUI.ICommand command = new ESRI.ArcGIS.Controls.ControlsMapFullExtentCommandClass();
            command.OnCreate(this.m_MapControl);
            command.OnClick();
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("7EA91BC1-97AA-4A22-B1EC-E0587465D531"); } }//抽象成员一定要重实现

        public override string Name { get { return "全图"; } }
        #endregion
    }
}
