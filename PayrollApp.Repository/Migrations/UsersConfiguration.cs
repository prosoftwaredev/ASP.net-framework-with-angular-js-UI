using PayrollApp.Core.Data.System;
using System;
using System.Data.Entity.Migrations;

namespace PayrollApp.Repository.Migrations
{
    internal sealed partial class Configuration : DbMigrationsConfiguration<PayrollAppDbContext>
    {
        private void SeedUsers(PayrollAppDbContext context)
        {
            context.Users.AddOrUpdate(p => p.UserID, new User
            {
                UserID = 1,
                Firstname = "Mathieu",
                Lastname = "Cupryk",
                Email = "admin@admin.com",
                Password = "01b307acba4f54f55aafc33bb06bbbf6ca803e9a",
                Phone = "2043396704",
                Gender = "Male",
                Picture = "",
                DOB = new DateTime(1973, 10, 18),
                Hash = "",
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
