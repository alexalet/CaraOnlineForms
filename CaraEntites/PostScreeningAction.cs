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
    
    public partial class PostScreeningAction
    {
        public PostScreeningAction()
        {
            this.Screeners = new HashSet<Screener>();
        }
    
        public int PostScreeningActionId { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Screener> Screeners { get; set; }
    }
}