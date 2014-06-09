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
        //
        // GET: /Screener/

        public ActionResult Screener(int? filmId)
        {
            ScreenerViewModel model = new ScreenerViewModel();
            model.ListOfAttributes = new ScreenerRepository().GetListOfAttributes();
            model.ListOfGenres = new ScreenerRepository().GetListOfGenres();
            model.ListOfFormats = new ScreenerRepository().GetListOfFormats();
            int filmSubId = filmId ?? -1;
            Screener screener = new ScreenerRepository().GetScreener(filmSubId);
            if (screener == null)
                screener = new Screener { ScreenerId = -1, FormatId = -1, FilmSubmissionId = filmSubId, DurationMinutes = 0 };
            model.Screener = screener;

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveScreener(ScreenerViewModel screener, List<int> SelectedAttributes, List<int> SelectedGenres,int Screener_FormatId)
        {
            string a = "";
            return View();
        }

    }
}
