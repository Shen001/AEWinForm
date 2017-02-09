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
    Description: Toc
    Modify:
    */
    using SeanShen.Framework;
    using ESRI.ArcGIS.Controls;
    public partial class ucTOCDockWindow : UserControl, ISeanTocWindow
    {
        private ISeanApplication mApplication = null;
        public ucTOCDockWindow()
        {
            InitializeComponent();
            this.axTOCControl1.Dock = DockStyle.Fill;
        }

        # region ISeanTOCWindow
        public ITOCControlDefault GetITOCControl()
        {
            ITOCControlDefault tocControl = this.axTOCControl1.Object as ITOCControlDefault;
            return tocControl;
        }

        public AxTOCControl GetAxTOCControl()
        {
            return this.axTOCControl1;
        }
        #endregion

        #region IDockWindow
        public Control GetControl()
        {
            return this;
        }

        public new void Show()
        {
            this.Parent.Show();//parent是个panel
        }

        public new void Hide()
        {
            this.Parent.Hide();
        }

        #endregion

        #region ISeanResource

        public Guid UID { get { return Guid.Parse("61985D90-DAE9-4F27-AB35-DA515B3BFDE2"); } }

        string ISeanResource.Name { get { return "ucTOCDockWindow"; } }

        public string Caption { get { return "图层视图"; } }

        public enumResourceType Type { get { return enumResourceType.TocWindow; } }

        public void SetHook(ISeanApplication application)
        {
            if (application != null)
                this.mApplication = application;
        }
        #endregion
    }

}
