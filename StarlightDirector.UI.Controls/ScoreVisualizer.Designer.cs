namespace StarlightDirector.UI.Controls {
    partial class ScoreVisualizer {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.scoreRenderer1 = new StarlightDirector.UI.Controls.ScoreRenderer();
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(439, 3);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 504);
            this.vScrollBar1.TabIndex = 1;
            // 
            // scoreRenderer1
            // 
            this.scoreRenderer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.scoreRenderer1.ClearColor = System.Drawing.SystemColors.Control;
            this.scoreRenderer1.Location = new System.Drawing.Point(3, 3);
            this.scoreRenderer1.Name = "scoreRenderer1";
            this.scoreRenderer1.Score = null;
            this.scoreRenderer1.Size = new System.Drawing.Size(433, 504);
            this.scoreRenderer1.TabIndex = 2;
            this.scoreRenderer1.Text = "scoreRenderer1";
            // 
            // ScoreVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scoreRenderer1);
            this.Controls.Add(this.vScrollBar1);
            this.Name = "ScoreVisualizer";
            this.Size = new System.Drawing.Size(459, 510);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private ScoreRenderer scoreRenderer1;
    }
}
