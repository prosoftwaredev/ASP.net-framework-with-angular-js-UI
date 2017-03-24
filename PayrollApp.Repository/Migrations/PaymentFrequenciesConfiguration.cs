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
        private void SeedPayFrequencies(PayrollAppDbContext context)
        {
            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 1,
                PayFrequencyName = "Daily",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 2,
                PayFrequencyName = "Weekly",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 3,
                PayFrequencyName = "Biweekly",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });


            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 4,
                PayFrequencyName = "Monthly",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 5,
                PayFrequencyName = "Semimonthly",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 6,
                PayFrequencyName = "Yearly",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 7,
                PayFrequencyName = "10 pay periods",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 8,
                PayFrequencyName = "13 pay periods",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 9,
                PayFrequencyName = "22 pay periods",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 10,
                PayFrequencyName = "27 pay periods",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PayFrequencies.AddOrUpdate(p => p.PayFrequencyID, new PayFrequency
            {
                PayFrequencyID = 11,
                PayFrequencyName = "53 pay periods",
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
