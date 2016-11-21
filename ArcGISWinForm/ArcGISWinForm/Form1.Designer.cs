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
            this.custom_FontFamilyCombobox1 = new SeanShen.CustomControls.Custom_FontFamilyCombobox();
            this.mixedCheckBoxesTreeView1 = new SeanShen.CustomControls.Custom_MixedCheckBoxesTreeView();
            this.ucColorRampSelector1 = new SeanShen.CustomControls.ucColorRampSelector();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(763, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 164);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(519, 359);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(282, 218);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 4;
            // 
            // custom_FontFamilyCombobox1
            // 
            this.custom_FontFamilyCombobox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.custom_FontFamilyCombobox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custom_FontFamilyCombobox1.FormattingEnabled = true;
            this.custom_FontFamilyCombobox1.IntegralHeight = false;
            this.custom_FontFamilyCombobox1.Location = new System.Drawing.Point(549, 396);
            this.custom_FontFamilyCombobox1.MaxDropDownItems = 20;
            this.custom_FontFamilyCombobox1.Name = "custom_FontFamilyCombobox1";
            this.custom_FontFamilyCombobox1.Size = new System.Drawing.Size(185, 22);
            this.custom_FontFamilyCombobox1.TabIndex = 7;
            // 
            // mixedCheckBoxesTreeView1
            // 
            this.mixedCheckBoxesTreeView1.Location = new System.Drawing.Point(549, 293);
            this.mixedCheckBoxesTreeView1.Name = "mixedCheckBoxesTreeView1";
            this.mixedCheckBoxesTreeView1.Size = new System.Drawing.Size(121, 97);
            this.mixedCheckBoxesTreeView1.TabIndex = 6;
            // 
            // ucColorRampSelector1
            // 
            this.ucColorRampSelector1.Custom_ColorRampCBB = null;
            this.ucColorRampSelector1.Location = new System.Drawing.Point(549, 265);
            this.ucColorRampSelector1.Name = "ucColorRampSelector1";
            this.ucColorRampSelector1.Size = new System.Drawing.Size(152, 22);
            this.ucColorRampSelector1.TabIndex = 5;
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.EditValue = "测试button";
            this.buttonEdit1.Location = new System.Drawing.Point(537, 162);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(100, 20);
            this.buttonEdit1.TabIndex = 8;
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 576);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(949, 27);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 1;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(949, 145);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 603);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.buttonEdit1);
            this.Controls.Add(this.custom_FontFamilyCombobox1);
            this.Controls.Add(this.mixedCheckBoxesTreeView1);
            this.Controls.Add(this.ucColorRampSelector1);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private SeanShen.CustomControls.ucColorRampSelector ucColorRampSelector1;
        private SeanShen.CustomControls.Custom_MixedCheckBoxesTreeView mixedCheckBoxesTreeView1;
        private SeanShen.CustomControls.Custom_FontFamilyCombobox custom_FontFamilyCombobox1;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    }
}

