namespace StarlightDirector.App.UI.Forms {
    partial class FEditorSettings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.radInvertedScrollingOn = new System.Windows.Forms.RadioButton();
            this.radInvertedScrollingOff = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowNoteIndicators = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtScrollingSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLanguage = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScrollingSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radInvertedScrollingOn);
            this.panel1.Controls.Add(this.radInvertedScrollingOff);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(286, 63);
            this.panel1.TabIndex = 0;
            // 
            // radInvertedScrollingOn
            // 
            this.radInvertedScrollingOn.AutoSize = true;
            this.radInvertedScrollingOn.Location = new System.Drawing.Point(120, 34);
            this.radInvertedScrollingOn.Name = "radInvertedScrollingOn";
            this.radInvertedScrollingOn.Size = new System.Drawing.Size(94, 21);
            this.radInvertedScrollingOn.TabIndex = 3;
            this.radInvertedScrollingOn.Text = "Like macOS";
            this.radInvertedScrollingOn.UseVisualStyleBackColor = true;
            // 
            // radInvertedScrollingOff
            // 
            this.radInvertedScrollingOff.AutoSize = true;
            this.radInvertedScrollingOff.Checked = true;
            this.radInvertedScrollingOff.Location = new System.Drawing.Point(120, 8);
            this.radInvertedScrollingOff.Name = "radInvertedScrollingOff";
            this.radInvertedScrollingOff.Size = new System.Drawing.Size(148, 21);
            this.radInvertedScrollingOff.TabIndex = 2;
            this.radInvertedScrollingOff.TabStop = true;
            this.radInvertedScrollingOff.Text = "Like Windows / Linux";
            this.radInvertedScrollingOff.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scrolling style:";
            // 
            // chkShowNoteIndicators
            // 
            this.chkShowNoteIndicators.AutoSize = true;
            this.chkShowNoteIndicators.Location = new System.Drawing.Point(132, 110);
            this.chkShowNoteIndicators.Name = "chkShowNoteIndicators";
            this.chkShowNoteIndicators.Size = new System.Drawing.Size(149, 21);
            this.chkShowNoteIndicators.TabIndex = 1;
            this.chkShowNoteIndicators.Text = "Show note indicators";
            this.chkShowNoteIndicators.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(112, 189);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(208, 189);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Scrolling speed:";
            // 
            // txtScrollingSpeed
            // 
            this.txtScrollingSpeed.Location = new System.Drawing.Point(132, 81);
            this.txtScrollingSpeed.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtScrollingSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtScrollingSpeed.Name = "txtScrollingSpeed";
            this.txtScrollingSpeed.Size = new System.Drawing.Size(70, 23);
            this.txtScrollingSpeed.TabIndex = 5;
            this.txtScrollingSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Language:";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(132, 138);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(148, 25);
            this.cboLanguage.TabIndex = 7;
            // 
            // FEditorSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(310, 224);
            this.Controls.Add(this.cboLanguage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtScrollingSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkShowNoteIndicators);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FEditorSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScrollingSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radInvertedScrollingOn;
        private System.Windows.Forms.RadioButton radInvertedScrollingOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowNoteIndicators;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtScrollingSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLanguage;
    }
}