using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PayrollApp.Core.Data.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public long EmployeeID { get; set; }

        //public long EmployeeTypeID { get; set; }

        public long? CityID { get; set; }

        public long? PayFrequencyID { get; set; }

        public long TitleID { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(2)]
        public string MiddleName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(99)]
        public string PrintName { get; set; }   //

        [StringLength(99)]
        public string AccountNo { get; set; }

        [StringLength(99)]
        public string EmailMain { get; set; }   //

        [StringLength(99)]
        public string EmailCC { get; set; }   //

        [StringLength(99)]
        public string Website { get; set; }   //

        [StringLength(255)]
        public string Other { get; set; }   //

        [StringLength(21)]
        public string Phone { get; set; }

        [StringLength(21)]
        public string Mobile { get; set; }

        [StringLength(21)]
        public string Fax { get; set; }

        [StringLength(41)]
        public string Address1 { get; set; }

        [StringLength(41)]
        public string Address2 { get; set; }

        [StringLength(13)]
        public string PostalCode { get; set; }

        [StringLength(99)]
        public string NextOfKin { get; set; }

        [StringLength(99)]
        public string NextOfKinContact { get; set; }

        [StringLength(15)]
        public string SIN { get; set; }

        public DateTime? DOB { get; set; }

        public string Gender { get; set; }

        public decimal Balance { get; set; }

        public decimal Withholding { get; set; }

        public decimal Dormant { get; set; }

        public DateTime? PayStubs { get; set; }

        public bool Garnishee { get; set; }

        public bool IsNeverUse { get; set; }

        public string XmlNote { get; set; }

        //public virtual EmployeeType EmployeeType { get; set; }

        public virtual PayFrequency PayFrequency { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<EmployeeNote> EmployeeNotes { get; set; }

        public virtual ICollection<EmployeeLabourClassification> EmployeeLabourClassifications { get; set; }

        public virtual ICollection<EmployeeBlacklist> EmployeeBlacklists { get; set; }

        public virtual ICollection<EmployeeCertification> EmployeeCertifications { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
    }
}
