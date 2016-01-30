using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using ESRI.ArcGIS.Controls;

namespace SeanShen.CustomControls
{
    public partial class ucColorRampSelector : UserControl
    {
        public ucColorRampSelector()
        {
            InitializeComponent();
            this.custom_ColorRampCombobox.Dock = DockStyle.Fill;
            this.Load += new EventHandler(ucColorRampSelector_Load);
        }
        private Custom_ColorRampCombobox custom_ColorRampCBB;
        public Custom_ColorRampCombobox Custom_ColorRampCBB
        {
            get
            {
                return this.custom_ColorRampCBB;
            }
            set
            {
                this.custom_ColorRampCBB = value;
            }
        }
        void ucColorRampSelector_Load(object sender, EventArgs e)
        {
            this.axSymbologyControl1.Visible = false;

            string stylePath = ConfigurationManager.AppSettings["ESRI.ServerStyle"];
            //颜色带
            this.axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
            this.axSymbologyControl1.LoadStyleFile(stylePath);
            this.custom_ColorRampCombobox.SymbologyStyleClass = this.axSymbologyControl1.GetStyleClass(this.axSymbologyControl1.StyleClass);

            this.custom_ColorRampCBB = this.custom_ColorRampCombobox;
        }
    }
}
