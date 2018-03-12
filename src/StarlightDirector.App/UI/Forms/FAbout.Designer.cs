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
            this.lblAbout = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.label1 = new System.Windows.Forms.LinkLabel();
            this.picAnimation = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(208, 345);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblAbout
            // 
            this.lblAbout.AutoScroll = true;
            this.lblAbout.BackColor = System.Drawing.SystemColors.Window;
            this.lblAbout.BaseStylesheet = null;
            this.lblAbout.IsContextMenuEnabled = false;
            this.lblAbout.Location = new System.Drawing.Point(10, 10);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(553, 329);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = null;
            this.lblAbout.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.lblAbout.UseGdiPlusTextRendering = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.LinkArea = new System.Windows.Forms.LinkArea(9, 58);
            this.label1.Location = new System.Drawing.Point(130, 286);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 21);
            this.label1.TabIndex = 7;
            this.label1.TabStop = true;
            this.label1.Text = "GIF from http://deremas.doorblog.jp/archives/33091887.html";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // picAnimation
            // 
            this.picAnimation.Location = new System.Drawing.Point(188, 53);
            this.picAnimation.Name = "picAnimation";
            this.picAnimation.Size = new System.Drawing.Size(204, 204);
            this.picAnimation.TabIndex = 6;
            this.picAnimation.TabStop = false;
            // 
            // FAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(575, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picAnimation);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAbout);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel lblAbout;
        private System.Windows.Forms.LinkLabel label1;
        private System.Windows.Forms.PictureBox picAnimation;
    }
}