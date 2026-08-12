using Dapper;
using Newtonsoft.Json;
using System;
using System.Data.SQLite;

namespace latihribbon
{
    public static class LoggingHelper
    {
        public static void WriteLog(SQLiteConnection conn, SQLiteTransaction trans, string tableName, string action, object pkId, object beforeData = null, object afterData = null)
        {
            if (conn == null || string.IsNullOrWhiteSpace(tableName)) return;

            string normalizedTableName = tableName.Trim();
            string logTableName = $"Log_{normalizedTableName}";
            string currentUser = UserSession.CurrentUser ?? "System";
            string pkString = pkId != null ? pkId.ToString() : "0";

            var logPayload = new
            {
                TableName = normalizedTableName,
                Action = action,
                PrimaryKey = pkString,
                User = currentUser,
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Before = beforeData,
                After = afterData
            };

            string jsonContent = JsonConvert.SerializeObject(logPayload, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            });

            string sqlLog = $@"INSERT INTO {logTableName} (Timestamp, User, Action, PkId, ReferenceTable, ContentJson) 
                              VALUES (@Timestamp, @User, @Action, @PkId, @ReferenceTable, @ContentJson)";

            var dp = new DynamicParameters();
            dp.Add("@Timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dp.Add("@User", currentUser);
            dp.Add("@Action", action);
            dp.Add("@PkId", pkString);
            dp.Add("@ReferenceTable", normalizedTableName);
            dp.Add("@ContentJson", jsonContent);

            conn.Execute(sqlLog, dp, trans);
        }
    }
}
