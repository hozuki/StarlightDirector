namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FExportTxt {
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtComposer = new System.Windows.Forms.TextBox();
            this.lblComposer = new System.Windows.Forms.Label();
            this.txtLyricist = new System.Windows.Forms.TextBox();
            this.lblLyricist = new System.Windows.Forms.Label();
            this.txtBgFile = new System.Windows.Forms.TextBox();
            this.lblBgFile = new System.Windows.Forms.Label();
            this.txtSongFile = new System.Windows.Forms.TextBox();
            this.lblSongFile = new System.Windows.Forms.Label();
            this.lblDifficulty = new System.Windows.Forms.Label();
            this.cboDifficulty = new System.Windows.Forms.ComboBox();
            this.txtLevel = new System.Windows.Forms.TextBox();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cboColor = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtBgmVolume = new System.Windows.Forms.TextBox();
            this.lblBgmVolume = new System.Windows.Forms.Label();
            this.txtSeVolume = new System.Windows.Forms.TextBox();
            this.lblSeVolume = new System.Windows.Forms.Label();
            this.cboFormat = new System.Windows.Forms.ComboBox();
            this.lblFormat = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(21, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(41, 12);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(110, 21);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(180, 21);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "(Title)";
            // 
            // txtComposer
            // 
            this.txtComposer.Location = new System.Drawing.Point(110, 47);
            this.txtComposer.Name = "txtComposer";
            this.txtComposer.Size = new System.Drawing.Size(180, 21);
            this.txtComposer.TabIndex = 3;
            this.txtComposer.Text = "(Composer)";
            // 
            // lblComposer
            // 
            this.lblComposer.AutoSize = true;
            this.lblComposer.Location = new System.Drawing.Point(21, 50);
            this.lblComposer.Name = "lblComposer";
            this.lblComposer.Size = new System.Drawing.Size(59, 12);
            this.lblComposer.TabIndex = 2;
            this.lblComposer.Text = "Composer:";
            // 
            // txtLyricist
            // 
            this.txtLyricist.Location = new System.Drawing.Point(110, 74);
            this.txtLyricist.Name = "txtLyricist";
            this.txtLyricist.Size = new System.Drawing.Size(180, 21);
            this.txtLyricist.TabIndex = 5;
            this.txtLyricist.Text = "(Lyricist)";
            // 
            // lblLyricist
            // 
            this.lblLyricist.AutoSize = true;
            this.lblLyricist.Location = new System.Drawing.Point(21, 77);
            this.lblLyricist.Name = "lblLyricist";
            this.lblLyricist.Size = new System.Drawing.Size(59, 12);
            this.lblLyricist.TabIndex = 4;
            this.lblLyricist.Text = "Lyricist:";
            // 
            // txtBgFile
            // 
            this.txtBgFile.Location = new System.Drawing.Point(110, 127);
            this.txtBgFile.Name = "txtBgFile";
            this.txtBgFile.Size = new System.Drawing.Size(180, 21);
            this.txtBgFile.TabIndex = 7;
            this.txtBgFile.Text = "video.mp4";
            // 
            // lblBgFile
            // 
            this.lblBgFile.AutoSize = true;
            this.lblBgFile.Location = new System.Drawing.Point(21, 130);
            this.lblBgFile.Name = "lblBgFile";
            this.lblBgFile.Size = new System.Drawing.Size(83, 12);
            this.lblBgFile.TabIndex = 6;
            this.lblBgFile.Text = "BGI/BGA File:";
            // 
            // txtSongFile
            // 
            this.txtSongFile.Location = new System.Drawing.Point(110, 154);
            this.txtSongFile.Name = "txtSongFile";
            this.txtSongFile.Size = new System.Drawing.Size(180, 21);
            this.txtSongFile.TabIndex = 9;
            this.txtSongFile.Text = "song.ogg";
            // 
            // lblSongFile
            // 
            this.lblSongFile.AutoSize = true;
            this.lblSongFile.Location = new System.Drawing.Point(21, 157);
            this.lblSongFile.Name = "lblSongFile";
            this.lblSongFile.Size = new System.Drawing.Size(65, 12);
            this.lblSongFile.TabIndex = 8;
            this.lblSongFile.Text = "Song File:";
            // 
            // lblDifficulty
            // 
            this.lblDifficulty.AutoSize = true;
            this.lblDifficulty.Location = new System.Drawing.Point(314, 24);
            this.lblDifficulty.Name = "lblDifficulty";
            this.lblDifficulty.Size = new System.Drawing.Size(71, 12);
            this.lblDifficulty.TabIndex = 10;
            this.lblDifficulty.Text = "Difficulty:";
            // 
            // cboDifficulty
            // 
            this.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDifficulty.FormattingEnabled = true;
            this.cboDifficulty.Location = new System.Drawing.Point(399, 21);
            this.cboDifficulty.Name = "cboDifficulty";
            this.cboDifficulty.Size = new System.Drawing.Size(148, 20);
            this.cboDifficulty.TabIndex = 11;
            // 
            // txtLevel
            // 
            this.txtLevel.Location = new System.Drawing.Point(399, 47);
            this.txtLevel.Name = "txtLevel";
            this.txtLevel.Size = new System.Drawing.Size(61, 21);
            this.txtLevel.TabIndex = 13;
            this.txtLevel.Text = "25";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(314, 50);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(41, 12);
            this.lblLevel.TabIndex = 12;
            this.lblLevel.Text = "Level:";
            // 
            // cboColor
            // 
            this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColor.FormattingEnabled = true;
            this.cboColor.Location = new System.Drawing.Point(399, 74);
            this.cboColor.Name = "cboColor";
            this.cboColor.Size = new System.Drawing.Size(148, 20);
            this.cboColor.TabIndex = 15;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(314, 77);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(65, 12);
            this.lblColor.TabIndex = 14;
            this.lblColor.Text = "Attribute:";
            // 
            // txtBgmVolume
            // 
            this.txtBgmVolume.Location = new System.Drawing.Point(399, 100);
            this.txtBgmVolume.Name = "txtBgmVolume";
            this.txtBgmVolume.Size = new System.Drawing.Size(61, 21);
            this.txtBgmVolume.TabIndex = 17;
            this.txtBgmVolume.Text = "100";
            // 
            // lblBgmVolume
            // 
            this.lblBgmVolume.AutoSize = true;
            this.lblBgmVolume.Location = new System.Drawing.Point(314, 103);
            this.lblBgmVolume.Name = "lblBgmVolume";
            this.lblBgmVolume.Size = new System.Drawing.Size(59, 12);
            this.lblBgmVolume.TabIndex = 16;
            this.lblBgmVolume.Text = "BGM Vol.:";
            // 
            // txtSeVolume
            // 
            this.txtSeVolume.Location = new System.Drawing.Point(399, 127);
            this.txtSeVolume.Name = "txtSeVolume";
            this.txtSeVolume.Size = new System.Drawing.Size(61, 21);
            this.txtSeVolume.TabIndex = 19;
            this.txtSeVolume.Text = "100";
            // 
            // lblSeVolume
            // 
            this.lblSeVolume.AutoSize = true;
            this.lblSeVolume.Location = new System.Drawing.Point(314, 130);
            this.lblSeVolume.Name = "lblSeVolume";
            this.lblSeVolume.Size = new System.Drawing.Size(53, 12);
            this.lblSeVolume.TabIndex = 18;
            this.lblSeVolume.Text = "SE Vol.:";
            // 
            // cboFormat
            // 
            this.cboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFormat.Enabled = false;
            this.cboFormat.FormattingEnabled = true;
            this.cboFormat.Location = new System.Drawing.Point(399, 154);
            this.cboFormat.Name = "cboFormat";
            this.cboFormat.Size = new System.Drawing.Size(148, 20);
            this.cboFormat.TabIndex = 21;
            // 
            // lblFormat
            // 
            this.lblFormat.AutoSize = true;
            this.lblFormat.Enabled = false;
            this.lblFormat.Location = new System.Drawing.Point(314, 157);
            this.lblFormat.Name = "lblFormat";
            this.lblFormat.Size = new System.Drawing.Size(47, 12);
            this.lblFormat.TabIndex = 20;
            this.lblFormat.Text = "Format:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(192, 203);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(93, 27);
            this.btnExport.TabIndex = 22;
            this.btnExport.Text = "&Export...";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(292, 203);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 27);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FExportTxt
            // 
            this.AcceptButton = this.btnExport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(575, 248);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cboFormat);
            this.Controls.Add(this.lblFormat);
            this.Controls.Add(this.txtSeVolume);
            this.Controls.Add(this.lblSeVolume);
            this.Controls.Add(this.txtBgmVolume);
            this.Controls.Add(this.lblBgmVolume);
            this.Controls.Add(this.cboColor);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.txtLevel);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.cboDifficulty);
            this.Controls.Add(this.lblDifficulty);
            this.Controls.Add(this.txtSongFile);
            this.Controls.Add(this.lblSongFile);
            this.Controls.Add(this.txtBgFile);
            this.Controls.Add(this.lblBgFile);
            this.Controls.Add(this.txtLyricist);
            this.Controls.Add(this.lblLyricist);
            this.Controls.Add(this.txtComposer);
            this.Controls.Add(this.lblComposer);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FExportTxt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export to Deleste TXT Beatmap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtComposer;
        private System.Windows.Forms.Label lblComposer;
        private System.Windows.Forms.TextBox txtLyricist;
        private System.Windows.Forms.Label lblLyricist;
        private System.Windows.Forms.TextBox txtBgFile;
        private System.Windows.Forms.Label lblBgFile;
        private System.Windows.Forms.TextBox txtSongFile;
        private System.Windows.Forms.Label lblSongFile;
        private System.Windows.Forms.Label lblDifficulty;
        private System.Windows.Forms.ComboBox cboDifficulty;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.ComboBox cboColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.TextBox txtBgmVolume;
        private System.Windows.Forms.Label lblBgmVolume;
        private System.Windows.Forms.TextBox txtSeVolume;
        private System.Windows.Forms.Label lblSeVolume;
        private System.Windows.Forms.ComboBox cboFormat;
        private System.Windows.Forms.Label lblFormat;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
