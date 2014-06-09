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
    
    public partial class Screener
    {
        public Screener()
        {
            this.ScreenerGenres = new HashSet<ScreenerGenre>();
            this.Attributes = new HashSet<Attribute>();
        }
    
        public int ScreenerId { get; set; }
        public int FilmSubmissionId { get; set; }
        public int PostScreeningActionId { get; set; }
        public Nullable<int> FormatId { get; set; }
        public Nullable<long> DurationMinutes { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    
        public virtual FilmSubmission FilmSubmission { get; set; }
        public virtual User User { get; set; }
        public virtual Format Format { get; set; }
        public virtual PostScreeningAction PostScreeningAction { get; set; }
        public virtual ICollection<ScreenerGenre> ScreenerGenres { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}