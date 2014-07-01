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
                addrOld = company.Address;
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

 
    }
}
