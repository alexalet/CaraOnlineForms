using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using CaraEntites;

namespace Repository
{
    public class FilmRepository:RepositoryBase
    {
        public FilmSubmission GetFilm(int filmId, int userId)
        {
            FilmSubmission res= _context.FilmSubmissions
                                        .Include("FilmAltTitles")
                                        //.Include
                                        .FirstOrDefault(x => x.FilmSubmissionId == filmId && x.UserId == userId && !x.Deleted && x.Submitted == null);
            if (res != null && res.Synopsis != null)
                res.Synopsis.Replace("\n","\r\n");

            return res;
        }

        public FilmSubmission SaveFilm(FilmSubmission newFilm, List<string> altTitles, int userId )
        {
            FilmSubmission film = null;
            if (newFilm.FilmSubmissionId >= 1)
            {
                film = GetFilm(newFilm.FilmSubmissionId, newFilm.UserId);
                if (film == null)
                    return null;
                film.FilmTitle = newFilm.FilmTitle;
                film.Synopsis = newFilm.Synopsis.Trim().Replace("\r", "");
                film.USFilmLocation = newFilm.USFilmLocation;
                film.CanadaFilmLocation = newFilm.CanadaFilmLocation;
                film.ForeignFilmLocation = newFilm.ForeignFilmLocation;
                film.IncludeOtherLanguage = newFilm.IncludeOtherLanguage;
                film.IsDistributedUnrated = newFilm.IsDistributedUnrated;
                film.DistributedUnrated = newFilm.DistributedUnrated;
                film.ModifiedDate = DateTime.Now;
                film.ModifiedBy = userId;
                _context.SaveChanges();

                foreach (FilmAltTitle at in film.FilmAltTitles.ToList())
                {
                    _context.FilmAltTitles.Remove(at);
                  
                }
                _context.SaveChanges();


                foreach (string str in altTitles)
                {
                    FilmAltTitle at = new FilmAltTitle { FilmSubmissionId = newFilm.FilmSubmissionId, AltTitle = str };
                    _context.FilmAltTitles.Add(at);
                    _context.SaveChanges();

                }
            }
            else
            {
                film = new FilmSubmission();
                film.UserId = newFilm.UserId;
                film.FilmTitle = newFilm.FilmTitle;
                film.Synopsis = newFilm.Synopsis.Trim().Replace("\r", "");
                film.USFilmLocation = newFilm.USFilmLocation;
                film.CanadaFilmLocation = newFilm.CanadaFilmLocation;
                film.ForeignFilmLocation = newFilm.ForeignFilmLocation;
                film.IncludeOtherLanguage = newFilm.IncludeOtherLanguage;
                film.IsDistributedUnrated = newFilm.IsDistributedUnrated;
                film.DistributedUnrated = newFilm.DistributedUnrated;
                film.CreatedDate = DateTime.Now;
                film.ModifiedDate = DateTime.Now;
                film.ModifiedBy = userId;
                _context.FilmSubmissions.Add(film);
                _context.SaveChanges();

                foreach (string str in altTitles)
                {
                    FilmAltTitle at = new FilmAltTitle { FilmSubmissionId = film.FilmSubmissionId, AltTitle = str };
                    _context.FilmAltTitles.Add(at);
                    _context.SaveChanges();

                }
            
            }

            return film;
        }

    }
}
