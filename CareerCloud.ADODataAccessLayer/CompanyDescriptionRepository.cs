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
   public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyDescriptionPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                                        ([Id],[Company],[LanguageID],[Company_Name],[Company_Description])
                                            VALUES (@Id,@Company,@LanguageID,@Company_Name,@Company_Description)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                        cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * 
                                   FROM [JOB_PORTAL_DB].[dbo].[Company_Descriptions]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyDescriptionPoco[] appPocos = new CompanyDescriptionPoco[1000];

                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.LanguageId = reader.IsDBNull(2) ?  string.Empty : reader.GetString(2);
                    poco.CompanyName = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    poco.CompanyDescription = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];

                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyDescriptionPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"DELETE FROM  [dbo].[Company_Descriptions]
                                            WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);

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

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyDescriptionPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                            SET  [Company] = @Company,
                                                 [LanguageID] = @LanguageID,
                                                 [Company_Name] = @Company_Name,
                                                 [Company_Description] = @Company_Description
                                          WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                        cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                        cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

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
