namespace ArcGISWinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.ucColorRampSelector1 = new SeanShen.CustomControls.ucColorRampSelector();
            this.mixedCheckBoxesTreeView1 = new SeanShen.CustomControls.MixedCheckBoxesTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(756, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(40, 31);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(484, 287);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(275, 13);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // ucColorRampSelector1
            // 
            this.ucColorRampSelector1.Custom_ColorRampCBB = null;
            this.ucColorRampSelector1.Location = new System.Drawing.Point(573, 50);
            this.ucColorRampSelector1.Name = "ucColorRampSelector1";
            this.ucColorRampSelector1.Size = new System.Drawing.Size(152, 22);
            this.ucColorRampSelector1.TabIndex = 5;
            // 
            // mixedCheckBoxesTreeView1
            // 
            this.mixedCheckBoxesTreeView1.Location = new System.Drawing.Point(573, 89);
            this.mixedCheckBoxesTreeView1.Name = "mixedCheckBoxesTreeView1";
            this.mixedCheckBoxesTreeView1.Size = new System.Drawing.Size(152, 144);
            this.mixedCheckBoxesTreeView1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 425);
            this.Controls.Add(this.mixedCheckBoxesTreeView1);
            this.Controls.Add(this.ucColorRampSelector1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private SeanShen.CustomControls.ucColorRampSelector ucColorRampSelector1;
        private SeanShen.CustomControls.MixedCheckBoxesTreeView mixedCheckBoxesTreeView1;
    }
}

