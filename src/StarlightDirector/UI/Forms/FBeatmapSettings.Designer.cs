namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FBeatmapSettings {
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBPM = new System.Windows.Forms.TextBox();
            this.txtMusicOffset = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOneMeasureMore = new System.Windows.Forms.Button();
            this.btnOneMeasureLess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Beats per minute:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Score offset (sec):";
            // 
            // txtBPM
            // 
            this.txtBPM.Location = new System.Drawing.Point(154, 26);
            this.txtBPM.Name = "txtBPM";
            this.txtBPM.Size = new System.Drawing.Size(91, 23);
            this.txtBPM.TabIndex = 2;
            // 
            // txtMusicOffset
            // 
            this.txtMusicOffset.Location = new System.Drawing.Point(154, 58);
            this.txtMusicOffset.Name = "txtMusicOffset";
            this.txtMusicOffset.Size = new System.Drawing.Size(91, 23);
            this.txtMusicOffset.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(99, 103);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(195, 103);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOneMeasureMore
            // 
            this.btnOneMeasureMore.Location = new System.Drawing.Point(251, 58);
            this.btnOneMeasureMore.Name = "btnOneMeasureMore";
            this.btnOneMeasureMore.Size = new System.Drawing.Size(23, 23);
            this.btnOneMeasureMore.TabIndex = 6;
            this.btnOneMeasureMore.Text = "+";
            this.btnOneMeasureMore.UseVisualStyleBackColor = true;
            // 
            // btnOneMeasureLess
            // 
            this.btnOneMeasureLess.Location = new System.Drawing.Point(280, 58);
            this.btnOneMeasureLess.Name = "btnOneMeasureLess";
            this.btnOneMeasureLess.Size = new System.Drawing.Size(23, 23);
            this.btnOneMeasureLess.TabIndex = 7;
            this.btnOneMeasureLess.Text = "-";
            this.btnOneMeasureLess.UseVisualStyleBackColor = true;
            // 
            // FBeatmapSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(323, 139);
            this.Controls.Add(this.btnOneMeasureLess);
            this.Controls.Add(this.btnOneMeasureMore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtMusicOffset);
            this.Controls.Add(this.txtBPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FBeatmapSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Beatmap Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBPM;
        private System.Windows.Forms.TextBox txtMusicOffset;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOneMeasureLess;
        private System.Windows.Forms.Button btnOneMeasureMore;
    }
}