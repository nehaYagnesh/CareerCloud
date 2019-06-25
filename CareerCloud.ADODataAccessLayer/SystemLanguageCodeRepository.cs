using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                
                    foreach (SystemLanguageCodePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO  [JOB_PORTAL_DB].[dbo].[System_Language_Codes]
                                                        ([LanguageID],[Name],[Native_Name])
                                                 VALUES (@LanguageID,@Name,@NativeName)";

                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@NativeName", item.NativeName);

                        conn.Open();
                        int rowAffected = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT  *
                                    FROM [JOB_PORTAL_DB].[dbo].[System_Language_Codes]";

                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SystemLanguageCodePoco[] appPocos = new SystemLanguageCodePoco[10];
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    poco.Name = reader.IsDBNull(1) ? string.Empty :reader.GetString(1);
                    poco.NativeName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    appPocos[position] = poco;
                    position++;
                }
                
                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (SystemLanguageCodePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                            WHERE LanguageID = @LanguageID";
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                        conn.Open();
                        int rowAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (SystemLanguageCodePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                            SET [Name] = @Name,
                                                [Native_Name] = @NativeName
                                           WHERE LanguageID = @LanguageID";
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                        cmd.Parameters.AddWithValue("@Name", item.Name);
                        cmd.Parameters.AddWithValue("@NativeName", item.NativeName);

                        conn.Open();
                        int rowAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
