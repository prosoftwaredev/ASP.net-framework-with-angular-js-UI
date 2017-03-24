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
        private void SeedLabourClassifications(PayrollAppDbContext context)
        {
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 1,
                LabourClassificationName = "_Flagger",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 2,
                LabourClassificationName = "_Special",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 3,
                LabourClassificationName = "_Special 1",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 4,
                LabourClassificationName = "_Special 2",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 5,
                LabourClassificationName = "_Special 3",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 6,
                LabourClassificationName = "_Special 5",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 7,
                LabourClassificationName = "_Special 6",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 8,
                LabourClassificationName = "Asbestos Abatement",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 9,
                LabourClassificationName = "Building Maint General",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 10,
                LabourClassificationName = "Forklift",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 11,
                LabourClassificationName = "General Labour",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 12,
                LabourClassificationName = "General Labour0",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 13,
                LabourClassificationName = "Heavy Construction ICI",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 14,
                LabourClassificationName = "Heavy Construction NonICI",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 15,
                LabourClassificationName = "Load / Unload",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.LabourClassifications.AddOrUpdate(p => p.LabourClassificationID, new LabourClassification
            {
                LabourClassificationID = 16,
                LabourClassificationName = "Load / Unload0",
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
