using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data.Entity;
using CaraEntites;


namespace Repository
{
    public class ScreenerRepository:RepositoryBase
    {
        /// <summary>
        /// retrieves the Screener for the selected submitted film
        /// </summary>
        /// <param name="filmSubId"></param>
        /// <returns>can be null</returns>
        public Screener GetScreener(int filmSubId)
        {
            return _context.Screeners
                .Include(x => x.ScreenerGenres.Select(g => g.Genre))
                .Include(a => a.Attributes)
                .Include(f => f.Format)
                .Include(p => p.PostScreeningAction)
                .Include(u => u.User)
                .FirstOrDefault(x => x.FilmSubmissionId == filmSubId);
        }

        public NameValueCollection AddScreener(Screener newScreener, List<int> selectedGenres, List<int> selectedAttributes, int userId)
        {
            NameValueCollection result = new NameValueCollection();
            bool isNew = false;
            int screenerId;

            Screener screener = GetScreener(newScreener.FilmSubmissionId);
            if (screener == null)
            {
                isNew = true;
                newScreener.User.UserId = userId;
                newScreener.ModifiedDate = DateTime.Now;
                screener.ModifiedBy = userId;

                _context.Screeners.Add(newScreener);
            }
            else
            {
                screener.FormatId = newScreener.FormatId;
                screener.DurationMinutes = newScreener.DurationMinutes;
                screener.PostScreeningActionId = newScreener.PostScreeningActionId;

                screener.ModifiedDate = DateTime.Now;
                screener.ModifiedBy = userId;


            }
            _context.SaveChanges();
            if (isNew)
                screenerId = newScreener.ScreenerId;
            else
                screenerId = screener.ScreenerId;

            // delete previous genres
            var prevGenres = _context.ScreenerGenres.Where(x => x.ScreenerId == screenerId);
            foreach (var pg in prevGenres)
            {
                _context.ScreenerGenres.Remove(pg);
            }
            _context.SaveChanges();
            // add new genres
            if (selectedGenres != null)
            {
                selectedGenres.ForEach(g => _context.ScreenerGenres.Add(new ScreenerGenre() { ScreenerId = screenerId, GenreId = g }));
                _context.SaveChanges();
            }

            // delete previous attributes
            if (screener != null) // it is existing screener
            {
                screener.Attributes.Clear();
                _context.SaveChanges();
            }

            // add new attributes
            if (selectedAttributes != null)
            {
                foreach (var sa in selectedAttributes)
                {
                    var attr = _context.Attributes.Find(sa);
                    attr.Screeners.Add(screener ?? newScreener);

                }
                _context.SaveChanges();
            }
            return result;
        }

        /// <summary>
        /// returns list of all Formats with empty format  order by Description
        /// </summary>
        /// <returns></returns>
        public List<Format> GetListOfFormats()
        {
            return _context.Formats.OrderBy(x => x.Description).ToList();
        }


        /// <summary>
        /// returns list of all Genres order by Description
        /// </summary>
        /// <returns></returns>
        public List<Genre> GetListOfGenres()
        {
            return _context.Genres.OrderBy(x => x.Description).ToList();
        }


        /// <summary>
        /// returns list of all Attributes order by description
        /// </summary>
        /// <returns></returns>
        public List<CaraEntites.Attribute> GetListOfAttributes()
        {
            return _context.Attributes.OrderBy(x => x.Description).ToList();
        }

    }
}
