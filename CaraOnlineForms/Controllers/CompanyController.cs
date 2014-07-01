using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;
using ViewModels;

namespace CaraOnlineForms.Controllers
{
    public class CompanyController : ControllerBase
    {
        //
        // GET: /Company/

        public ActionResult Companies(int filmId)
        {
            CompaniesViewModel model = new CompaniesViewModel { FilmSubmissionId = filmId, Companies = new CompanyRepository().Companies(filmId) };

            return View("Companies",model);
        }

        /// <summary>
        /// retrieves Cara Company information for editing
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public PartialViewResult EditCompanyInfo(int companyId,int filmId)
        {
            Company model = new CompanyRepository().GetCompany(companyId);
            if (model == null)
            {
                model = new Company { CompanyID = -1, FilmSubmissionId = filmId, Address = new Address { AddressID = -1 } };
            }
            return PartialView("_CompanyEdit", model);
        }

        public JsonResult SaveCompanyInfo(Company company)
        {
            NameValueCollection errors = new NameValueCollection();
            var res = new CompanyRepository().SaveCompanyInfo(company,User.UserId);
            return Json(new
            {
                Success = errors.Count == 0//,
                //Errors = (ModelState.IsValid ? null : ModelState.Errors())
            },
            JsonRequestBehavior.AllowGet);

        }

    }
}
