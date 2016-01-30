namespace SeanShen.CustomControls
{
    partial class ucColorRampSelector
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucColorRampSelector));
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axSymbologyControl1 = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.custom_ColorRampCombobox = new SeanShen.CustomControls.Custom_ColorRampCombobox();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(119, 9);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 2;
            // 
            // axSymbologyControl1
            // 
            this.axSymbologyControl1.Location = new System.Drawing.Point(104, 3);
            this.axSymbologyControl1.Name = "axSymbologyControl1";
            this.axSymbologyControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSymbologyControl1.OcxState")));
            this.axSymbologyControl1.Size = new System.Drawing.Size(25, 23);
            this.axSymbologyControl1.TabIndex = 3;
            // 
            // custom_ColorRampCombobox
            // 
            this.custom_ColorRampCombobox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.custom_ColorRampCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.custom_ColorRampCombobox.FormattingEnabled = true;
            this.custom_ColorRampCombobox.Location = new System.Drawing.Point(22, -3);
            this.custom_ColorRampCombobox.MaxDropDownItems = 10;
            this.custom_ColorRampCombobox.Name = "custom_ColorRampCombobox";
            this.custom_ColorRampCombobox.SelectSymbol = null;
            this.custom_ColorRampCombobox.Size = new System.Drawing.Size(91, 22);
            this.custom_ColorRampCombobox.SymbologyStyleClass = null;
            this.custom_ColorRampCombobox.TabIndex = 4;
            // 
            // ucColorRampSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.custom_ColorRampCombobox);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.axSymbologyControl1);
            this.Name = "ucColorRampSelector";
            this.Size = new System.Drawing.Size(152, 22);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axSymbologyControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxSymbologyControl axSymbologyControl1;
        private Custom_ColorRampCombobox custom_ColorRampCombobox;
    }
}
