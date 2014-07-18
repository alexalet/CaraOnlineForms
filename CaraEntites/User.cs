//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaraEntites
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.UserToConfirms = new HashSet<UserToConfirm>();
            this.PasswordToResets = new HashSet<PasswordToReset>();
            this.Screeners = new HashSet<Screener>();
            this.Companies = new HashSet<Company>();
            this.CompanyContacts = new HashSet<CompanyContact>();
        }
    
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CountryId { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<byte> PasswordRetriesCount { get; set; }
        public string Password { get; set; }
        public Nullable<int> ParentCompanyId { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<UserToConfirm> UserToConfirms { get; set; }
        public virtual ICollection<PasswordToReset> PasswordToResets { get; set; }
        public virtual ICollection<Screener> Screeners { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
