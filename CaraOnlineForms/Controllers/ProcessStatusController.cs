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
        
        public ActionResult ProcessStatus()
        {
            List<Film> model = new SubmitionProcessRepository().GetFilmsForUser(this.User.UserId);
            return View(model);
        }

    }
}
