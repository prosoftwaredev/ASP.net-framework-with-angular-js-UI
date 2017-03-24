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
        private void SeedCustomers(PayrollAppDbContext context)
        {
            context.Customers.AddOrUpdate(x => x.CustomerID,
                new Customer()
                {
                    CustomerID = 1,
                    CustomerName = "Mathieu Cupryk",
                    CompanyName = "Example Company",
                    SalesRepID = 3,
                    PaymentTermID = 4,
                    RequirePO = true,
                    UniquePO = true,
                    PayByCC = false,
                    Delinquent = false,
                    InvoiceDiscountMessage = true,
                    HideCustomerName = true,
                    SortOrder = 1,
                    IsEnable = true,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Remark = "-",
                    IsDelete = false,
                    CreatedBy = 1,
                    LastUpdatedBy = 1
                }
                );
        }


    }
}
