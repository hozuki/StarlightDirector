using System.Data.SQLite;
using System.IO;

namespace StarlightDirector.Beatmap.IO {
    public static class KnownScoreFormats {

        public static int CheckFormatVersion(string fileName) {
            // major*100+minor
            // 0 = unknown
            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists) {
                return 0;
            }
            var csb = new SQLiteConnectionStringBuilder {
                DataSource = fileInfo.FullName
            };

            object versionObject;
            try {
                using (var db = new SQLiteConnection(csb.ToString())) {
                    db.Open();
                    using (var queryVersion = new SQLiteCommand("SELECT value FROM main WHERE key = 'version';", db)) {
                        versionObject = queryVersion.ExecuteScalar();
                    }
                    db.Close();
                }
            } catch (SQLiteException ex) {
                return 0;
            }
            var versionString = (string)versionObject;
            var fpVersion = double.Parse(versionString);
            int version;
            if (fpVersion < 1) {
                version = (int)(fpVersion * 100);
            } else {
                version = (int)fpVersion;
            }
            switch (version) {
                case ProjectVersion.V0_3:
                case ProjectVersion.V0_3_1:
                case ProjectVersion.V0_4:
                    return version;
                default:
                    return 0;
            }
        }

    }
}
