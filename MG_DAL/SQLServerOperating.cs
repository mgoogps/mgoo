using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MG_DAL
{
   public class SQLServerOperating
    {
        string _connectString { get; set; }
        public SQLServerOperating()
        {
            _connectString = ConfigurationManager.ConnectionStrings["connectionString"].ToString();
        }

        public SQLServerOperating(string ConnectString)
        {
            _connectString = ConnectString;
        }

        private SqlConnection GetConnection(string ConnectString = null)
        {
            SqlConnection conn = new SqlConnection();
            if (string.IsNullOrEmpty(ConnectString))
            {
                conn.ConnectionString = _connectString;// ConfigurationManager.ConnectionStrings["connectionString"].ToString();
            }
            else
            {
                conn.ConnectionString = ConnectString;
            } 
            return conn;
        }

        public int ExecuteSql(string strSql)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("ExecuteSql Error3:" + ex.Message+",Sql:"+strSql);
                return 0;
            }
         
        }
        public int ExecuteSql(string strSql, SqlParameter[] sqlParameter)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = strSql;
                    cmd.Connection = conn;
                    cmd.Parameters.AddRange(sqlParameter);
                    return cmd.ExecuteNonQuery(); 
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("ExecuteSql Error2:" + ex.Message + ",Sql:" + strSql);
                return 0;
            }
         
        }

        public string Select(string strSql)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    return cmd.ExecuteScalar().ToStringEmpty();
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("Select Error2:" + ex.Message + ",Sql:" + strSql);
                return string.Empty;
            }
        }
        public string Select(string strSql, SqlParameter[] parameter)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddRange(parameter);
                    cmd.Connection = conn;
                    cmd.CommandText = strSql;
                    return cmd.ExecuteScalar().ToStringEmpty();
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("Select Error1:" + ex.Message + ",Sql:" + strSql);
                return string.Empty;
            }
        }

        public DataTable Selects(string strSql)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("Selects Error1:" + ex.Message + ",Sql:" + strSql);
                return new DataTable();
            }
        }
        public DataTable Selects(string strSql,SqlParameter[] parmeter)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                { 
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(strSql, conn);
                    cmd.Parameters.AddRange(parmeter);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("Selects Error2:" + ex.Message +",strSql:"+ strSql);
                return new DataTable();
            }
        }

        public int ExecuteSql(List<string> sqlList, List<SqlParameter[]> listSqlParmeter)
        {

            if (sqlList.Count == 0)
                return 0;
            SqlConnection conn = GetConnection();
            SqlTransaction st = null;
            try
            {
                conn.Open();
                st = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = st;
                int count = 0;
                cmd.Connection = conn;
                for (int i = 0; i < sqlList.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = sqlList[i];
                    cmd.Parameters.AddRange(listSqlParmeter[i]);
                    count += cmd.ExecuteNonQuery();
                }
                st.Commit();
                return count;
            }
            catch (Exception ex)
            {
                ILog.WriteLog("ExecuteSql Error1:" + ex.Message);
                st.Rollback(); 
                return 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable QueryByProc(string procName, Dictionary<string, string> arr = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    SqlDataAdapter sda = new SqlDataAdapter(procName, conn);
                  
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (arr != null)
                    {
                        foreach (KeyValuePair<string, string> var in arr)
                        {
                            sda.SelectCommand.Parameters.AddWithValue("@" + var.Key, var.Value);
                        }
                    }
                    sda.Fill(dt); 
                }
            }
            catch (Exception ex)
            {
                ILog.WriteLog("QueryByProc Error:"+ ex.Message + ",procName：" + procName); 
            }
            return dt;
        }
    }
}
