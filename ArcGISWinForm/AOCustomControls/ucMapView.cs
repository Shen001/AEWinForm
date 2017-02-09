using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeanShen.AOCustomControls
{
    /*
    Time: 22/11/2016 15:49 PM 周二
    Author: shenxin
    Description: 封装mapcontrol控件
    Modify:（需要重构identify）
    */

    using Framework;
    using ESRI.ArcGIS.Controls;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.Carto;
    public partial class ucMapView : UserControl, ISeanMapControlView
    {
        private ISeanApplication mApplication = null;
        private IScreenDisplay mScreenDisplay = null;
        public ucMapView()
        {
            InitializeComponent();
            axMapControl1.Dock = DockStyle.Fill;
            this.axMapControl1.OnExtentUpdated += new IMapControlEvents2_Ax_OnExtentUpdatedEventHandler(axMapControl1_OnExtentUpdated);
            this.axMapControl1.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(axMapControl1_OnMouseMove);
            this.axMapControl1.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(axMapControl1_OnMouseUp);
        }

        void axMapControl1_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (mScreenDisplay!=null&&4 == e.button)
            {
                mScreenDisplay.PanStop();
                this.axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewAll, null, this.axMapControl1.ActiveView.Extent);
            }
        }

        void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (4 == e.button)
            {
                mScreenDisplay = this.axMapControl1.ActiveView.ScreenDisplay;
                mScreenDisplay.PanStart(mScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y));
            }
        }

        void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            this.mApplication.StatusBar.ShowMapCoordinate(e.mapX, e.mapY);
            if (e.button == 4&&mScreenDisplay!=null)
            {
                mScreenDisplay.PanMoveTo(mScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y));
            }
        }
        //视图范围大小改变
        void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            
        }

        #region IMapControlView成员
        public ESRI.ArcGIS.Controls.IMapControlDefault GetIMapControl()
        {
            IMapControlDefault mapcontrol = this.axMapControl1.Object as IMapControlDefault;
            return mapcontrol;
        }

        public ESRI.ArcGIS.Controls.AxMapControl GetAxMapControl()
        {
            return this.axMapControl1;
        }
        #endregion

        # region ISeanView成员

        public Control GetControl()
        {
            return this;
        }

        public new void Show()
        {
            this.ParentForm.Show();
        }

        # endregion

        #region ISeanResource成员

        /// <summary>
        /// guid唯一标示符
        /// </summary>
        public Guid UID { get { return Guid.Parse("44CB876E-4886-417E-8C89-D1855E222D7A"); } }
        //资源名称
        string ISeanResource.Name { get { return "ucMapView"; } }

        public string Caption { get { return "地图视图"; } }
        /// <summary>
        /// 资源类型
        /// </summary>
        public enumResourceType Type { get { return enumResourceType.MapView; } }
        /// <summary>
        /// 为每个资源绑定当前的application
        /// </summary>
        /// <param name="application"></param>
        public void SetHook(ISeanApplication application)
        {
            if (application != null)
                this.mApplication = application;
        }

        #endregion


    }
}
