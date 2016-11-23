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
    Modify:
    */

    using Framework;
    using ESRI.ArcGIS.Controls;
    public partial class ucMapView : UserControl, ISeanMapControlView
    {
        private ISeanApplication mApplication = null;
        public ucMapView()
        {
            InitializeComponent();
            axMapControl1.Dock = DockStyle.Fill;
        }

        public ESRI.ArcGIS.Controls.IMapControlDefault GetMapControl()
        {
            IMapControlDefault mapcontrol = this.axMapControl1.Object as IMapControlDefault;
            return mapcontrol;
        }

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
        string ISeanResource.Name { get { return "地图视图"; } }
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
