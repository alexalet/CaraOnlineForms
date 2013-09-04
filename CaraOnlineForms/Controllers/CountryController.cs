using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using ViewModels;

namespace CaraOnlineForms.Controllers
{
    public class CountryController : Controller
    {

        public PartialViewResult Countries(string name, string ISO)
        {

            var rep = new CountryRepository();
            var model = new CountryViewModel { Name = name, ISO = ISO };
            model.Countries = rep.GetCountries();
            return PartialView("_Countries", model);
        }

    }
}
