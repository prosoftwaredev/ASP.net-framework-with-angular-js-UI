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
        private void SeedTitles(PayrollAppDbContext context)
        {
            context.Titles.AddOrUpdate(p => p.TitleID, new Title
            {
                TitleID = 1,
                TitleName = "Mr.",
                Gender = "Male",
                SortOrder = 1,
                Created = DateTime.Now,
                CreatedBy = 1,
                Remark = "--",
                IsDelete = false,
                IsEnable = true,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = 1,


            });
            context.Titles.AddOrUpdate(p => p.TitleID, new Title
            {
                TitleID = 2,
                TitleName = "Ms.",
                Gender = "Female",
                SortOrder = 1,
                Created = DateTime.Now,
                CreatedBy = 1,
                Remark = "--",
                IsDelete = false,
                IsEnable = true,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = 1
            });
            context.Titles.AddOrUpdate(p => p.TitleID, new Title
            {
                TitleID = 3,
                TitleName = "Mrs.",
                Gender = "Female",
                SortOrder = 1,
                Created = DateTime.Now,
                Remark = "--",
                CreatedBy = 1,
                IsDelete = false,
                IsEnable = true,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = 1,

            });
        }

       
       
    }
}
