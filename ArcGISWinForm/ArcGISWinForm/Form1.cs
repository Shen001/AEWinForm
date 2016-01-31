using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;



using System.Configuration;
namespace ArcGISWinForm
{
    using SeanShen.AEUtilities;
    using SeanShen.CustomControls;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load+=new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.mixedCheckBoxesTreeView1.CheckBoxes = true;
            //Custom_HiddenCheckBoxTreeNode hideNode = new Custom_HiddenCheckBoxTreeNode("隐藏checkbox");
            //this.mixedCheckBoxesTreeView1.Nodes.Add(hideNode);
            //TreeNode node = new TreeNode("不隐藏checkbox");
            //this.mixedCheckBoxesTreeView1.Nodes[0].Nodes.Add(node);
            //还可以改进一个鼠标悬浮显示样式或者封装为控件
            this.custom_FontFamilyCombobox1.Populate(false);
            
        }
    }
}
