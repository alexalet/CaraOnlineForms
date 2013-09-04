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
    public class AccountController : Controller
    {
       
        public ActionResult Registration()
        {
           
            //var rep = new UserRepository();
            //rep.IsEmailUnique("aaa");
            return View(new RegistrationViewModel());
        }

        public ActionResult SubmitRegistration(RegistrationViewModel newUser)
        {

            var rep = new UserRepository();
            if (!rep.IsEmailUnique(newUser.User.EmailAddress))
            {
                newUser.ErrorMessage = "The email address already was taken by another user.";
                return View("Registration", newUser);
            }

            rep.CreateUser(newUser.User, newUser.Phone, newUser.Cell, newUser.Fax);
             
            return View("Registration", newUser);
        }


        public ActionResult MyProfile(string message)
        {
            int userId = 3;
            var rep = new UserRepository();
            string phone = "";
            string cell = "";
            string fax = "";
            User user = rep.GetUser(userId, out phone, out cell, out fax);
            RegistrationViewModel registration = new RegistrationViewModel { Phone=phone, Cell=cell, Fax=fax, User=user, ErrorMessage=message}; 

            return View("Registration", registration);
        }


        public ActionResult UpdateProfile(RegistrationViewModel updatedUser)
        {
            int userId = 3;
            updatedUser.User.UserId = userId;
            var rep = new UserRepository();
            rep.UpdateUser(updatedUser.User, updatedUser.Phone, updatedUser.Cell, updatedUser.Fax);

            return RedirectToAction("MyProfile", new { message = "Profile has been updated." });
            //var registration = rep.GetUser(
            //return View("Registration", registration);
        }

    }
}
