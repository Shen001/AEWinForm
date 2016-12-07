using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.MapViewCommand
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2016/12/06,周二 16:06:30
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  c51b6efe-c7a4-45e1-bce0-7a12326b808b
    ** 描述： 可以扩展成按照不同几何选择要素~
    *******************************/
    public class MapSelectFeaturesTool:SeanShen.Framework.SeanBaseTool
    {
        private System.Drawing.Bitmap m_Bitmap;
        private DevExpress.XtraBars.BarItem m_BindBarItem;
        /// <summary>
        /// 构造函数设置ui图片
        /// </summary>
        public MapSelectFeaturesTool()
        {
            try
            {
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Share.Consts.SMALLIMAGE_16, "SelectFeaturesTool.png");
                this.m_Bitmap = new System.Drawing.Bitmap(path);
            }
            catch
            {
                this.m_Bitmap = null;
            }
        }
        #region ISeanCommand成员
        public override System.Drawing.Bitmap Bitmap { get { return this.m_Bitmap; }}
        /// <summary>
        /// 分类
        /// </summary>
        public override string Category { get { return "地图视图工具"; } }//必须设置分类信息
        
        public override string Tooltip { get { return "选择要素"; } }

        public override DevExpress.XtraBars.BarItem BindBarItem { get { return this.m_BindBarItem; } set { this.m_BindBarItem = value; } }

        public override void Run()
        {
            base.Run();
            ESRI.ArcGIS.SystemUI.ICommand tool = new ESRI.ArcGIS.Controls.ControlsSelectFeaturesTool();
            tool.OnCreate(this.m_MapControl);
            this.m_MapControl.CurrentTool = (ESRI.ArcGIS.SystemUI.ITool)tool;
        }
        # endregion


        #region ISeanResource成员
        public override Guid UID { get { return Guid.Parse("c51b6efe-c7a4-45e1-bce0-7a12326b808b"); } }//抽象成员一定要重实现

        public override string Name { get { return "MapSelectFeaturesTool"; } }

        public override string Caption
        {
            get { return "选择要素"; }
        }
        #endregion
    }
}
