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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
          
                 foreach (CompanyProfilePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                                        ([Id],[Registration_Date],[Company_Website],[Contact_Phone],[Contact_Name],[Company_Logo])
                                            VALUES (@Id,@Registration_Date,@Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

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

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * 
                                   FROM [JOB_PORTAL_DB].[dbo].[Company_Profiles]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyProfilePoco[] appPocos = new CompanyProfilePoco[1000];

                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2);
                    poco.ContactPhone =reader.IsDBNull(3) ? string.Empty :reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.CompanyLogo = reader.IsDBNull(5) ? null :(byte[])reader[5];
                    poco.TimeStamp = (byte[])reader[6];

                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyProfilePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
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

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyProfilePoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                            SET  [Registration_Date] = @Registration_Date,
                                                 [Company_Website] = @Company_Website,
                                                 [Contact_Phone] = @Contact_Phone,
                                                 [Contact_Name] = @Contact_Name,
                                                 [Company_Logo] = @Company_Logo
                                           WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                        cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                        cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                        cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                        cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

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
