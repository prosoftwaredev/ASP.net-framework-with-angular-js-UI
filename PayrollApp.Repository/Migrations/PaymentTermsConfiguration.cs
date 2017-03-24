using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollApp.Repository.Migrations
{
    internal sealed partial class Configuration: DbMigrationsConfiguration<PayrollAppDbContext>
    {
        private void SeedPaymentTerms(PayrollAppDbContext context)
        {
            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 1,
                PaymentTermName = "0",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 2,
                PaymentTermName = "0% Net 15",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 3,
                PaymentTermName = "1% 10 Net 30", // Default Value
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 3,
                PaymentTermName = "1% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 4,
                PaymentTermName = "10% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 5,
                PaymentTermName = "2% 10 Net 30",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 6,
                PaymentTermName = "2% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 7,
                PaymentTermName = "3% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 8,
                PaymentTermName = "4% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 9,
                PaymentTermName = "5% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 10,
                PaymentTermName = "6% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 11,
                PaymentTermName = "7% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 12,
                PaymentTermName = "8% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 13,
                PaymentTermName = "9% NET 7",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });


            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 14,
                PaymentTermName = "Consignment",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 15,
                PaymentTermName = "Due on receipt",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 16,
                PaymentTermName = "Net 15",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 17,
                PaymentTermName = "Net 30",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 18,
                PaymentTermName = "Net 60",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.PaymentTerms.AddOrUpdate(p => p.PaymentTermID, new PaymentTerm
            {
                PaymentTermID = 19,
                PaymentTermName = "NET 7",
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
