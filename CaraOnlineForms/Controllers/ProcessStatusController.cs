using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;

namespace CaraOnlineForms.Controllers
{
    public class ProcessStatusController : ControllerBase
    {
        
        public ActionResult ProcessStatus(string message)
        {
            ViewBag.Message = message;
            List<FilmSubmission> model = new SubmitionProcessRepository().GetFilmsForUser(this.User.UserId);
            return View(model);
        }

        public ActionResult DeleteTitle(int filmId)
        {
            bool res = new SubmitionProcessRepository().DeleteTitle(filmId, this.User.UserId);
            if (res)
            {
                // if deleted submission is the current film then reset it in session 
                if (this.CurrentFilm != null && this.CurrentFilm.FilmSubmissionId == filmId)
                {
                    this.CurrentFilm = null;
                }
            }
            return RedirectToAction("ProcessStatus", new {message=(res ? "Selected Title has been deleted." : "Failed to delete selected Title.") });
        }
    }
}
