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
            if (screener != null)
                model.Screener = screener;

            return View();
        }

    }
}
