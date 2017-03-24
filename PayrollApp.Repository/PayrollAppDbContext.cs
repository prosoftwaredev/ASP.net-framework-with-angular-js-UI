using PayrollApp.Core.Data.Entities;
using PayrollApp.Core.Data.System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PayrollApp.Repository
{
    public class PayrollAppDbContext : DbContext
    {

        public PayrollAppDbContext()
            : base("name=PayrollAppDbContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LastLogin> LastLogins { get; set; }
        public DbSet<ExcLogger> ExcLoggers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeNote> EmployeeNotes { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PayFrequency> PayFrequencies { get; set; }
        public DbSet<LabourClassification> LabourClassifications { get; set; }
        public DbSet<EmployeeLabourClassification> EmployeeLabourClassifications { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<EmployeeCertification> EmployeeCertifications { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<CustomerSite> CustomerSites { get; set; }
        public DbSet<CustomerSiteNote> CustomerSiteNotes { get; set; }
        public DbSet<SalesRep> SalesReps { get; set; }
        public DbSet<CustomerSiteLabourClassification> CustomerSiteLabourClassifications { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEquipment> OrderEquipments { get; set; }
        public DbSet<OrderTimeslip> OrderTimeslips { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CustomerSiteJobLocation> CustomerSiteJobLocations { get; set; }
        public DbSet<Preference> Preferences { get; set; }

        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<CustomerSite>()
            //        .HasRequired(m => m.PrCity)
            //        .WithMany(t => t.PrCustomerSite)
            //        .HasForeignKey(m => m.PrCityID)
            //        .WillCascadeOnDelete(false);

            //modelBuilder.Entity<CustomerSite>()
            //            .HasRequired(m => m.InCity)
            //            .WithMany(t => t.InCustomerSite)
            //            .HasForeignKey(m => m.InCityID)
            //            .WillCascadeOnDelete(false);      
        }
    }
}
