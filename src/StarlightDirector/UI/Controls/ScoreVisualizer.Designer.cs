namespace OpenCGSS.StarlightDirector.UI.Controls {
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
            this.editor = new OpenCGSS.StarlightDirector.UI.Controls.Editing.ScoreEditor();
            this.previewer = new OpenCGSS.StarlightDirector.UI.Controls.Previewing.ScorePreviewer();
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
            this.editor.Difficulty = OpenCGSS.StarlightDirector.Models.Beatmap.Difficulty.Debut;
            this.editor.EditMode = OpenCGSS.StarlightDirector.UI.Controls.Editing.ScoreEditMode.Select;
            this.editor.Location = new System.Drawing.Point(3, 3);
            this.editor.Name = "editor";
            this.editor.NoteStartPosition = OpenCGSS.StarlightDirector.Models.Beatmap.NotePosition.Default;
            this.editor.Project = null;
            this.editor.ScrollOffsetX = 0;
            this.editor.Size = new System.Drawing.Size(433, 504);
            this.editor.SuspendOnFormInactive = false;
            this.editor.TabIndex = 2;
            this.editor.Text = "Score Editor";
            // 
            // previewer
            // 
            this.previewer.Location = new System.Drawing.Point(3, 18);
            this.previewer.Name = "previewer";
            this.previewer.RenderMode = OpenCGSS.StarlightDirector.UI.Controls.Previewing.PreviewerRenderMode.Standard;
            this.previewer.Score = null;
            this.previewer.Size = new System.Drawing.Size(456, 489);
            this.previewer.SuspendOnFormInactive = false;
            this.previewer.TabIndex = 3;
            this.previewer.Text = "Score Previewer";
            // 
            // ScoreVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.previewer);
            this.Controls.Add(this.editor);
            this.Controls.Add(this.vScroll);
            this.Name = "ScoreVisualizer";
            this.Size = new System.Drawing.Size(459, 510);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScroll;
        private global::OpenCGSS.StarlightDirector.UI.Controls.Editing.ScoreEditor editor;
        private Previewing.ScorePreviewer previewer;
    }
}
