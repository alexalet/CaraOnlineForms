using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaraOnlineForms.Controllers
{
    public class NavMenuController : Controller
    {


        public PartialViewResult _NavMenu()
        {
            bool showMenu = new Session().User != null;
            return PartialView("_NavMenu", showMenu);
        }

    }
}
