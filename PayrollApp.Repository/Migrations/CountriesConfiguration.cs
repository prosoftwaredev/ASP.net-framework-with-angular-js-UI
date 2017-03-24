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
        private void SeedCountries(PayrollAppDbContext context)
        {
            context.Countries.AddOrUpdate(x => x.CountryID,
                new Country() { CountryID = 1, CountryCode = "CA", CountryName = "Canada", SortOrder = 1, IsEnable = true, Created = DateTime.Now, LastUpdated = DateTime.Now, Remark = "-", IsDelete = false, CreatedBy = 1, LastUpdatedBy = 1 },
                new Country() { CountryID = 2, CountryCode = "US", CountryName = "United States", SortOrder = 1, IsEnable = true, Created = DateTime.Now, LastUpdated = DateTime.Now, Remark = "-", IsDelete = false, CreatedBy = 1, LastUpdatedBy = 1 }
               );
        }

    }
}
