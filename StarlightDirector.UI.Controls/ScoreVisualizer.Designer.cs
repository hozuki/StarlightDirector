using System.Drawing;

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
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.editor = new StarlightDirector.UI.Controls.ScoreEditor();
            this.SuspendLayout();
            // 
            // vScroll
            // 
            this.vScroll.LargeChange = 100;
            this.vScroll.Location = new System.Drawing.Point(439, 3);
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(20, 504);
            this.vScroll.SmallChange = 29;
            this.vScroll.TabIndex = 1;
            // 
            // editor
            // 
            this.editor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.editor.ClearColor = System.Drawing.Color.Black;
            this.editor.Difficulty = StarlightDirector.Beatmap.Difficulty.Debut;
            this.editor.Location = new System.Drawing.Point(3, 3);
            this.editor.Name = "editor";
            this.editor.PrimaryBeatMode = StarlightDirector.UI.Controls.PrimaryBeatMode.EveryFourBeats;
            this.editor.Project = null;
            this.editor.ScrollOffsetX = 0;
            this.editor.ScrollOffsetY = 0;
            this.editor.Size = new System.Drawing.Size(433, 504);
            this.editor.TabIndex = 2;
            this.editor.Text = "Score Editor";
            // 
            // ScoreVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.editor);
            this.Controls.Add(this.vScroll);
            this.Name = "ScoreVisualizer";
            this.Size = new System.Drawing.Size(459, 510);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScroll;
        private ScoreEditor editor;
    }
}
