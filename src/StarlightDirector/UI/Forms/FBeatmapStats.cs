using System;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.Models.Editor;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public partial class FBeatmapStats : Form {

        public static void ShowDialog(IWin32Window parent, ProjectDocument project) {
            using (var f = new FBeatmapStats()) {
                f._project = project;
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                f.ShowDialog(parent);
                f.UnmonitorLocalizationChange();
            }
        }

        ~FBeatmapStats() {
            UnregisterEventHandlers();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Escape:
                    Close();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private FBeatmapStats() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FBeatmapInfo_Load;
        }

        private void RegisterEventHandlers() {
            Load += FBeatmapInfo_Load;
        }

        private void FBeatmapInfo_Load(object sender, EventArgs e) {
            var listView = lv;
            var project = _project;

            listView.View = View.Details;
            listView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView.ShowItemToolTips = false;
            listView.ShowGroups = true;
            listView.FullRowSelect = true;
            listView.MultiSelect = false;

            listView.Items.Clear();
            listView.Groups.Clear();
            listView.Columns.Clear();

            listView.Columns.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.column.name.text") ?? "Name");
            listView.Columns.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.column.value.text") ?? "Value");

            var groupGeneral = listView.Groups.Add("General", LanguageManager.TryGetString("ui.fbeatmapstats.listview.header.general.text") ?? "General");
            var it = listView.Items.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.item.project_file_name.text") ?? "Project file name");
            it.SubItems.Add(project.SaveFilePath ?? string.Empty);
            it.Group = groupGeneral;
            it = listView.Items.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.item.music_file_name.text") ?? "Music file name");
            it.SubItems.Add(project.Project.MusicFileName ?? string.Empty);
            it.Group = groupGeneral;

            var difficulties = new[] { Difficulty.Debut, Difficulty.Regular, Difficulty.Pro, Difficulty.Master, Difficulty.MasterPlus };
            foreach (var difficulty in difficulties) {
                var score = project.Project.GetScore(difficulty);
                var numberOfNotes = score.GetAllNotes().Count;
                var numberOfBars = score.Bars.Count;
                var duration = score.CalculateDuration();
                var difficultyDescription = DescribedEnumConverter.GetEnumDescription(difficulty);
                var headerTextFormat = LanguageManager.TryGetString("ui.fbeatmapstats.listview.header.items.text_template") ?? "Difficulty: {0}";
                var text = string.Format(headerTextFormat, difficultyDescription);
                var group = listView.Groups.Add(text, text);
                var it1 = listView.Items.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.item.measures.text") ?? "Measures");
                it1.SubItems.Add(numberOfBars.ToString());
                var it2 = listView.Items.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.item.notes.text") ?? "Notes");
                it2.SubItems.Add(numberOfNotes.ToString());
                var it3 = listView.Items.Add(LanguageManager.TryGetString("ui.fbeatmapstats.listview.item.duration.text") ?? "Duration");
                it3.SubItems.Add(duration.ToString("g"));
                it1.Group = group;
                it2.Group = group;
                it3.Group = group;
            }

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private ProjectDocument _project;

    }
}
