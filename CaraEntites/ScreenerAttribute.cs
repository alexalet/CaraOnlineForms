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
    
    public partial class ScreenerAttribute
    {
        public int ScreenerAttributeId { get; set; }
        public int ScreenerId { get; set; }
        public int AttributeId { get; set; }
    
        public virtual Attribute Attribute { get; set; }
        public virtual Screener Screener { get; set; }
    }
}
