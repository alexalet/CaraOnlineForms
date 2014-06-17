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
    
    public partial class Company
    {
        public Company()
        {
            this.CoContacts = new HashSet<CoContact>();
        }
    
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int FilmSubmissionId { get; set; }
        public int CorporateParentID { get; set; }
        public byte Member { get; set; }
        public string Phone { get; set; }
        public string FAX { get; set; }
        public int AddressID { get; set; }
        public string URL { get; set; }
        public int Valid { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<CoContact> CoContacts { get; set; }
        public virtual FilmSubmission FilmSubmission { get; set; }
    }
}
