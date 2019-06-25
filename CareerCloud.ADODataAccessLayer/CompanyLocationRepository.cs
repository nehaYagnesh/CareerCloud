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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                
                    foreach (CompanyLocationPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                                        ([Id],[Company],[Country_Code],[State_Province_Code],[Street_Address],[City_Town],[Zip_Postal_Code])
                                            VALUES (@Id,@Company,@Country_Code,@State_Province_Code,@Street_Address,@City_Town,@Zip_Postal_Code)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                        cmd.Parameters.AddWithValue("@City_Town", item.City);
                        cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * 
                                   FROM [JOB_PORTAL_DB].[dbo].[Company_Locations]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyLocationPoco[] appPocos = new CompanyLocationPoco[1000];

                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2).PadRight(10);
                    poco.Province = reader.IsDBNull(3) ? null : reader.GetString(3).PadRight(10);
                    poco.Street = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.City = reader.IsDBNull(5) ? null : reader.GetString(5);
                    poco.PostalCode = reader.IsDBNull(6) ? string.Empty : reader.GetString(6).PadRight(20);
                    poco.TimeStamp = (byte[])reader[7];

                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyLocationPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"DELETE FROM  [dbo].[Company_Locations] 
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

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (CompanyLocationPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                                            SET  [Company] = @Company,
                                                 [Country_Code] = @Country_Code,
                                                 [State_Province_Code] = @State_Province_Code,
                                                 [Street_Address] = @Street_Address,
                                                 [City_Town] = @City_Town,
                                                 [Zip_Postal_Code] = @Zip_Postal_Code
                                           WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Company", item.Company);
                        cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                        cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                        cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                        cmd.Parameters.AddWithValue("@City_Town", item.City);
                        cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

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
