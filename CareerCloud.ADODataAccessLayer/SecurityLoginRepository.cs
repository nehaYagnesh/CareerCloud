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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                foreach (SecurityLoginPoco item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                                        ([Id],
                                                        [Login],
                                                        [Password],
                                                        [Created_Date],
                                                        [Password_Update_Date],
                                                        [Agreement_Accepted_Date],
                                                        [Is_Locked],
                                                        [Is_Inactive],
                                                        [Email_Address],
                                                        [Phone_Number],
                                                        [Full_Name],
                                                        [Force_Change_Password],
                                                        [Prefferred_Language])
                                                 VALUES (@Id,
                                                         @Login,
                                                         @Password,
                                                         @Created_Date,
                                                         @Password_Update_Date,
                                                         @Agreement_Accepted_Date,
                                                         @Is_Locked,
                                                         @Is_Inactive,
                                                         @Email_Address,
                                                         @Phone_Number,
                                                         @Full_Name,
                                                         @Force_Change_Password,
                                                         @Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * 
                                   FROM [JOB_PORTAL_DB].[dbo].[Security_Logins]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[1000];

                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Login = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);   
                    poco.Password = reader.IsDBNull(1) ? string.Empty : reader.GetString(2);
                    poco.Created = reader.GetDateTime(3);
                    poco.PasswordUpdate = reader.IsDBNull(4) ? null : (DateTime?)reader.GetDateTime(4);
                    poco.AgreementAccepted = reader.IsDBNull(5) ? null : (DateTime?)reader.GetDateTime(5);
                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    poco.PhoneNumber = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                    poco.FullName = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    poco.PrefferredLanguage = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    poco.TimeStamp = (byte[])reader[13];

                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                foreach (SecurityLoginPoco item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"DELETE FROM  [dbo].[Security_Logins]
                                            WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    conn.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString))
            {
                foreach (SecurityLoginPoco item in items)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                                            SET  [Login] = @Login,
                                                 [Password] = @Password,
                                                 [Created_Date] = @Created_Date,
                                                 [Password_Update_Date] = @Password_Update_Date,
                                                 [Agreement_Accepted_Date] = @Agreement_Accepted_Date,
                                                 [Is_Locked] = @Is_Locked,
                                                 [Is_Inactive] = @Is_Inactive,
                                                 [Email_Address] = @Email_Address,
                                                 [Phone_Number] = @Phone_Number,
                                                 [Full_Name] = @Full_Name,
                                                 [Force_Change_Password] = @Force_Change_Password,
                                                 [Prefferred_Language] = @Prefferred_Language
                                           WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                    conn.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}

