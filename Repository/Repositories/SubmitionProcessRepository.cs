using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaraEntites;

namespace Repository
{
    public class SubmitionProcessRepository : RepositoryBase
    {
        public List<Film> GetFilmsForUser(int userId)
        {
            
            return _context.Films.Where(x=>x.UserId==userId && !x.Deleted).ToList();
        }
        
    }
}
