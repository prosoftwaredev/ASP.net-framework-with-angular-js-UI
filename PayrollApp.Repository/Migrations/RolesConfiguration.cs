using PayrollApp.Core.Data.System;
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
        private void SeedRoles(PayrollAppDbContext context)
        {
            context.Roles.AddOrUpdate(p => p.RoleID, new Role
            {
                RoleID = 1,
                RoleName = "Admin",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1,
            });

            context.Roles.AddOrUpdate(p => p.RoleID, new Role
            {
                RoleID = 2,
                RoleName = "Clerk",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1,
            });

        }

      
    }
}
