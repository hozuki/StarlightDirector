namespace OpenCGSS.StarlightDirector.UI.Forms {
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
            this.tabPages = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnSelectExternalPreviewerFile = new System.Windows.Forms.Button();
            this.txtExternalPreviewerArgs = new System.Windows.Forms.TextBox();
            this.txtExternalPreviewerFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPreviewSpeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPreviewRenderMode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScrollingSpeed)).BeginInit();
            this.tabPages.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radInvertedScrollingOn);
            this.panel1.Controls.Add(this.radInvertedScrollingOff);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
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
            this.chkShowNoteIndicators.Location = new System.Drawing.Point(126, 104);
            this.chkShowNoteIndicators.Name = "chkShowNoteIndicators";
            this.chkShowNoteIndicators.Size = new System.Drawing.Size(149, 21);
            this.chkShowNoteIndicators.TabIndex = 1;
            this.chkShowNoteIndicators.Text = "Show note indicators";
            this.chkShowNoteIndicators.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(165, 184);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(261, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Scrolling speed:";
            // 
            // txtScrollingSpeed
            // 
            this.txtScrollingSpeed.Location = new System.Drawing.Point(126, 75);
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
            this.label3.Location = new System.Drawing.Point(19, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Language:";
            // 
            // cboLanguage
            // 
            this.cboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLanguage.FormattingEnabled = true;
            this.cboLanguage.Location = new System.Drawing.Point(126, 13);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Size = new System.Drawing.Size(148, 25);
            this.cboLanguage.TabIndex = 7;
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.tabPage1);
            this.tabPages.Controls.Add(this.tabPage2);
            this.tabPages.Controls.Add(this.tabPage3);
            this.tabPages.Location = new System.Drawing.Point(12, 12);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(339, 166);
            this.tabPages.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.chkShowNoteIndicators);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtScrollingSpeed);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(331, 136);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Editor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnSelectExternalPreviewerFile);
            this.tabPage2.Controls.Add(this.txtExternalPreviewerArgs);
            this.tabPage2.Controls.Add(this.txtExternalPreviewerFile);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txtPreviewSpeed);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.cboPreviewRenderMode);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(331, 136);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Preview";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnSelectExternalPreviewerFile
            // 
            this.btnSelectExternalPreviewerFile.Location = new System.Drawing.Point(195, 102);
            this.btnSelectExternalPreviewerFile.Name = "btnSelectExternalPreviewerFile";
            this.btnSelectExternalPreviewerFile.Size = new System.Drawing.Size(32, 25);
            this.btnSelectExternalPreviewerFile.TabIndex = 11;
            this.btnSelectExternalPreviewerFile.Text = "...";
            this.btnSelectExternalPreviewerFile.UseVisualStyleBackColor = true;
            // 
            // txtExternalPreviewerArgs
            // 
            this.txtExternalPreviewerArgs.Location = new System.Drawing.Point(233, 103);
            this.txtExternalPreviewerArgs.Name = "txtExternalPreviewerArgs";
            this.txtExternalPreviewerArgs.Size = new System.Drawing.Size(76, 23);
            this.txtExternalPreviewerArgs.TabIndex = 10;
            this.txtExternalPreviewerArgs.Text = "--editor_port %port%";
            // 
            // txtExternalPreviewerFile
            // 
            this.txtExternalPreviewerFile.Location = new System.Drawing.Point(21, 103);
            this.txtExternalPreviewerFile.Name = "txtExternalPreviewerFile";
            this.txtExternalPreviewerFile.ReadOnly = true;
            this.txtExternalPreviewerFile.Size = new System.Drawing.Size(168, 23);
            this.txtExternalPreviewerFile.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "External Previewer:";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(277, 45);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 25);
            this.button4.TabIndex = 7;
            this.button4.Text = "+1";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(126, 44);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(32, 25);
            this.button3.TabIndex = 6;
            this.button3.Text = "-1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(164, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 25);
            this.button2.TabIndex = 5;
            this.button2.Text = "-";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(246, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 4;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtPreviewSpeed
            // 
            this.txtPreviewSpeed.Enabled = false;
            this.txtPreviewSpeed.Location = new System.Drawing.Point(195, 46);
            this.txtPreviewSpeed.Name = "txtPreviewSpeed";
            this.txtPreviewSpeed.Size = new System.Drawing.Size(45, 23);
            this.txtPreviewSpeed.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(18, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Speed:";
            // 
            // cboPreviewRenderMode
            // 
            this.cboPreviewRenderMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPreviewRenderMode.FormattingEnabled = true;
            this.cboPreviewRenderMode.Location = new System.Drawing.Point(126, 13);
            this.cboPreviewRenderMode.Name = "cboPreviewRenderMode";
            this.cboPreviewRenderMode.Size = new System.Drawing.Size(126, 25);
            this.cboPreviewRenderMode.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Render Mode:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.cboLanguage);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(331, 136);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FEditorSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(370, 224);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
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
            this.tabPages.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPreviewRenderMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPreviewSpeed;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtExternalPreviewerFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectExternalPreviewerFile;
        private System.Windows.Forms.TextBox txtExternalPreviewerArgs;
        private System.Windows.Forms.OpenFileDialog ofd;
    }
}