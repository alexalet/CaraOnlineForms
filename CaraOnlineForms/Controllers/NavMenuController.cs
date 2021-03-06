﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using CaraEntites;

namespace CaraOnlineForms.Controllers
{
    public class NavMenuController : Controller
    {


        public PartialViewResult _NavMenu()
        {
            bool showMenu = new Session().User != null;
            return PartialView("_NavMenu", showMenu);
        }


        public PartialViewResult _WorkFlow(string StepId)
        {
            WorkFlowViewModel model = new WorkFlowViewModel { FilmTitle=null};
            FilmSubmission currentFilm = new Session().Film;
            if (currentFilm != null)
            {
                model.FilmSubmitionId = currentFilm.FilmSubmissionId;
                model.FilmTitle = currentFilm.FilmTitle;
                model.StepId = StepId;
            }
            return PartialView("_WorkFlow", model);
        }  
    }
}
