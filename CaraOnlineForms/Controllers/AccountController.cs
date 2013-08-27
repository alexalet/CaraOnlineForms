using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;

namespace CaraOnlineForms.Controllers
{
    public class AccountController : Controller
    {
       
        public ActionResult Registration()
        {
           
            //var rep = new UserRepository();
            //rep.IsEmailUnique("aaa");
            return View(new User());
        }

    }
}
