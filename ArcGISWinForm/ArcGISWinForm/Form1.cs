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


using SeanShen.AEUtilities;
namespace ArcGISWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button1.Click += new EventHandler(button1_Click);
            this.richTextBox1.TextChanged += new EventHandler(richTextBox1_TextChanged);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InitialDGVColumn();
        }

        void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //让文本框获取焦点
            this.richTextBox1.Focus();
            //设置光标的位置到文本尾
            this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
            //滚动到控件光标处
            this.richTextBox1.ScrollToCaret();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //this.dataGridView1.Rows
            
        }
        private void InitialDGVColumn()
        {
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("11111111111111111");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("22222222222222222");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("33333333333333333"); 
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("444444444444444444");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("555555555555555555");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("666666666666666666");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("777777777777777777");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("888888888888888888"); 
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("999999999999999999"); 
            this.richTextBox1.AppendText(Environment.NewLine);

            this.richTextBox1.AppendText("111111111111111111111111****************************");
            this.richTextBox1.AppendText(Environment.NewLine);
            this.richTextBox1.AppendText("2222222222222222222222222222222****************************");
        }
    }
}
