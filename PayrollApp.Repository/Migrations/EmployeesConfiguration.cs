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
        private void SeedEmployees(PayrollAppDbContext context)
        {
            context.Employees.AddOrUpdate(x => x.EmployeeID,
                new Employee
                {
                    EmployeeID = 1,
                    CityID = 38445,
                    PayFrequencyID = 3,
                    TitleID = 1,
                    FirstName = "Mathieu",
                    MiddleName = "A",
                    LastName = "Cupryk",
                    PrintName = "Mathieu A Cupryk",
                    AccountNo = "LOH001",
                    EmailMain = "mathieu_cupryk@hotmail.com",
                    Phone = "+1 (213) 221 3213",
                    Mobile = "+1 (321) 321 3213",
                    Address1 = "111",
                    PostalCode = "R2W",
                    SIN = "192334506",
                    DOB = new DateTime(1989, 4, 21),
                    Gender = "Male",
                    PayStubs = new DateTime(2017, 2, 16),
                    SortOrder = 1,
                    IsEnable = true,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Remark = "-",
                    IsDelete = false,
                    CreatedBy = 1,
                    LastUpdatedBy = 1
                });
        }

    }
}
