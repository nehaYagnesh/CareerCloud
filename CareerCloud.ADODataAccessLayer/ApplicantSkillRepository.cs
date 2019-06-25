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
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (ApplicantSkillPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                                        ([Id],[Applicant],[Skill],[Skill_Level],[Start_Month],[Start_Year],[End_Month],[End_Year])
                                            VALUES (@Id,@Applicant,@Skill,@Skill_Level,@Start_Month,@Start_Year,@End_Month,@End_Year)";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                        cmd.Parameters.AddWithValue("@Skill", item.Skill);
                        cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * 
                                   FROM [JOB_PORTAL_DB].[dbo].[Applicant_Skills]";
                conn.Open();
                int position = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                ApplicantSkillPoco[] appPocos = new ApplicantSkillPoco[1000];

                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = reader.GetGuid(1);
                    poco.Skill = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    poco.SkillLevel = reader.IsDBNull(3) ? string.Empty :  reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);
                    poco.TimeStamp = (byte[])reader[8];

                    appPocos[position] = poco;
                    position++;
                }

                return appPocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (ApplicantSkillPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"DELETE FROM  [dbo].[Applicant_Skills]
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

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString))
            {
                try
                {
                    foreach (ApplicantSkillPoco item in items)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                            SET  [Applicant] = @Applicant,
                                                 [Skill] = @Skill,
                                                 [Skill_Level] = @Skill_Level,
                                                 [Start_Month] = @Start_Month,
                                                 [Start_Year] = @Start_Year,
                                                 [End_Month] = @End_Month,
                                                 [End_Year] = @End_Year
                                            WHERE Id = @Id";
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                        cmd.Parameters.AddWithValue("@Skill", item.Skill);
                        cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                        cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                        cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                        cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                        cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

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
