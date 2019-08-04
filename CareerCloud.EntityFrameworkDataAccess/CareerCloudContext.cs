using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
   public class CareerCloudContext : DbContext
    {
        public CareerCloudContext() : base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyDescriptions).WithRequired(d => d.CompanyProfile).HasForeignKey(d => d.Company).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyJobs).WithRequired(d => d.CompanyProfile).HasForeignKey(d => d.Company).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyLocations).WithRequired(d => d.CompanyProfile).HasForeignKey(d => d.Company).WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemLanguageCodePoco>().HasMany(s => s.CompanyDescriptions).WithRequired(l => l.SystemLanguageCode).HasForeignKey(l => l.LanguageId).WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyJobPoco>().HasMany(c => c.CompanyJobEducations).WithRequired(e => e.CompanyJob).HasForeignKey(e => e.Job).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyJobPoco>().HasMany(c => c.CompanyJobSkills).WithRequired(s => s.CompanyJob).HasForeignKey(s => s.Job).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyJobPoco>().HasMany(c => c.CompanyJobDescriptions).WithRequired(s => s.CompanyJob).HasForeignKey(s => s.Job).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyJobPoco>().HasMany(c => c.ApplicantJobApplications).WithRequired(s => s.CompanyJob).HasForeignKey(s => s.Job).WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicantProfilePoco>().HasMany(c => c.ApplicantJobApplications).WithRequired(s => s.ApplicantProfile).HasForeignKey(s => s.Applicant).WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicantProfilePoco>().HasMany(c => c.ApplicantWorkHistories).WithRequired(s => s.ApplicantProfile).HasForeignKey(s => s.Applicant).WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicantProfilePoco>().HasMany(c => c.ApplicantSkills).WithRequired(s => s.ApplicantProfile).HasForeignKey(s => s.Applicant).WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicantProfilePoco>().HasMany(c => c.ApplicantResumes).WithRequired(s => s.ApplicantProfile).HasForeignKey(s => s.Applicant).WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicantProfilePoco>().HasMany(c => c.ApplicantEducations).WithRequired(s => s.ApplicantProfile).HasForeignKey(s => s.Applicant).WillCascadeOnDelete(true);

            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyDescriptions).WithRequired(s => s.CompanyProfile).HasForeignKey(s => s.Company).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyJobs).WithRequired(s => s.CompanyProfile).HasForeignKey(s => s.Company).WillCascadeOnDelete(true);
            modelBuilder.Entity<CompanyProfilePoco>().HasMany(c => c.CompanyLocations).WithRequired(s => s.CompanyProfile).HasForeignKey(s => s.Company).WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityLoginPoco>().HasMany(c => c.ApplicantProfiles).WithRequired(s => s.SecurityLogin).HasForeignKey(s => s.Login).WillCascadeOnDelete(true);
            modelBuilder.Entity<SecurityLoginPoco>().HasMany(c => c.SecurityLoginsLogs).WithRequired(s => s.SecurityLogin).HasForeignKey(s => s.Login).WillCascadeOnDelete(true);
            modelBuilder.Entity<SecurityLoginPoco>().HasMany(c => c.SecurityLoginsRoles).WithRequired(s => s.SecurityLogin).HasForeignKey(s => s.Login).WillCascadeOnDelete(true);

            modelBuilder.Entity<SecurityRolePoco>().HasMany(c => c.SecurityLoginsRoles).WithRequired(s => s.SecurityRole).HasForeignKey(s => s.Role).WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemCountryCodePoco>().HasMany(c => c.ApplicantProfiles).WithRequired(s => s.SystemCountryCode).HasForeignKey(s => s.Country).WillCascadeOnDelete(true);
            modelBuilder.Entity<SystemCountryCodePoco>().HasMany(c => c.ApplicantWorkHistories).WithRequired(s => s.SystemCountryCode).HasForeignKey(s => s.CountryCode).WillCascadeOnDelete(true);

            modelBuilder.Entity<SystemLanguageCodePoco>().HasMany(c => c.CompanyDescriptions).WithRequired(s => s.SystemLanguageCode).HasForeignKey(s => s.LanguageId).WillCascadeOnDelete(true);


            modelBuilder.Entity<ApplicantEducationPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<ApplicantProfilePoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<ApplicantSkillPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyJobEducationPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyJobPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyJobSkillPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyLocationPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<CompanyProfilePoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<SecurityLoginPoco>().Ignore(s => s.TimeStamp);
            modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore(s => s.TimeStamp);


            base.OnModelCreating(modelBuilder);
        }

        
        DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


    }
}
