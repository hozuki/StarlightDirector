namespace StarlightDirector.App.UI.Forms {
    partial class FAbout {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblAbout = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(427, 345);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = false;
            this.lblAbout.BackColor = System.Drawing.SystemColors.Window;
            this.lblAbout.BaseStylesheet = null;
            this.lblAbout.IsContextMenuEnabled = false;
            this.lblAbout.Location = new System.Drawing.Point(10, 10);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(507, 329);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = null;
            // 
            // FAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(529, 380);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel lblAbout;

    }
}