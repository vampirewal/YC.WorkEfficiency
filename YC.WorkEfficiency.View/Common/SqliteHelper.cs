#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：SqliteHelper
// 创 建 者：杨程
// 创建时间：2021/4/23 17:49:34
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace YC.WorkEfficiency.View.Common
{
    public class SQLiteHelper
    {
        /// <summary>
        /// ConnectionString样例：Data Source=Test.db3;Pooling=true;FailIfMissing=false
        /// </summary>
        public static string ConnectionString { get; set; }

        private static void PrepareCommand(SqliteCommand cmd, SqliteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                    cmd.Parameters.AddWithValue(string.Empty, parm);
            }
        }

        //public static DataSet ExecuteQuery(string cmdText, params object[] p)
        //{
        //    using (SqliteConnection conn = new SqliteConnection(ConnectionString))
        //    {
        //        using (SqliteCommand command = new SqliteCommand())
        //        {
        //            DataSet ds = new DataSet();
        //            PrepareCommand(command, conn, cmdText, p);
        //            SQLiteDataAdapter da = new SQLiteDataAdapter(command);
        //            da.Fill(ds);
        //            return ds;
        //        }
        //    }
        //}

        public static int ExecuteNonQuery(string cmdText, params object[] p)
        {
            using (SqliteConnection conn = new SqliteConnection(ConnectionString))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public static SqliteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            using (SqliteConnection conn = new SqliteConnection(ConnectionString))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }

        public static object ExecuteScalar(string cmdText, params object[] p)
        {
            using (SqliteConnection conn = new SqliteConnection(ConnectionString))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    PrepareCommand(command, conn, cmdText, p);
                    return command.ExecuteScalar();
                }
            }
        }

    }
}
