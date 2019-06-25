using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
               
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (ApplicantEducationPoco item in items)
                    {
                        cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                       ([Id],[Applicant],[Major],[Certificate_Diploma],[Start_Date],[Completion_Date],[Completion_Percent])
                                 VALUES
                                        (@Id,@Applicant,@Major,@Certificate_Diploma,@Start_Date,@Completion_Date,@Completion_Percent)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                        cmd.Parameters.AddWithValue("@Major", item.Major);
                        cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                        cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);
                        
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                }
                
                
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT *
                                   FROM [JOB_PORTAL_DB].[dbo].[Applicant_Educations]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                ApplicantEducationPoco[] appPocos = new ApplicantEducationPoco[1000];

                while (reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Major = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    poco.CertificateDiploma = reader.GetString(3);
                    poco.StartDate = reader.IsDBNull(4) ? (DateTime?)null  : (DateTime?)reader.GetDateTime(4);
                    poco.CompletionDate = reader.IsDBNull(5) ? (DateTime?)null : (DateTime?)reader.GetDateTime(5);
                    poco.CompletionPercent = (byte?)reader[6];
                    poco.TimeStamp = (byte[])reader[7];
                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
            
        }
        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco item in items)
                {

                    cmd.CommandText = $"DELETE [dbo].[Applicant_Educations] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    conn.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    foreach (ApplicantEducationPoco item in items)
                    {
                        cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations] 
                                            SET [Major]= @Major,
                                                [Certificate_Diploma] = @Certificate_Diploma,
                                                [Start_Date]= @Start_Date,
                                                [Completion_Date] = @Completion_Date,
                                                [Completion_Percent]=@Completion_Percent
                                            WHERE [Id] = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Major", item.Major);
                        cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                        cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                        cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                        cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

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

