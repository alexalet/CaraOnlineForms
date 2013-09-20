using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaraEntites;

namespace Repository
{
    public class SubmitionProcessRepository : RepositoryBase
    {
        public List<FilmSubmission> GetFilmsForUser(int userId)
        {
            
            return _context.FilmSubmissions.Where(x=>x.UserId==userId && !x.Deleted).ToList();
        }


        public bool DeleteTitle(int filmSubmissionId, int userId)
        {
           FilmSubmission film = _context.FilmSubmissions.FirstOrDefault(x => x.UserId == userId && x.FilmSubmissionId == filmSubmissionId && x.Submitted == null);
           if (film == null)
               return false;
        
            film.Deleted = true;
           film.ModifiedBy = userId;
           film.Modified = DateTime.Now;
          _context.SaveChanges();
            
            return true;
        }
    }
}
