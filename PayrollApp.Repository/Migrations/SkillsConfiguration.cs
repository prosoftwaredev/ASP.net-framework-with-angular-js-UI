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
        private void SeedSkills(PayrollAppDbContext context)
        {
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 1,
                SkillName = "Asbestos Abatement",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 2,
                SkillName = "Flagging",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 3,
                SkillName = "Food servers",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 4,
                SkillName = "Swamping  Lumping",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 5,
                SkillName = "Forklifting",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 6,
                SkillName = "Welding",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 7,
                SkillName = "Data Entry General",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 8,
                SkillName = "Warehouse General",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });
            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 9,
                SkillName = "Customer Service General",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 10,
                SkillName = "Power Tools Grinding",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 11,
                SkillName = "Drivers license Valid",
                SortOrder = 1,
                IsEnable = true,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Remark = "--",
                IsDelete = false,
                CreatedBy = 1,
                LastUpdatedBy = 1
            });

            context.Skills.AddOrUpdate(p => p.SkillID, new Skill
            {
                SkillID = 12,
                SkillName = "Parts Counter Customer Service",
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
