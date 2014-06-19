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
        /// <summary>
        /// Retrieves the list of all Certificate Mailing (Right Holder) Recipients info by the film Id
        /// </summary>
        /// <param name="filmId"></param>
        /// <returns></returns>
        public ActionResult GetMailingList(int filmId)
        {
            List<CertificateMailing> mailingList = new ContactsRepository().GetMailingList(filmId);

            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = filmId, Suffix = "" });
            return View("Contacts", mailingList);
        }

        public ActionResult SaveRecipient(int FilmSubmissionId, int CertificateMailingId, string FirstName, string LastName, string Suffix, string Title, string Company, string Address1, string Address2, string City, string State, string Zip, string Country, string Phone, string Fax, string Email,bool Original)
        {
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
                Email=Email,
                Original=Original
            };
            List<CertificateMailing> mailingList = new ContactsRepository().SaveMailingRecipient(cert, this.User.UserId);

            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = FilmSubmissionId, Suffix = "" });

            ViewBag.Message = "The Contact has been saved";
            return View("Contacts", mailingList);
        }

        public ActionResult DeleteRecipient(int FilmSubmissionId,int CertificateMailingId)
        {
            CertificateMailing cert = new CertificateMailing { FilmSubmissionId=FilmSubmissionId, CertificateMailingId = CertificateMailingId };
            List<CertificateMailing> mailingList = new ContactsRepository().DeleteMailingRecipient(cert, this.User.UserId);
            mailingList.Add(new CertificateMailing { CertificateMailingId = -1, FilmSubmissionId = cert.FilmSubmissionId, Suffix = "" });
            ViewBag.Message = "The Contact has been deleted";
            return View("Contacts", mailingList);
        }
    }
}
