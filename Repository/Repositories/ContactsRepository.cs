using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data.Entity;
using CaraEntites;

namespace Repository
{
    public class ContactsRepository:RepositoryBase
    {
         public List<CertificateMailing> GetMailingList(int filmSubmissionId)
        {
            return _context.CertificateMailings.Where(x => x.FilmSubmissionId == filmSubmissionId).ToList();
        }

        public CertificateMailing GetCertificateMailingRecipient(int certMailingId)
        {
            return _context.CertificateMailings.FirstOrDefault(x => x.CertificateMailingId == certMailingId);
        }

        public List<CertificateMailing> SaveMailingRecipient(CertificateMailing cert,int userId)
        {
            int filmSubmissionId = cert.FilmSubmissionId;
            CertificateMailing certOld = _context.CertificateMailings.FirstOrDefault(x => x.CertificateMailingId == cert.CertificateMailingId);
            if (certOld == null)    //new Recipient
            {
                cert.ModifiedBy = userId;
                cert.ModifiedDate = DateTime.Now;
                _context.CertificateMailings.Add(cert);
                _context.SaveChanges();
            }
            else
            {
                certOld.Address1 = cert.Address1;
                certOld.Address2 = cert.Address2;
                certOld.City = cert.City;
                certOld.Company = cert.Company;
                certOld.Country = cert.Country;
                certOld.Email = cert.Email;
                certOld.Fax = cert.Fax;
                certOld.FirstName = cert.FirstName;
                certOld.LastName = cert.LastName;
                certOld.Phone = cert.Phone;
                certOld.State = cert.State;
                certOld.Suffix = cert.Suffix;
                certOld.Title = cert.Title;
                certOld.Zip = cert.Zip;
                certOld.ModifiedDate = DateTime.Now;
                certOld.ModifiedBy = userId;
                _context.SaveChanges();
            }

            return _context.CertificateMailings.Where(x => x.FilmSubmissionId == filmSubmissionId).ToList();
        }

        public List<CertificateMailing> DeleteMailingRecipient(CertificateMailing cert, int userId)
        {
            int filmSubmissionId = cert.FilmSubmissionId;
            CertificateMailing certOld = _context.CertificateMailings.FirstOrDefault(x => x.CertificateMailingId == cert.CertificateMailingId);
            _context.CertificateMailings.Remove(certOld);
            _context.SaveChanges();

            return _context.CertificateMailings.Where(x => x.FilmSubmissionId == filmSubmissionId).ToList();
        }
    }
}
