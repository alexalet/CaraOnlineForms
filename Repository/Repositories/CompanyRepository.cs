using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data.Entity;
using CaraEntites;

namespace Repository
{
    /// <summary>
    /// Retrieves the list of all companies (including Company Contacts) for the current Title
    /// </summary>
    public class CompanyRepository:RepositoryBase
    {
        public List<Company> Companies(int filmSubmissionId)
        {
            return _context.Companies
                .Include(x => x.Address)
                .Include(z=>z.RelType)
                .Include(u=>u.CompanyContacts)
                .Where(y => y.FilmSubmissionId == filmSubmissionId).ToList();
        }

        public List<RelType> RelTypes()
        {
            return _context.RelTypes.ToList();
        }

        public Company GetCompany(int companyId)
        {
            return _context.Companies
                .Include(x => x.Address)
                .Include(z => z.RelType)
                .Include(u => u.CompanyContacts)
                .FirstOrDefault(x => x.CompanyID == companyId);
        }

        public bool SaveCompanyInfo(Company company,int userId)
        {
            //first, save Address
            int addrId = 0;
            Address addrOld = _context.Addresses.FirstOrDefault(x => x.AddressID == company.Address.AddressID);
            if (addrOld==null)  //new Address
            {
                Address addrNew = company.Address;
                _context.Addresses.Add(addrNew);
                _context.SaveChanges();
                addrId = addrNew.AddressID;
            }
            else
            {
                addrOld .Addr1= company.Address.Addr1;
                addrOld.Addr2 = company.Address.Addr2;
                addrOld.City = company.Address.City;
                addrOld.State = company.Address.State;
                addrOld.Country = company.Address.Country;
                addrOld.PostalCode = company.Address.PostalCode;
                _context.SaveChanges();
            }
            Company compOld = _context.Companies.FirstOrDefault(x => x.CompanyID == company.CompanyID);
            if (compOld == null)
            {
                compOld = company;
                compOld.ModifiedDate = DateTime.Now;
                compOld.ModifiedBy = userId;
                _context.Companies.Add(compOld);
                _context.SaveChanges();
            }
            else
            {
                compOld.CompanyName = company.CompanyName;
                compOld.FAX = company.FAX;
                compOld.ModifiedBy = userId;
                compOld.ModifiedDate = DateTime.Now;
                compOld.Phone = company.Phone;
                compOld.RelTypeId = company.RelTypeId;
                _context.SaveChanges();

            }
            return true;
        }

        public bool DeleteCompany(int compId)
        {
            Company comp = _context.Companies.FirstOrDefault(x => x.CompanyID == compId);
            //delete all contacts, first
            List<CompanyContact> contacts = _context.CompanyContacts.Where(x => x.CompanyID == compId).ToList();
            foreach (CompanyContact c in contacts)
            {
                _context.CompanyContacts.Remove(c);
            }
            _context.SaveChanges();

            _context.Companies.Remove(comp);
            _context.SaveChanges();
            return true;
        }

        public CompanyContact GetCompanyContact(int contactId)
        {
            return _context.CompanyContacts
                .Include(x => x.Address)
                .Include(x=>x.ContactRole)
                .FirstOrDefault(x => x.ContactID == contactId);
        }

        public bool SaveCompanyContact(CompanyContact contact,int userId)
        {
            //first, save Address
            int addrId = 0;
            Address addrOld = _context.Addresses.FirstOrDefault(x => x.AddressID == contact.Address.AddressID);
            if (addrOld == null)  //new Address
            {
                Address addrNew = contact.Address;
                _context.Addresses.Add(addrNew);
                _context.SaveChanges();
                addrId = addrNew.AddressID;
            }
            else
            {
                addrOld.Addr1 = contact.Address.Addr1;
                addrOld.Addr2 = contact.Address.Addr2;
                addrOld.City = contact.Address.City;
                addrOld.State = contact.Address.State;
                addrOld.Country = contact.Address.Country;
                addrOld.PostalCode = contact.Address.PostalCode;
                _context.SaveChanges();
            }
            CompanyContact contOld = _context.CompanyContacts.FirstOrDefault(x => x.ContactID == contact.ContactID);
            if (contOld == null)
            {
                contOld = contact;
                contOld.ModifiedDate = DateTime.Now;
                contOld.ModifiedBy = userId;
                _context.CompanyContacts.Add(contOld);
                _context.SaveChanges();
            }
            else
            {
                contOld.Suffix = contact.Suffix;
                contOld.FirstName = contact.FirstName;
                contOld.MidName = contact.MidName;
                contOld.LastName = contact.LastName;
                contOld.eMail = contact.eMail;
                contOld.Phone1 = contact.Phone1;
                contOld.Phone2 = contact.Phone2;
                contOld.ModifiedBy = userId;
                contOld.ModifiedDate = DateTime.Now;
                
                contOld.RoleId = contact.RoleId;
                _context.SaveChanges();

            }
            return true;
        }

        public bool DeleteCompanyContact(int contactId)
        {
            CompanyContact cont = _context.CompanyContacts.FirstOrDefault(x => x.ContactID == contactId);
            _context.CompanyContacts.Remove(cont);
            _context.SaveChanges();
            return true;
        }
 
    }
}
