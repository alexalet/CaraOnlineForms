using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;
using ViewModels;

namespace CaraOnlineForms.Controllers
{
    public class ScreenerController : ControllerBase
    {
        public ActionResult Screener(int? filmId)
        {
            ScreenerViewModel model = new ScreenerViewModel();
            model.ListOfAttributes = new ScreenerRepository().GetListOfAttributes();
            model.ListOfGenres = new ScreenerRepository().GetListOfGenres();
            model.ListOfFormats = new ScreenerRepository().GetListOfFormats();
            int filmSubId = filmId ?? -1;
            Screener scr = new ScreenerRepository().GetScreener(filmSubId);
            if (scr == null)
                scr = new Screener { ScreenerId = -1, FormatId = -1, FilmSubmissionId = filmSubId, DurationMinutes = 0 };
            model.Screener = scr;
            model.Duration.NumberOfMinutes = scr.DurationMinutes ?? 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveScreener(ScreenerViewModel screenerView)
        {
            if (ModelState.IsValid)
            {
                if (screenerView.Screener.FormatId == -1)
                    screenerView.Screener.FormatId = null;
                screenerView.Screener.DurationMinutes = screenerView.Duration.NumberOfMinutes;
                if (screenerView.Screener.DurationMinutes.HasValue && screenerView.Screener.DurationMinutes == 0)
                    screenerView.Screener.DurationMinutes = null;
            }

            ScreenerViewModel model = new ScreenerViewModel();
            model.ListOfAttributes = new ScreenerRepository().GetListOfAttributes();
            model.ListOfGenres = new ScreenerRepository().GetListOfGenres();
            model.ListOfFormats = new ScreenerRepository().GetListOfFormats();
            Screener scr = new ScreenerRepository().AddScreener(screenerView.Screener, screenerView.SelectedGenres, screenerView.SelectedAttributes, this.User.UserId);
            model.Screener = scr;
            ViewBag.Message = "The Screener has been saved";
            return View("Screener", model);
        }

    }
}
