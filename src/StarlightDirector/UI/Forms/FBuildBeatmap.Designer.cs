namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FBuildBeatmap {
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
            this.lblEstimatedDuration = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCustomEndTime = new System.Windows.Forms.TextBox();
            this.radEndTimeCustom = new System.Windows.Forms.RadioButton();
            this.radEndTimeAuto = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.radBuildCsv = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboDurationDifficulty = new System.Windows.Forms.ComboBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cboCsvDifficulty = new System.Windows.Forms.ComboBox();
            this.radBuildBdb = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboDiffculty1 = new System.Windows.Forms.ComboBox();
            this.cboDiffculty2 = new System.Windows.Forms.ComboBox();
            this.cboDiffculty3 = new System.Windows.Forms.ComboBox();
            this.cboDiffculty4 = new System.Windows.Forms.ComboBox();
            this.cboDiffculty5 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLiveID = new System.Windows.Forms.Label();
            this.btnSelectLiveID = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboDurationDifficulty);
            this.panel1.Controls.Add(this.lblEstimatedDuration);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCustomEndTime);
            this.panel1.Controls.Add(this.radEndTimeCustom);
            this.panel1.Controls.Add(this.radEndTimeAuto);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 97);
            this.panel1.TabIndex = 1;
            // 
            // lblEstimatedDuration
            // 
            this.lblEstimatedDuration.AutoSize = true;
            this.lblEstimatedDuration.Location = new System.Drawing.Point(175, 65);
            this.lblEstimatedDuration.Name = "lblEstimatedDuration";
            this.lblEstimatedDuration.Size = new System.Drawing.Size(53, 17);
            this.lblEstimatedDuration.TabIndex = 8;
            this.lblEstimatedDuration.Text = "999.999";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Estimated:";
            // 
            // txtCustomEndTime
            // 
            this.txtCustomEndTime.Enabled = false;
            this.txtCustomEndTime.Location = new System.Drawing.Point(178, 28);
            this.txtCustomEndTime.Name = "txtCustomEndTime";
            this.txtCustomEndTime.Size = new System.Drawing.Size(75, 23);
            this.txtCustomEndTime.TabIndex = 6;
            // 
            // radEndTimeCustom
            // 
            this.radEndTimeCustom.AutoSize = true;
            this.radEndTimeCustom.Location = new System.Drawing.Point(102, 29);
            this.radEndTimeCustom.Name = "radEndTimeCustom";
            this.radEndTimeCustom.Size = new System.Drawing.Size(70, 21);
            this.radEndTimeCustom.TabIndex = 5;
            this.radEndTimeCustom.Text = "&Custom";
            this.radEndTimeCustom.UseVisualStyleBackColor = true;
            // 
            // radEndTimeAuto
            // 
            this.radEndTimeAuto.AutoSize = true;
            this.radEndTimeAuto.Checked = true;
            this.radEndTimeAuto.Location = new System.Drawing.Point(102, 7);
            this.radEndTimeAuto.Name = "radEndTimeAuto";
            this.radEndTimeAuto.Size = new System.Drawing.Size(53, 21);
            this.radEndTimeAuto.TabIndex = 4;
            this.radEndTimeAuto.TabStop = true;
            this.radEndTimeAuto.Text = "&Auto";
            this.radEndTimeAuto.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "End time (sec):";
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(120, 414);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(93, 27);
            this.btnBuild.TabIndex = 11;
            this.btnBuild.Text = "&Build...";
            this.btnBuild.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnSelectLiveID);
            this.panel2.Controls.Add(this.lblLiveID);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cboCsvDifficulty);
            this.panel2.Controls.Add(this.cboDiffculty5);
            this.panel2.Controls.Add(this.cboDiffculty4);
            this.panel2.Controls.Add(this.cboDiffculty3);
            this.panel2.Controls.Add(this.cboDiffculty2);
            this.panel2.Controls.Add(this.cboDiffculty1);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.radBuildBdb);
            this.panel2.Controls.Add(this.radBuildCsv);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Location = new System.Drawing.Point(12, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(387, 293);
            this.panel2.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "Build type:";
            // 
            // radBuildCsv
            // 
            this.radBuildCsv.AutoSize = true;
            this.radBuildCsv.Checked = true;
            this.radBuildCsv.Location = new System.Drawing.Point(102, 3);
            this.radBuildCsv.Name = "radBuildCsv";
            this.radBuildCsv.Size = new System.Drawing.Size(152, 21);
            this.radBuildCsv.TabIndex = 1;
            this.radBuildCsv.TabStop = true;
            this.radBuildCsv.Text = "Single beatmap (CSV)";
            this.radBuildCsv.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(219, 414);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 27);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // cboDurationDifficulty
            // 
            this.cboDurationDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDurationDifficulty.FormattingEnabled = true;
            this.cboDurationDifficulty.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDurationDifficulty.Location = new System.Drawing.Point(234, 62);
            this.cboDurationDifficulty.Name = "cboDurationDifficulty";
            this.cboDurationDifficulty.Size = new System.Drawing.Size(103, 25);
            this.cboDurationDifficulty.TabIndex = 26;
            // 
            // cboCsvDifficulty
            // 
            this.cboCsvDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCsvDifficulty.FormattingEnabled = true;
            this.cboCsvDifficulty.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboCsvDifficulty.Location = new System.Drawing.Point(207, 30);
            this.cboCsvDifficulty.Name = "cboCsvDifficulty";
            this.cboCsvDifficulty.Size = new System.Drawing.Size(103, 25);
            this.cboCsvDifficulty.TabIndex = 36;
            // 
            // radBuildBdb
            // 
            this.radBuildBdb.AutoSize = true;
            this.radBuildBdb.Location = new System.Drawing.Point(102, 60);
            this.radBuildBdb.Name = "radBuildBdb";
            this.radBuildBdb.Size = new System.Drawing.Size(159, 21);
            this.radBuildBdb.TabIndex = 2;
            this.radBuildBdb.Text = "Beatmap bundle (BDB)";
            this.radBuildBdb.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Difficulty mapping:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Debut";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(88, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Regular";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(88, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 17);
            this.label8.TabIndex = 28;
            this.label8.Text = "Pro";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(88, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 17);
            this.label9.TabIndex = 29;
            this.label9.Text = "Master";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(88, 259);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Master+";
            // 
            // cboDiffculty1
            // 
            this.cboDiffculty1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiffculty1.FormattingEnabled = true;
            this.cboDiffculty1.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDiffculty1.Location = new System.Drawing.Point(207, 132);
            this.cboDiffculty1.Name = "cboDiffculty1";
            this.cboDiffculty1.Size = new System.Drawing.Size(103, 25);
            this.cboDiffculty1.TabIndex = 31;
            // 
            // cboDiffculty2
            // 
            this.cboDiffculty2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiffculty2.FormattingEnabled = true;
            this.cboDiffculty2.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDiffculty2.Location = new System.Drawing.Point(207, 163);
            this.cboDiffculty2.Name = "cboDiffculty2";
            this.cboDiffculty2.Size = new System.Drawing.Size(103, 25);
            this.cboDiffculty2.TabIndex = 32;
            // 
            // cboDiffculty3
            // 
            this.cboDiffculty3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiffculty3.FormattingEnabled = true;
            this.cboDiffculty3.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDiffculty3.Location = new System.Drawing.Point(207, 194);
            this.cboDiffculty3.Name = "cboDiffculty3";
            this.cboDiffculty3.Size = new System.Drawing.Size(103, 25);
            this.cboDiffculty3.TabIndex = 33;
            // 
            // cboDiffculty4
            // 
            this.cboDiffculty4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiffculty4.FormattingEnabled = true;
            this.cboDiffculty4.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDiffculty4.Location = new System.Drawing.Point(207, 225);
            this.cboDiffculty4.Name = "cboDiffculty4";
            this.cboDiffculty4.Size = new System.Drawing.Size(103, 25);
            this.cboDiffculty4.TabIndex = 34;
            // 
            // cboDiffculty5
            // 
            this.cboDiffculty5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiffculty5.FormattingEnabled = true;
            this.cboDiffculty5.Items.AddRange(new object[] {
            "Debut",
            "Regular",
            "Pro",
            "Master",
            "Master+"});
            this.cboDiffculty5.Location = new System.Drawing.Point(207, 256);
            this.cboDiffculty5.Name = "cboDiffculty5";
            this.cboDiffculty5.Size = new System.Drawing.Size(103, 25);
            this.cboDiffculty5.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 37;
            this.label3.Text = "Live ID:";
            // 
            // lblLiveID
            // 
            this.lblLiveID.AutoSize = true;
            this.lblLiveID.Location = new System.Drawing.Point(202, 84);
            this.lblLiveID.Name = "lblLiveID";
            this.lblLiveID.Size = new System.Drawing.Size(29, 17);
            this.lblLiveID.TabIndex = 38;
            this.lblLiveID.Text = "001";
            // 
            // btnSelectLiveID
            // 
            this.btnSelectLiveID.Location = new System.Drawing.Point(267, 79);
            this.btnSelectLiveID.Name = "btnSelectLiveID";
            this.btnSelectLiveID.Size = new System.Drawing.Size(80, 27);
            this.btnSelectLiveID.TabIndex = 26;
            this.btnSelectLiveID.Text = "&Select...";
            this.btnSelectLiveID.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(88, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 39;
            this.label5.Text = "Difficulty:";
            // 
            // FBuildBeatmap
            // 
            this.AcceptButton = this.btnBuild;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(411, 453);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FBuildBeatmap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build Beatmap";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radEndTimeCustom;
        private System.Windows.Forms.RadioButton radEndTimeAuto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCustomEndTime;
        private System.Windows.Forms.Label lblEstimatedDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radBuildCsv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboDurationDifficulty;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ComboBox cboCsvDifficulty;
        private System.Windows.Forms.Button btnSelectLiveID;
        private System.Windows.Forms.Label lblLiveID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDiffculty5;
        private System.Windows.Forms.ComboBox cboDiffculty4;
        private System.Windows.Forms.ComboBox cboDiffculty3;
        private System.Windows.Forms.ComboBox cboDiffculty2;
        private System.Windows.Forms.ComboBox cboDiffculty1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radBuildBdb;
        private System.Windows.Forms.Label label5;
    }
}