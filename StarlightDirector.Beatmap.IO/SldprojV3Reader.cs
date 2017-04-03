using System;
using System.Data.SQLite;
using System.IO;

namespace StarlightDirector.Beatmap.IO {
    public sealed partial class SldprojV3Reader : ProjectReader {

        public override Project ReadProject(string fileName) {
            var fileInfo = new FileInfo(fileName);
            if (!fileInfo.Exists) {
                throw new IOException($"File '{fileName}' does not exist.");
            }
            if (fileInfo.Length == 0) {
                throw new IOException($"File '{fileName}' is empty.");
            }
            var builder = new SQLiteConnectionStringBuilder {
                DataSource = fileName
            };
            using (var db = new SQLiteConnection(builder.ToString())) {
                db.Open();
                var score = ReadProject(db);
                db.Close();
                return score;
            }
        }

        private static Project ReadProject(SQLiteConnection db) {
            var score = new Score();
            throw new NotImplementedException();
        }

    }
}
