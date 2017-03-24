
using PayrollApp.Core.Data.Entities;
using System;
using System.Data.Entity.Migrations;
namespace PayrollApp.Repository.Migrations
{
    internal sealed partial class Configuration : DbMigrationsConfiguration<PayrollAppDbContext>
    {
        private void SeedEquiments(PayrollAppDbContext context)
        {
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 1,
                EquipmentName = "Bus Ticket",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 2,
                EquipmentName = "Hard Hat",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 3,
                EquipmentName = "Boots",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 4,
                EquipmentName = "Gloves",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 5,
                EquipmentName = "Glasses",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 6,
                EquipmentName = "Taxi",
                Rate = 10.00M,
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Equipments.AddOrUpdate(p => p.EquipmentID, new Equipment
            {
                EquipmentID = 7,
                EquipmentName = "Vest",
                Rate = 10.00M,
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
