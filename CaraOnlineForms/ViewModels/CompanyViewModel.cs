using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public List<RelType> CompanyTypes { get; set; }
    }

    public class CompaniesViewModel
    {
        public CompaniesViewModel()
        {
            Companies = new List<Company>();
        }
        public int FilmSubmissionId { get; set; }
        public List<Company> Companies { get; set; }
    }
}