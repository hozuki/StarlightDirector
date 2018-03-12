using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.App.Gaming;
using StarlightDirector.Beatmap;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FSelectMusicID : Form {

        public static (DialogResult DialogResult, int MusicID, int LiveID) RequestInput(IWin32Window parent, int initialMusicID = 0, int initialLiveID = 0) {
            using (var f = new FSelectMusicID()) {
                f._musicID = initialMusicID;
                f._liveID = initialLiveID;
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                var r = f.ShowDialog(parent);
                f.UnmonitorLocalizationChange();
                return (r, f._musicID, f._liveID);
            }
        }

        ~FSelectMusicID() {
            UnregisterEventHandlers();
        }

        private FSelectMusicID() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FSelectMusicID_Load;
            btnOK.Click -= BtnOK_Click;
        }

        private void RegisterEventHandlers() {
            Load += FSelectMusicID_Load;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if (cboDatabaseItems.Items.Count == 0) {
                _musicID = 1001;
                _liveID = 1;
            } else {
                var record = _musicList[cboDatabaseItems.SelectedIndex];
                _musicID = record.MusicID;
                _liveID = record.LiveID;
            }
            DialogResult = DialogResult.OK;
        }

        private void FSelectMusicID_Load(object sender, EventArgs e) {
            var initialMusicID = _musicID;
            var initialLiveID = _liveID;
            cboDatabaseItems.Items.Clear();
            if (!File.Exists(MasterDatabasePath)) {
                var fileInfo = new FileInfo(MasterDatabasePath);
                var errorMessageTemplate = LanguageManager.TryGetString("messages.fselectmusicid.master_database_file_missing") ?? "The database file '{0}' is missing. Using the default music ID.";
                var errorMessage = string.Format(errorMessageTemplate, fileInfo.FullName);
                MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _musicID = 1001;
                _liveID = 1;
                Close();
            } else {
                try {
                    var csb = new SQLiteConnectionStringBuilder {
                        DataSource = MasterDatabasePath
                    };
                    var musicList = new List<LiveMusicRecord>();
                    using (var connection = new SQLiteConnection(csb.ToString())) {
                        connection.Open();
                        using (var adapter = new SQLiteDataAdapter(FormatFilter, connection)) {
                            using (var dataTable = new DataTable()) {
                                adapter.Fill(dataTable);
                                foreach (DataRow dataRow in dataTable.Rows) {
                                    var record = new LiveMusicRecord {
                                        LiveID = (int)(long)dataRow["live_id"],
                                        MusicID = (int)(long)dataRow["music_id"],
                                        MusicName = ((string)dataRow["music_name"]).Replace(@" \n", " ").Replace(@"\n", " "),
                                        DifficultyExists = new bool[5]
                                    };
                                    for (var i = (int)Difficulty.Debut; i <= (int)Difficulty.MasterPlus; ++i) {
                                        var v = (int)(long)dataRow["d" + i];
                                        record.DifficultyExists[i - 1] = v > 0;
                                    }
                                    var color = (int)(long)dataRow["live_type"];
                                    if (color > 0) {
                                        record.Attribute |= (MusicAttribute)(1 << (color - 1));
                                    }
                                    var isEvent = (long)dataRow["event_type"] > 0;
                                    if (isEvent) {
                                        record.Attribute |= MusicAttribute.Event;
                                    }
                                    // お願い！シンデレラ (solo ver.)
                                    if (record.MusicID == 1901) {
                                        record.Attribute |= MusicAttribute.Solo;
                                    }
                                    // TODO: some other temporary songs (e.g. for songs election)

                                    musicList.Add(record);
                                }
                            }
                        }
                        connection.Close();
                    }

                    _musicList = musicList;
                    var itemTextTemplate = LanguageManager.TryGetString("ui.fselectmusicid.combobox.item.text_template") ?? "{0} [{1}]";
                    foreach (var record in musicList) {
                        var attributeDescription = DescribedEnumConverter.GetEnumDescription(record.Attribute);
                        var str = string.Format(itemTextTemplate, record.MusicName, attributeDescription);
                        cboDatabaseItems.Items.Add(str);
                    }

                    if (cboDatabaseItems.Items.Count > 0) {
                        if (initialMusicID <= 0) {
                            cboDatabaseItems.SelectedIndex = 0;
                        } else {
                            var targetSelectionIndex = musicList.FindIndex(record => {
                                if (initialMusicID != 0 && initialMusicID != record.MusicID) {
                                    return false;
                                }
                                if (initialLiveID != 0 && initialLiveID != record.LiveID) {
                                    return false;
                                }
                                return true;
                            });
                            if (targetSelectionIndex < 0) {
                                targetSelectionIndex = 0;
                            }
                            cboDatabaseItems.SelectedIndex = targetSelectionIndex;
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show(this, $"An error occurred while reading the database. Using the default music ID.{Environment.NewLine}Information: {ex.Message}", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _musicID = 1001;
                    _liveID = 1;
                    Close();
                }
            }
        }

        private IReadOnlyList<LiveMusicRecord> _musicList;

        private int _musicID;
        private int _liveID;

        private static readonly string MasterDatabasePath = "Resources/GameData/master.mdb";

        private static readonly string FormatFilter = @"
SELECT live_data.id AS live_id, music_data.id AS music_id, music_data.name AS music_name,
    difficulty_1 AS d1, difficulty_2 AS d2, difficulty_3 AS d3, difficulty_4 AS d4, difficulty_5 AS d5,
    live_data.sort AS sort, live_data.type as live_type, live_data.event_type as event_type
FROM live_data, music_data
WHERE live_data.music_data_id = music_data.id
ORDER BY sort;";

    }
}
