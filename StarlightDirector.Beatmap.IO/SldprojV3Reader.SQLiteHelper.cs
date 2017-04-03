using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace StarlightDirector.Beatmap.IO {
    partial class SldprojV3Reader {

        private static class SQLiteHelper {

            public static bool DoesTableExist(SQLiteTransaction transaction, string tableName) {
                return DoesTableExist(transaction.Connection, tableName);
            }

            public static bool DoesTableExist(SQLiteConnection connection, string tableName) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tableName;";
                    command.Parameters.Add("tableName", DbType.AnsiString).Value = tableName;
                    var value = command.ExecuteScalar();
                    return value != null;
                }
            }

            public static bool DoesColumnExist(SQLiteTransaction transaction, string tableName, string columnName) {
                return DoesColumnExist(transaction.Connection, tableName, columnName);
            }

            public static bool DoesColumnExist(SQLiteConnection connection, string tableName, string columnName) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tableName;";
                    command.Parameters.Add("tableName", DbType.AnsiString).Value = tableName;
                    var value = command.ExecuteScalar();
                    if (value == null) {
                        return false;
                    }
                    // TODO: Is it possible to use command parameters?
                    command.CommandText = $"PRAGMA table_info('{tableName}');";
                    command.Parameters.RemoveAt("tableName");
                    using (var reader = command.ExecuteReader()) {
                        while (reader.NextResult()) {
                            while (reader.Read()) {
                                var ordinal = reader.GetOrdinal("name");
                                value = reader.GetValue(ordinal);
                                if ((string)value == columnName) {
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }

            public static void SetValue(SQLiteTransaction transaction, string tableName, string key, string value, bool creatingNewDatabase, ref SQLiteCommand command) {
                SetValue(transaction.Connection, tableName, key, value, creatingNewDatabase, ref command);
            }

            public static void SetValue(SQLiteConnection connection, string tableName, string key, string value, bool creatingNewDatabase, ref SQLiteCommand command) {
                if (creatingNewDatabase) {
                    InsertValue(connection, tableName, key, value, ref command);
                } else {
                    UpdateValue(connection, tableName, key, value, ref command);
                }
            }

            public static void InsertValue(SQLiteTransaction transaction, string tableName, string key, string value, ref SQLiteCommand command) {
                InsertValue(transaction.Connection, tableName, key, value, ref command);
            }

            public static void InsertValue(SQLiteConnection connection, string tableName, string key, string value, ref SQLiteCommand command) {
                if (command == null) {
                    command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO {tableName} (key, value) VALUES (@key, @value);";
                    command.Parameters.Add("key", DbType.AnsiString).Value = key;
                    command.Parameters.Add("value", DbType.AnsiString).Value = value;
                } else {
                    command.CommandText = $"INSERT INTO {tableName} (key, value) VALUES (@key, @value);";
                    command.Parameters["key"].Value = key;
                    command.Parameters["value"].Value = value;
                }
                command.ExecuteNonQuery();
            }

            public static void UpdateValue(SQLiteTransaction transaction, string tableName, string key, string value, ref SQLiteCommand command) {
                UpdateValue(transaction.Connection, tableName, key, value, ref command);
            }

            public static void UpdateValue(SQLiteConnection connection, string tableName, string key, string value, ref SQLiteCommand command) {
                if (command == null) {
                    command = connection.CreateCommand();
                    command.CommandText = $"UPDATE {tableName} SET value = @value WHERE key = @key;";
                    command.Parameters.Add("key", DbType.AnsiString).Value = key;
                    command.Parameters.Add("value", DbType.AnsiString).Value = value;
                } else {
                    command.CommandText = $"UPDATE {tableName} SET value = @value WHERE key = @key;";
                    command.Parameters["key"].Value = key;
                    command.Parameters["value"].Value = value;
                }
                command.ExecuteNonQuery();
            }

            public static string GetValue(SQLiteTransaction transaction, string tableName, string key, ref SQLiteCommand command) {
                return GetValue(transaction.Connection, tableName, key, ref command);
            }

            public static string GetValue(SQLiteConnection connection, string tableName, string key, ref SQLiteCommand command) {
                if (command == null) {
                    command = connection.CreateCommand();
                    command.CommandText = $"SELECT value FROM {tableName} WHERE key = @key;";
                    command.Parameters.Add("key", DbType.AnsiString).Value = key;
                } else {
                    command.CommandText = $"SELECT value FROM {tableName} WHERE key = @key;";
                    command.Parameters["key"].Value = key;
                }
                var value = command.ExecuteScalar();
                return (string)value;
            }

            public static StringDictionary GetValues(SQLiteTransaction transaction, string tableName, ref SQLiteCommand command) {
                return GetValues(transaction.Connection, tableName, ref command);
            }

            public static StringDictionary GetValues(SQLiteConnection connection, string tableName, ref SQLiteCommand command) {
                if (command == null) {
                    command = connection.CreateCommand();
                }
                command.CommandText = $"SELECT key, value FROM {tableName};";
                var result = new StringDictionary();
                using (var reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var row = reader.GetValues();
                        result.Add(row[0], row[1]);
                    }
                }
                return result;
            }

            public static void CreateKeyValueTable(SQLiteTransaction transaction, string tableName) {
                CreateKeyValueTable(transaction.Connection, tableName);
            }

            public static void CreateKeyValueTable(SQLiteConnection connection, string tableName) {
                using (var command = connection.CreateCommand()) {
                    // Have to use LONGTEXT (2^31-1) rather than TEXT (32768).
                    command.CommandText = $"CREATE TABLE {tableName} (key LONGTEXT PRIMARY KEY NOT NULL, value LONGTEXT NOT NULL);";
                    command.ExecuteNonQuery();
                }
            }

            public static void CreateBarParamsTable(SQLiteTransaction transaction) {
                CreateBarParamsTable(transaction.Connection);
            }

            public static void CreateBarParamsTable(SQLiteConnection connection) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $@"CREATE TABLE {Names.Table_BarParams} (
{Names.Column_Difficulty} INTEGER NOT NULL, {Names.Column_BarIndex} INTEGER NOT NULL, {Names.Column_GridPerSignature} INTEGER, {Names.Column_Signature} INTEGER,
PRIMARY KEY ({Names.Column_Difficulty}, {Names.Column_BarIndex}));";
                    command.ExecuteNonQuery();
                }
            }

            public static void CreateSpecialNotesTable(SQLiteTransaction transaction) {
                CreateSpecialNotesTable(transaction.Connection);
            }

            public static void CreateSpecialNotesTable(SQLiteConnection connection) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $@"CREATE TABLE {Names.Table_SpecialNotes} (
{Names.Column_ID} INTEGER NOT NULL PRIMARY KEY, {Names.Column_Difficulty} INTEGER NOT NULL, {Names.Column_BarIndex} INTEGER NOT NULL, {Names.Column_IndexInGrid} INTEGER NOT NULL,
{Names.Column_NoteType} INTEGER NOT NULL, {Names.Column_ParamValues} TEXT NOT NULL,
FOREIGN KEY ({Names.Column_ID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}));";
                    command.ExecuteNonQuery();
                }
            }

            public static void CreateScoresTable(SQLiteTransaction transaction) {
                CreateScoresTable(transaction.Connection);
            }

            public static void CreateScoresTable(SQLiteConnection connection) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $"CREATE TABLE {Names.Table_NoteIDs} ({Names.Column_ID} INTEGER NOT NULL PRIMARY KEY);";
                    command.ExecuteNonQuery();
                    command.CommandText = $@"CREATE TABLE {Names.Table_Notes} (
{Names.Column_ID} INTEGER PRIMARY KEY NOT NULL, {Names.Column_Difficulty} INTEGER NOT NULL, {Names.Column_BarIndex} INTEGER NOT NULL, {Names.Column_IndexInGrid} INTEGER NOT NULL,
{Names.Column_NoteType} INTEGER NOT NULL, {Names.Column_StartPosition} INTEGER NOT NULL, {Names.Column_FinishPosition} INTEGER NOT NULL, {Names.Column_FlickType} INTEGER NOT NULL,
{Names.Column_PrevFlickNoteID} INTEGER NOT NULL, {Names.Column_NextFlickNoteID} NOT NULL, {Names.Column_SyncTargetID} INTEGER NOT NULL, {Names.Column_HoldTargetID} INTEGER NOT NULL,
FOREIGN KEY ({Names.Column_ID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}), FOREIGN KEY ({Names.Column_PrevFlickNoteID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}),
FOREIGN KEY ({Names.Column_NextFlickNoteID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}), FOREIGN KEY ({Names.Column_SyncTargetID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}),
FOREIGN KEY ({Names.Column_HoldTargetID}) REFERENCES {Names.Table_NoteIDs}({Names.Column_ID}));";
                    command.ExecuteNonQuery();
                }
            }

            public static void ReadNotesTable(SQLiteConnection connection, Difficulty difficulty, DataTable table) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $"SELECT * FROM {Names.Table_Notes} WHERE {Names.Column_Difficulty} = @difficulty;";
                    command.Parameters.Add("difficulty", DbType.Int32).Value = (int)difficulty;
                    using (var adapter = new SQLiteDataAdapter(command)) {
                        adapter.Fill(table);
                    }
                }
            }

            public static void ReadBarParamsTable(SQLiteConnection connection, Difficulty difficulty, DataTable dataTable) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $"SELECT * FROM {Names.Table_BarParams} WHERE {Names.Column_Difficulty} = @difficulty;";
                    command.Parameters.Add("difficulty", DbType.Int32).Value = (int)difficulty;
                    using (var adapter = new SQLiteDataAdapter(command)) {
                        adapter.Fill(dataTable);
                    }
                }
            }

            public static void ReadSpecialNotesTable(SQLiteConnection connection, Difficulty difficulty, DataTable dataTable) {
                using (var command = connection.CreateCommand()) {
                    command.CommandText = $"SELECT * FROM {Names.Table_SpecialNotes} WHERE {Names.Column_Difficulty} = @difficulty;";
                    command.Parameters.Add("difficulty", DbType.Int32).Value = (int)difficulty;
                    using (var adapter = new SQLiteDataAdapter(command)) {
                        adapter.Fill(dataTable);
                    }
                }
            }

        }

    }
}
