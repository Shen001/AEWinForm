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
    Time: 28/11/2016 15:59  周一
    Author: shenxin
    Description: 布局视图
    Modify:
 */
    using SeanShen.Framework;
    using ESRI.ArcGIS.Controls;
    using ESRI.ArcGIS.Display;
    using ESRI.ArcGIS.Carto;
    public partial class ucPagelayoutView : UserControl,ISeanPagelayoutView
    {
        private ISeanApplication mApplication;

        public ucPagelayoutView()
        {
            InitializeComponent();
            this.axPageLayoutControl1.Dock = DockStyle.Fill;

            this.axPageLayoutControl1.OnMouseMove += new IPageLayoutControlEvents_Ax_OnMouseMoveEventHandler(axPageLayoutControl1_OnMouseMove);
        }

        void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
        {
            this.mApplication.StatusBar.ShowPagelayoutCoordinate(e.pageX, e.pageY);
        }

        #region ISeanPagelayoutView成员
        public ESRI.ArcGIS.Controls.IPageLayoutControlDefault GetIPageLayoutControl()
        {
            IPageLayoutControlDefault pagelayout = this.axPageLayoutControl1.Object as IPageLayoutControlDefault;
            return pagelayout;
        }

        public ESRI.ArcGIS.Controls.AxPageLayoutControl GetAxPagelayouControl()
        {
            return this.axPageLayoutControl1;
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
        public Guid UID { get { return Guid.Parse("90A2966E-9039-4647-8628-2E9780150255"); } }
        //资源名称
        string ISeanResource.Name { get { return "ucPagelayoutView"; } }

        public string Caption { get { return "布局视图"; } }
        /// <summary>
        /// 资源类型
        /// </summary>
        public enumResourceType Type { get { return enumResourceType.PageLayoutView; } }
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
