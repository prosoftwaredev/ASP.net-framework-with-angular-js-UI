using PayrollApp.Core.Data.Entities;
using System;
using System.Data.Entity.Migrations;

namespace PayrollApp.Repository.Migrations
{
    internal sealed partial class Configuration : DbMigrationsConfiguration<PayrollAppDbContext>
    {
        private void SeedSalesRepInitial(PayrollAppDbContext context)
        {
            context.SalesReps.AddOrUpdate(p => p.SalesRepID, new SalesRep
            {
                SalesRepID = 1,
                SalesRepName = "NOCOM",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.SalesReps.AddOrUpdate(p => p.SalesRepID, new SalesRep
            {
                SalesRepID = 2,
                SalesRepName = "AM",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.SalesReps.AddOrUpdate(p => p.SalesRepID, new SalesRep
            {
                SalesRepID = 3,
                SalesRepName = "JN",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.SalesReps.AddOrUpdate(p => p.SalesRepID, new SalesRep
            {
                SalesRepID = 4,
                SalesRepName = "GH",
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
