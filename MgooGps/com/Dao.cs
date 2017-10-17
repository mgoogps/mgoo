using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
//引入配制文件命名空间
using System.Configuration;
using System.Web.Script.Serialization;
using System.IO;
 using Newtonsoft.Json;
using System.Collections;
//using System.Data.SqlClient;

namespace MgooGps.com
{
    /// <summary>
    /// 通用的数据库操作类
    /// </summary>
    public class Dao
    {
        /// <summary>
        /// 创建连接对象
        /// </summary>
        /// <returns>连接对象</returns>
        public static SqlConnection CreateConn()
        {
            //读取配制文件内的连接字符串"Data Source =.;User ID=sa;Password=52family;database=YiwenGPS ";
            string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connStr);
            //  SqlConnection conn = new SqlConnection(connStr);
            return connection;
        }
        public static string CreateConnStr()
        {
            //读取配制文件内的连接字符串"Data Source =.;User ID=sa;Password=52family;database=YiwenGPS ";
            string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            return connStr;
        }

        public static SqlConnection CreateConn(DateTime time)
        {
            string connStr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            string dbName = ConfigurationManager.ConnectionStrings["StrConnectionDataBaseName"].ConnectionString + "" + time.Year + "" + time.Month.ToString().PadLeft(2, '0');
            connStr = connStr.Substring(0, connStr.IndexOf("database=")) + "database="+dbName;
            SqlConnection connection = new SqlConnection(connStr);
            return connection;
        }

        /// <summary>
        /// 通用的添加方法
        /// </summary>
        /// <param name="arr">存放数据集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>操作结果(大于0-成功,等于0-失败,小于0-异常)</returns>
        public static int Insert(Dictionary<string,object> arr,string tableName)
        {
            #region//添加单条
            SqlConnection conn = CreateConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string s1 = "insert into " + tableName + "(";
                string s2 = ")values(";
                string s3 = ")";
                //遍历集合的每一个元素
                foreach (KeyValuePair<string, object> var in arr)
                {
                    s1 += var.Key + ",";
                    s2 += "@" + var.Key + ",";
                    cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                }
                s1 = s1.Substring(0, s1.Length - 1);
                s2 = s2.Substring(0, s2.Length - 1);
                string sql = s1 + s2 + s3;
                cmd.CommandText = sql;
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
            #endregion
        }

        /// <summary>
        /// 通用的删除方法
        /// </summary>
        /// <param name="arr">存放数据的集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>操作结果(大于0-成功,等于0-失败,小于0-异常)</returns>
        public static int Delete(Dictionary<string, object> arr, string tableName)
        {
            SqlConnection conn = CreateConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string sql = "delete from " + tableName + " where ";
                foreach (KeyValuePair<string, object> var in arr)
                {
                    sql += var.Key + "=@" + var.Key + " and ";
                    cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                }
                sql = sql.Substring(0, sql.Length - 4);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        ///// <summary>
        ///// 执行查询语句，返回DataSet
        ///// </summary>
        ///// <param name="SQLString">查询语句</param>
        ///// <returns>DataSet</returns>
        //public static DataSet Select(string SQLString, params SqlParameter[] cmdParms)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
        //        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //        {
        //            DataSet ds = new DataSet();
        //            try
        //            {
        //                da.Fill(ds, "ds");
        //                cmd.Parameters.Clear();
        //            }
        //            catch (System.Data.SqlClient.SqlException ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //            return ds;
        //        }
        //    }
        //}
        /// <summary>
        /// 通用的查询单条方法,没有数据返回NULL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回一行数据</returns>
        public static DataRow Select(string sql)
        {
            try
            {
                using (SqlConnection conn = CreateConn())
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    conn.Open();
                    sda.Fill(ds);
                    if (ds.Tables[0].Rows.Count == 0)
                        return null;
                    //获得一行数据 
                    return ds.Tables[0].Rows[0];
                }
            }
            catch (Exception ex)
            {
                Utils.log("Select Error1:" + ex.Message);
                throw;
            }
          
        }

        public static Hashtable Select(string sql, SqlParameter[] parame)
        {
            try
            { 
                using (SqlConnection conn = CreateConn())
                {
                   
                    SqlCommand cmd = new SqlCommand();
                    for (int i = 0; i < parame.Length; i++)
                    {
                        cmd.Parameters.AddWithValue(parame[i].ParameterName, parame[i].Value);
                    }
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Hashtable table = new Hashtable(); 
                    if (reader.Read())
                    { 
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            table[reader.GetName(i)] = reader.GetValue(i); 
                        } 
                    }
                    cmd.Parameters.Clear();
                    reader.Close();
                    return table; 
                }
            }
            catch (Exception ex)
            {
                Utils.log("Select Error2:"+ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 通用的查询多条数据
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>返回多条数据</returns>
        public static DataTable Selects(string sql)
        {
            try
            { 
                SqlConnection conn = CreateConn(); 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Utils.log("Selects Error2:"+ex.Message);
                return new DataTable();
            }
        }
        public static DataTable Selects(string sql, SqlConnection conn)
        {
            try
            { 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Utils.log("Selects Error1:" + ex.Message);
                return new DataTable();
            }
        }

        public static DataTable Selects(string sql,SqlParameter[] parameter)
        {
            try
            {
                SqlConnection conn = CreateConn();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddRange(parameter);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                cmd.Parameters.Clear();
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Utils.log("Selects Error3:" + ex.Message);
                return new DataTable();
            }
        }

        /// <summary>
        /// 通用的修改方法
        /// </summary>
        /// <param name="di">存放数据的集合</param>
        /// <param name="tableName">表名</param>
        /// <returns>操作结果(大于0-成功,等于0-失败,小于0-异常)</returns>
        public static int Update(Dictionary<string,object> di,string tableName,String key)
        {
            SqlConnection conn = CreateConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                string sql = "update " + tableName + " set ";
                //查询指定表的主键列名
              //  string key = Select("sp_pkeys " + tableName)["COLUMN_NAME"].ToString(); 
                //遍历集合的每一个元素
                foreach (KeyValuePair<string, object> var in di)
                {
                    if (var.Key != key)
                    {
                        sql += var.Key + "=@" + var.Key + ",";
                        cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                    }
                }
                //去掉最后的逗事
                sql = sql.Substring(0, sql.Length - 1);
                //拼条件
                sql += " where " + key + "=@" + key;
                cmd.Parameters.AddWithValue("@" + key, di[key]);
                cmd.CommandText = sql;
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Utils.log("Update Error:" + ex.Message); 
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="list">要添加的数据集合</param>
        /// <param name="tableName">目标表名称</param>
        /// <returns>受影响的行数</returns> 
        public static int Inserts(List<Dictionary<string, object>> list, string tableName)
        {
            SqlConnection conn = CreateConn();
            SqlTransaction st = null;
            try
            {
                conn.Open();
                st = conn.BeginTransaction();
                int count = 0;
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = st;
                foreach (Dictionary<string, object> arr in list)
                {
                    //清空参数
                    cmd.Parameters.Clear();
                    string s1 = "insert into " + tableName + "(";
                    string s2 = ")values(";
                    string s3 = ")";
                    //遍历集合的每一个元素
                    foreach (KeyValuePair<string, object> var in arr)
                    {
                        s1 += var.Key + ",";
                        s2 += "@" + var.Key + ",";
                        cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                    }
                    s1 = s1.Substring(0, s1.Length - 1);
                    s2 = s2.Substring(0, s2.Length - 1);
                    string sql = s1 + s2 + s3;
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    count += cmd.ExecuteNonQuery();
                }
                //提交事务
                st.Commit();
                return count;
            }
            catch (Exception ex)
            {
                Utils.log("Inserts Error:" + ex.Message); 
                //回滚事务
                st.Rollback();
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="list">批量删除数据ID集合</param>
        /// <param name="tableName">目标表名称</param>
        /// <returns>受影响的行数</returns>
        public static int Deletes(List<Dictionary<string, object>> list, string tableName)
        {
            SqlConnection conn = CreateConn();
            //创建事务
            SqlTransaction st = null;
            //异常处理
            try
            {
                conn.Open();
                //开启事务
                st = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = st;
                int count = 0;
                foreach (Dictionary<string, object> arr in list)
                {
                    //清空参数列表
                    cmd.Parameters.Clear();
                    #region 单条删除
                    string sql = "delete from " + tableName + " where ";
                    foreach (KeyValuePair<string, object> var in arr)
                    {
                        sql += var.Key + "=@" + var.Key + " and ";
                        cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                    }
                    sql = sql.Substring(0, sql.Length - 4);
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    count += cmd.ExecuteNonQuery();
                    #endregion
                }
                //提交事务
                st.Commit();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //回滚事务
                st.Rollback();
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }
        public static string ExecuteScalar(string strSql, SqlParameter[] sqlParameter)
        {
            try
            {
                using (SqlConnection conn = CreateConn())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = strSql;
                    cmd.Connection = conn;
                    cmd.Parameters.AddRange(sqlParameter);
                    return cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.log("ExecuteScalar Error:" + ex.Message);
                return default(string); 
            } 
        }
        public static int ExecutionSQL(string strSql, SqlParameter[] sqlParameter)
        {
            SqlConnection conn = CreateConn();
            SqlCommand cmd = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.Parameters.AddRange(sqlParameter);
                cmd.CommandText = strSql;
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery(); 
            }
            catch (Exception ex)
            {
                Utils.log("ExecutionSQL Error1:" + ex.Message); 
                for (int i = 0; i < sqlParameter.Length; i++)
                { 
                    Utils.log(string.Format("列名：{0}, 值:{1},类型：{2},SourceColumn:{3}", sqlParameter[i].ParameterName,sqlParameter[i].SqlValue,sqlParameter[i].SqlDbType,sqlParameter[i].SourceColumn));
                }
              
                return -1;
            }
            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
        }
        public static int ExecutionSQL(List<string> sqlList,bool isTransaction=true)
        {
            if (sqlList.Count == 0)
                return 0;
            string connStr = CreateConnStr();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlTransaction st = null;
                try
                { 
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        conn.Open();
                        if (isTransaction)
                        {
                            st = conn.BeginTransaction();
                            cmd.Transaction = st; 
                        }
                        int sum = 0;
                        cmd.Connection = conn;
                        for (int i = 0; i < sqlList.Count; i++)
                        {
                            cmd.CommandText = sqlList[i];
                            int result = cmd.ExecuteNonQuery();
                            if (result > 0)
                                sum++;
                            else
                            {
                                Utils.log(sqlList[i]);
                            }
                        }
                        if (isTransaction)
                            st.Commit();
                        return sum;
                    }
                }
                catch (Exception ex)
                {
                    if (isTransaction)
                        st.Rollback();
                    else
                        Utils.log("ExecutionSQL: " + ex.Message);
                    return 0;
                }
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="list">修改数据集合</param>
        /// <param name="tableName">目标表名称</param>
        /// <returns>受影响的行数</returns>
        public static int Updates(List<Dictionary<string, object>> list, string tableName)
        {
            SqlConnection conn = CreateConn();
            SqlTransaction st = null;
            try
            {
                conn.Open();
                st = conn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = st;
                int count = 0;
                foreach (Dictionary<string, object> arr in list)
                {
                    cmd.Parameters.Clear();
                    //对arr条数据做单条修改
                    string sql = "update " + tableName + " set ";
                    //查询指定表的主键列名
                   string key =  Select("sp_pkeys " + tableName)["COLUMN_NAME"].ToString(); 
                    foreach (KeyValuePair<string, object> var in arr)
                    {
                        if (var.Key != key)
                        {
                            sql += var.Key + "=@" + var.Key + ",";
                            cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                        }
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    //拼where条件
                    sql += " where " + key + "=@" + key;
                    cmd.Parameters.AddWithValue("@" + key, arr[key]);
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    count += cmd.ExecuteNonQuery();
                }
                st.Commit();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                st.Rollback();
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 通用的查询存储过程
        /// </summary>
        /// <param name="arr">存放数据的集合</param>
        /// <param name="procName">存储过程名</param>
        /// <returns>多条数据</returns>
        public static List<Dictionary<string, object>> SelectByProc(Dictionary<string,object> arr,string procName)
        {
            List<Dictionary<string, object>> lst = new List<Dictionary<string, object>>();
            SqlConnection conn = CreateConn();
            SqlDataAdapter sda = new SqlDataAdapter(procName, conn);
            DataTable dt = new DataTable();
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;
            //判断集合是否为空
            if (arr != null)
            {
                foreach (KeyValuePair<string, object> var in arr)
                {
                    sda.SelectCommand.Parameters.AddWithValue("@" + var.Key, var.Value);
                }
            }
            sda.Fill(dt);
            //将数据表的所有数据加入至集合并返回至界面
            foreach (DataRow row in dt.Rows)
            {
                Dictionary<string, object> di = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    di.Add(col.ColumnName, row[col.ColumnName]);
                }
                lst.Add(di);
            }
            return lst;
        }

        /// <summary>
        /// 通用的增删改存储过程
        /// </summary>
        /// <param name="arr">存储数据的集合</param>
        /// <param name="procName">存储过程名</param>
        /// <returns>受影响的数据</returns>
        public static int OperByProc(Dictionary<string,object> arr,string procName)
        {
            SqlConnection conn = CreateConn();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procName;
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (KeyValuePair<string, object> var in arr)
                {
                    cmd.Parameters.AddWithValue("@" + var.Key, var.Value);
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 反序列化，把string转成Dictionary集合
        /// </summary>
        /// <param name="obj"></param>
        public static Dictionary<string, object> ToDict(string obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<Dictionary<string, object>>(obj);
        }
        /// <summary>
        ///  反序列化，把string转成List集合
        /// </summary>
        /// <param name="obj"></param>
        public static List<Dictionary<string, object>> ToList(string obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<List<Dictionary<string, object>>>(obj);
        }
        /// <summary>
        /// 把集合序列化
        /// </summary>
        /// <param name="json"></param>
        public static string ToJson(object json)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(json);
        }

        /// <summary>    
        /// 把表格转换成json数据   
        /// /// 通过表格名查找表格数组中的数据   
        /// /// </summary>    
        /// <param name="table">表格</param>    
        /// <param name="JsonName">表格名称</param>
        /// <returns></returns>    
        public static string DataTableToJSON(DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                jw.WriteStartObject();
               jw.WritePropertyName("");
              //  表格名           
                jw.WriteStartArray();
               // 表格数组           
                try
                {
                 //   通过循环输出表格中数据               
                    foreach (DataRow dr in table.Rows)
                    {
                        jw.WriteStartObject();
                        foreach (DataColumn dc in table.Columns)
                        {
                            jw.WritePropertyName(dc.ColumnName);
                            ser.Serialize(jw, dr[dc].ToString());
                        }
                        jw.WriteEndObject();//结束输出        
                    }
                    jw.WriteEndArray();//结束表格输出              
                    jw.WriteEndObject();//结束输出     
                }
                catch (Exception ex)
                {
                    string me = ex.Message;
                }
                sw.Close();//关闭流           
                jw.Close();//关闭流      
               }
              return sb.ToString(); 
        }
        /// <summary>
        /// 执行一条SQL语句
        /// </summary>
        /// <param name="strSql">ＳＱＬ语句</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecutionSQL(string strSql, SqlConnection conn = null)
        {
            conn = conn == null ? CreateConn() : conn;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(strSql, conn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
            }
        }

      
    }
}
