using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;

namespace ViewModels
{
    public class CountryViewModel
    {
        public List<Country> Countries { get; set; }
        // selected country
        public string ISO { get; set; }
        // name of control
        public string Name { get; set; }
    
    }
}