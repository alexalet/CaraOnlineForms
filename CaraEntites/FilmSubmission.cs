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
    
    public partial class FilmSubmission
    {
        public FilmSubmission()
        {
            this.CertificateMailings = new HashSet<CertificateMailing>();
            this.Companies = new HashSet<Company>();
            this.FilmAltTitles = new HashSet<FilmAltTitle>();
            this.Participants = new HashSet<Participant>();
            this.Screeners = new HashSet<Screener>();
            this.CompanyRightsTypes = new HashSet<CompanyRightsType>();
        }
    
        public int FilmSubmissionId { get; set; }
        public string FilmTitle { get; set; }
        public int UserId { get; set; }
        public string ISAN { get; set; }
        public string ISANCheck { get; set; }
        public string EIDR { get; set; }
        public string OtherID { get; set; }
        public string Synopsis { get; set; }
        public string USFilmLocation { get; set; }
        public string CanadaFilmLocation { get; set; }
        public string ForeignFilmLocation { get; set; }
        public bool IncludeOtherLanguage { get; set; }
        public bool IsDistributedUnrated { get; set; }
        public string DistributedUnrated { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.DateTime> Submitted { get; set; }
        public Nullable<System.DateTime> AcceptedByCARA { get; set; }
        public Nullable<System.DateTime> ReturnedByCARA { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual ICollection<CertificateMailing> CertificateMailings { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<FilmAltTitle> FilmAltTitles { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Screener> Screeners { get; set; }
        public virtual ICollection<CompanyRightsType> CompanyRightsTypes { get; set; }
    }
}
