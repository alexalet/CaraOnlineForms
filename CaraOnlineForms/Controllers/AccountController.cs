using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;
using Repository;
using ViewModels;
using Utility;

namespace CaraOnlineForms.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            LoginViewModel model = new LoginViewModel { ErrorMessage="", Pwd="", UserId="" };
            return View(model);
        }


        public ActionResult TryLogin(LoginViewModel login)
        {
            string encryptedPWD = Utility.Cryptography.EncryptPwd(login.Pwd);
            User user = new UserRepository().TryLogin(login.UserId, encryptedPWD);

            if (user == null)
            {
                LoginViewModel model = new LoginViewModel { ErrorMessage = "Login Failed", Pwd = "", UserId = login.UserId };
                return View("Login", model);
            }

            return RedirectToAction("Title", "Submition");
        }
        


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
                newUser.ErrorMessage = "The email address already is used by another user.";
                return View("Registration", newUser);
            }

            newUser.User.Password = Utility.Cryptography.EncryptPwd(newUser.User.Password);
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
