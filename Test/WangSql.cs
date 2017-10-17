using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace WangSql
{
    public enum DbType
    {
        //Oracle,SqlServer,MySql,Access,SqlLite
        NONE,
        ORACLE,
        SQLSERVER,
        MYSQL,
        ACCESS,
        SQLLITE
    }

    public class DBFactory
    {
        public static IDbConnection CreateDbConnection(DbType type, string connectionString)
        {
            IDbConnection conn = null;
            switch (type)
            {
                case DbType.ORACLE:
                    conn = new OracleConnection(connectionString);
                    break;
                case DbType.SQLSERVER:
                    conn = new SqlConnection(connectionString);
                    break;
                case DbType.MYSQL:
                    conn = new MySqlConnection(connectionString);
                    break;
                case DbType.ACCESS:
                    conn = new OleDbConnection(connectionString);
                    break;
                case DbType.SQLLITE:
                    conn = new SQLiteConnection(connectionString);
                    break;
                case DbType.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return conn;
        }


        public static IDbCommand CreateDbCommand(DbType type)
        {
            IDbCommand cmd = null;
            switch (type)
            {
                case DbType.ORACLE:
                    cmd = new OracleCommand();
                    break;
                case DbType.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
                case DbType.MYSQL:
                    cmd = new MySqlCommand();
                    break;
                case DbType.ACCESS:
                    cmd = new OleDbCommand();
                    break;
                case DbType.SQLLITE:
                    cmd = new SQLiteCommand();
                    break;
                case DbType.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return cmd;
        }
        public static IDbCommand CreateDbCommand(string sql, IDbConnection conn)
        {
            DbType type = DbType.NONE;
            if (conn is OracleConnection)
                type = DbType.ORACLE;
            else if (conn is SqlConnection)
                type = DbType.SQLSERVER;
            else if (conn is MySqlConnection)
                type = DbType.MYSQL;
            else if (conn is OleDbConnection)
                type = DbType.ACCESS;
            else if (conn is SQLiteConnection)
                type = DbType.SQLLITE;

            IDbCommand cmd = null;
            switch (type)
            {
                case DbType.ORACLE:
                    cmd = new OracleCommand(sql, (OracleConnection)conn);
                    break;
                case DbType.SQLSERVER:
                    cmd = new SqlCommand(sql, (SqlConnection)conn);
                    break;
                case DbType.MYSQL:
                    cmd = new MySqlCommand(sql, (MySqlConnection)conn);
                    break;
                case DbType.ACCESS:
                    cmd = new OleDbCommand(sql, (OleDbConnection)conn);
                    break;
                case DbType.SQLLITE:
                    cmd = new SQLiteCommand(sql, (SQLiteConnection)conn);
                    break;
                case DbType.NONE:
                    throw new Exception("未设置数据库类型");
                default:
                    throw new Exception("不支持该数据库类型");
            }
            return cmd;
        }


    }
}