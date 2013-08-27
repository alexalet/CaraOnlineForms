using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using CaraEntites;



namespace Repository
{
    public class UserRepository: RepositoryBase
    {

        public bool IsEmailUnique(string email)
        {
            var _context = new CaraEntities();
            var u = _context.Users.Where(x => x.EmailAddress == email).ToList();
            return u.Count == 0;
        }
    }
}
