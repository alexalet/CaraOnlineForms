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
    
    public partial class RightsType
    {
        public RightsType()
        {
            this.CompanyRightsTypes = new HashSet<CompanyRightsType>();
        }
    
        public int RightsTypeId { get; set; }
        public string RightsTypeDescription { get; set; }
    
        public virtual ICollection<CompanyRightsType> CompanyRightsTypes { get; set; }
    }
}