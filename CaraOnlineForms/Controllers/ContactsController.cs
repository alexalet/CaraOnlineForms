using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;
using ViewModels;

namespace CaraOnlineForms.Controllers
{
    public class ContactsController : ControllerBase
    {
        public ActionResult GetMailingList(int filmId)
        {
            List<CertificateMailing> mailingList = new ContactsRepository().GetMailingList(filmId);
            List<CertificateMailingViewModel> model = new List<CertificateMailingViewModel>();

            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = filmId, Suffix = "" });
            for (int i=0;i<mailingList.Count;i++)
            {
                CertificateMailingViewModel certModel = new CertificateMailingViewModel { Recipient = mailingList[i], Counter = i };
                model.Add(certModel);
            }
            return View("Contacts", model);

        }

        public ActionResult SaveRecipient(int FilmSubmissionId, int CertificateMailingId, string FirstName, string LastName, string Suffix, string Title, string Company, string Address1, string Address2, string City, string State, string Zip, string Country, string Phone, string Fax, string Email)
        {
            List<CertificateMailingViewModel> model = new List<CertificateMailingViewModel>();
            CertificateMailing cert = new CertificateMailing { FilmSubmissionId = FilmSubmissionId, 
                CertificateMailingId = CertificateMailingId, 
                FirstName = FirstName, 
                LastName = LastName, 
                Suffix = Suffix, 
                Title = Title, 
                Company = Company, 
                Address1 = Address1, 
                Address2 = Address2, 
                City = City, 
                State = State, 
                Zip = Zip, 
                Country = Country, 
                Phone = Phone, 
                Fax = Fax, 
                Email=Email };
            List<CertificateMailing> mailingList = new ContactsRepository().SaveMailingRecipient(cert, this.User.UserId);

            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = FilmSubmissionId, Suffix = "" });
            for (int i = 0; i < mailingList.Count; i++)
            {
                CertificateMailingViewModel certMod = new CertificateMailingViewModel { Recipient = mailingList[i], Counter = i };
                model.Add(certMod);
            }
            ViewBag.Message = "The Contact has been saved";
            return View("Contacts", model);
        }


        public ActionResult DeleteRecipient(int FilmSubmissionId,int CertificateMailingId)
        {
            List<CertificateMailingViewModel> model = new List<CertificateMailingViewModel>();
            CertificateMailing cert = new CertificateMailing { FilmSubmissionId=FilmSubmissionId, CertificateMailingId = CertificateMailingId };
            List<CertificateMailing> mailingList = new ContactsRepository().DeleteMailingRecipient(cert, this.User.UserId);
            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = cert.FilmSubmissionId, Suffix = "" });
            ViewBag.Message = "The Contact has been deleted";
            for (int i = 0; i < mailingList.Count; i++)
            {
                CertificateMailingViewModel certMod = new CertificateMailingViewModel { Recipient = mailingList[i], Counter = i };
                model.Add(certMod);
            }
            return View("Contacts", model);
        }

    }
}
