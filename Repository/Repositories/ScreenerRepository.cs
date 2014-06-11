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
                //.Include(x => x.ScreenerGenres.Select(g => g.Genre))
                //.Include(a => a.Attributes)
                .Include(f => f.Format)
                .Include(p => p.PostScreeningAction)
                .Include(u => u.User)
                .FirstOrDefault(x => x.FilmSubmissionId == filmSubId);
        }

        public Screener AddScreener(Screener newScreener, List<int> selectedGenres, List<int> selectedAttributes, int userId)
        {
            int screenerId;
            Screener screenerOld = GetScreener(newScreener.FilmSubmissionId);
            try
            {
                
                if (screenerOld == null)
                {
                    screenerOld = new Screener();
                    screenerOld.FilmSubmissionId = newScreener.FilmSubmissionId;
                    screenerOld.ModifiedBy = userId;
                    screenerOld.FormatId = newScreener.FormatId;
                    screenerOld.PostScreeningActionId = newScreener.PostScreeningActionId;
                    screenerOld.DurationMinutes = (newScreener.DurationMinutes ?? 0);
                    screenerOld.ModifiedDate = DateTime.Now;

                    _context.Screeners.Add(screenerOld);
                }
                else
                {
                    screenerOld.FormatId = newScreener.FormatId;
                    screenerOld.DurationMinutes = newScreener.DurationMinutes;
                    screenerOld.PostScreeningActionId = newScreener.PostScreeningActionId;

                    screenerOld.ModifiedDate = DateTime.Now;
                    screenerOld.ModifiedBy = userId;


                }
                _context.SaveChanges();

                screenerId = screenerOld.ScreenerId;

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
                    foreach (var sg in selectedGenres)
                    {
                        var gnr = _context.Genres.Find(sg);
                        _context.ScreenerGenres.Add(new ScreenerGenre{ScreenerId=screenerId,GenreId=gnr.GenreId});
                        _context.SaveChanges();
                    }
                }

                //// delete previous attributes
                var prevAttr = _context.ScreenerAttributes.Where(x => x.ScreenerId == screenerId);
                foreach (var pa in prevAttr)
                {
                    _context.ScreenerAttributes.Remove(pa);
                }
                _context.SaveChanges();

                // add new attributes
                if (selectedAttributes != null)
                {
                    foreach (var sa in selectedAttributes)
                    {
                        var attr = _context.Attributes.Find(sa);
                        _context.ScreenerAttributes.Add(new ScreenerAttribute { ScreenerId = screenerId, AttributeId = attr.AttributeId });
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message + ", " + ex.InnerException.InnerException.Message;
            }
            return screenerOld;
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
