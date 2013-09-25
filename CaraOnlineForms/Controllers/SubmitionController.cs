using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;

namespace CaraOnlineForms.Controllers
{
    public class SubmitionController : ControllerBase
    {

        
        public ActionResult Title(int? filmId)
        {
            FilmSubmission model = new FilmSubmission { FilmAltTitles = new List<FilmAltTitle>() };
            if (filmId == null) // new Title
            {
                this.CurrentFilm = null;
                return View(model);
            }

            model = new FilmRepository().GetFilm(filmId.Value, User.UserId);
            this.CurrentFilm = model; // save the selected film in session

            return View(model);
        }

        public ActionResult SaveTitle(FilmSubmission film, List<string> AltTitle, string NextPage)
        {
            // UserID of the saved film must be taken from session
            film.UserId = this.User.UserId;
            
            // FilmID of the saved film also must be taken from  session 
            if (this.CurrentFilm != null) // else FilmId=0 (means new Film)
                film.FilmSubmissionId = this.CurrentFilm.FilmSubmissionId;

            FilmSubmission model =  new FilmRepository().SaveFilm(film, AltTitle.Where(x=>x!="").ToList(), this.User.UserId);

            //the saved film must be saved in session - now it's current film
            this.CurrentFilm = model;

            if (!String.IsNullOrEmpty( NextPage ) )
              return  Redirect(NextPage);

            ViewBag.Message = "The Title has been saved";
            return View("Title", model);
            
        }

    }
}
