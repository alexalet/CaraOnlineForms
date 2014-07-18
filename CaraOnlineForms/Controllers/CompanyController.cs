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
                model = new Company { CompanyID = -1, FilmSubmissionId = filmId, Address = new Address { AddressID = -1 }};
                //List<CompanyRightsType> rightsTypes=new List<CompanyRightsType>{new CompanyRightsType{

            }
            
            return PartialView("_CompanyEdit", model);
        }

        public JsonResult SaveCompanyInfo(Company company, List<int> CompanyRightsTypes)
        {
            NameValueCollection errors = new NameValueCollection();
            var res = new CompanyRepository().SaveCompanyInfo(company, CompanyRightsTypes, User.UserId);
            ViewBag.Message = "Changes have been saved";
            return Json(new
            {
                Success = errors.Count == 0//,
                //Errors = (ModelState.IsValid ? null : ModelState.Errors())
            },
            JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteCompany(int companyId)
        {
            NameValueCollection errors = new NameValueCollection();
            var res = new CompanyRepository().DeleteCompany(companyId);

            ViewBag.Message = "Company has been deleted";
            return Json(new
            {
                Success = errors.Count == 0//,
                //Errors = (ModelState.IsValid ? null : ModelState.Errors())
            },
            JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult EditCompanyContact(int companyId, int contactId, int filmId)
        {
            CompanyContact model = new CompanyRepository().GetCompanyContact(contactId);
            if (model == null)
            {
                model = new CompanyContact { ContactID = -1, CompanyID = companyId, 
                    FilmSubmissionId = filmId, Address = new Address { AddressID = -1 },
                    ModifiedBy=User.UserId,ModifiedDate=DateTime.Now };
            }

            return PartialView("_CompanyContactEdit", model);
        }

        public JsonResult SaveCompanyContact(CompanyContact contact)
        {
            NameValueCollection errors = new NameValueCollection();
            var res = new CompanyRepository().SaveCompanyContact(contact, User.UserId);
            ViewBag.Message = "Changes have been saved";
            return Json(new
            {
                Success = errors.Count == 0//,
                //Errors = (ModelState.IsValid ? null : ModelState.Errors())
            },
            JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteCompanyContact(int contactId)
        {
            NameValueCollection errors = new NameValueCollection();
            var res = new CompanyRepository().DeleteCompanyContact(contactId);

            ViewBag.Message = "Contact has been deleted";
            return Json(new
            {
                Success = errors.Count == 0//,
                //Errors = (ModelState.IsValid ? null : ModelState.Errors())
            },
            JsonRequestBehavior.AllowGet);
        }

    }
}
