using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CaraEntites;

namespace Repository
{
    public class CountryRepository:RepositoryBase
    {
        public List<Country> GetCountries()
        {
            return _context.Countries.Where(x => x.Active == true).OrderBy(y => y.Name).ToList();
        }
    }
}
