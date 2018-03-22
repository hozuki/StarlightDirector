namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FGoTo {
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
            this.radGoToMeasure = new System.Windows.Forms.RadioButton();
            this.txtTargetMeasure = new System.Windows.Forms.NumericUpDown();
            this.radGoToTime = new System.Windows.Forms.RadioButton();
            this.txtTargetTime = new System.Windows.Forms.TextBox();
            this.lblTotalMeasures = new System.Windows.Forms.Label();
            this.lblTotalSeconds = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetMeasure)).BeginInit();
            this.SuspendLayout();
            // 
            // radGoToMeasure
            // 
            this.radGoToMeasure.AutoSize = true;
            this.radGoToMeasure.Checked = true;
            this.radGoToMeasure.Location = new System.Drawing.Point(22, 23);
            this.radGoToMeasure.Name = "radGoToMeasure";
            this.radGoToMeasure.Size = new System.Drawing.Size(80, 21);
            this.radGoToMeasure.TabIndex = 0;
            this.radGoToMeasure.TabStop = true;
            this.radGoToMeasure.Text = "&Measure:";
            this.radGoToMeasure.UseVisualStyleBackColor = true;
            // 
            // txtTargetMeasure
            // 
            this.txtTargetMeasure.Location = new System.Drawing.Point(108, 23);
            this.txtTargetMeasure.Name = "txtTargetMeasure";
            this.txtTargetMeasure.Size = new System.Drawing.Size(75, 23);
            this.txtTargetMeasure.TabIndex = 1;
            // 
            // radGoToTime
            // 
            this.radGoToTime.AutoSize = true;
            this.radGoToTime.Location = new System.Drawing.Point(22, 50);
            this.radGoToTime.Name = "radGoToTime";
            this.radGoToTime.Size = new System.Drawing.Size(88, 21);
            this.radGoToTime.TabIndex = 2;
            this.radGoToTime.Text = "&Time (sec):";
            this.radGoToTime.UseVisualStyleBackColor = true;
            // 
            // txtTargetTime
            // 
            this.txtTargetTime.Location = new System.Drawing.Point(108, 49);
            this.txtTargetTime.Name = "txtTargetTime";
            this.txtTargetTime.Size = new System.Drawing.Size(75, 23);
            this.txtTargetTime.TabIndex = 3;
            // 
            // lblTotalMeasures
            // 
            this.lblTotalMeasures.AutoSize = true;
            this.lblTotalMeasures.Location = new System.Drawing.Point(189, 25);
            this.lblTotalMeasures.Name = "lblTotalMeasures";
            this.lblTotalMeasures.Size = new System.Drawing.Size(34, 17);
            this.lblTotalMeasures.TabIndex = 4;
            this.lblTotalMeasures.Text = "/100";
            // 
            // lblTotalSeconds
            // 
            this.lblTotalSeconds.AutoSize = true;
            this.lblTotalSeconds.Location = new System.Drawing.Point(105, 75);
            this.lblTotalSeconds.Name = "lblTotalSeconds";
            this.lblTotalSeconds.Size = new System.Drawing.Size(116, 17);
            this.lblTotalSeconds.TabIndex = 5;
            this.lblTotalSeconds.Text = "-999.999 - 999.999";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(70, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(166, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FGoTo
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(268, 145);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblTotalSeconds);
            this.Controls.Add(this.lblTotalMeasures);
            this.Controls.Add(this.txtTargetTime);
            this.Controls.Add(this.radGoToTime);
            this.Controls.Add(this.txtTargetMeasure);
            this.Controls.Add(this.radGoToMeasure);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FGoTo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Go To";
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetMeasure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radGoToMeasure;
        private System.Windows.Forms.NumericUpDown txtTargetMeasure;
        private System.Windows.Forms.RadioButton radGoToTime;
        private System.Windows.Forms.TextBox txtTargetTime;
        private System.Windows.Forms.Label lblTotalMeasures;
        private System.Windows.Forms.Label lblTotalSeconds;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}