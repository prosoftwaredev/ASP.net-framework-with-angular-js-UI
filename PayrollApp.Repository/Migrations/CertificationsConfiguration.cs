using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Repository.Migrations
{
    internal sealed partial class Configuration : DbMigrationsConfiguration<PayrollAppDbContext>
    {
        private void SeedCertifications(PayrollAppDbContext context)
        {
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 1,
                CertificationName = "Constr Safety Officer",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 2,
                CertificationName = "Driver - Class 1",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 3,
                CertificationName = "Driver - Class 1 with air",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 4,
                CertificationName = "Driver - Class 3 with air",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 5,
                CertificationName = "Driver - Class 5 with air",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 6,
                CertificationName = "Driver - Class 3",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 7,
                CertificationName = "Driver - Class 5",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 8,
                CertificationName = "Electrician ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 9,
                CertificationName = "Fall Arrest",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 10,
                CertificationName = "Fit test for respirator",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 11,
                CertificationName = "Flagger ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 12,
                CertificationName = "Forklift ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 13,
                CertificationName = "Hearing test",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 14,
                CertificationName = "Inter Prov Carpentry",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 15,
                CertificationName = "Lockout/Tagout",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 16,
                CertificationName = "Manhoist ticket - Inside",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 17,
                CertificationName = "Manhoist ticket - Outside",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 18,
                CertificationName = "OFA - Level 1",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 19,
                CertificationName = "OFA - Level 2",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 20,
                CertificationName = "OFA - Level 3",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 21,
                CertificationName = "Plumbing ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 22,
                CertificationName = "Safety Monitor",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 23,
                CertificationName = "Safety Training (Vic)",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 24,
                CertificationName = "Traffic control Ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 25,
                CertificationName = "Welding ticket",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Certifications.AddOrUpdate(p => p.CertificationID, new Certification
            {
                CertificationID = 26,
                CertificationName = "WHIMIS Training",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
        }

    }
}
