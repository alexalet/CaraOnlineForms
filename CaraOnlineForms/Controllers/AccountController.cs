﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CaraEntites;
using Repository;
using ViewModels;
using Utility;

namespace CaraOnlineForms.Controllers
{
    public class AccountController : Controller
    {
       
        public ActionResult Logon()
        {
            //Membership.GeneratePassword(8, 1);
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
                return View("Logon", model);
            }

            CaraOnlineForms.Session session = new Session();
            session.User = user;

            return RedirectToAction("ProcessStatus", "ProcessStatus");
        }
        


        public ActionResult Registration()
        {
        
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
            string code = rep.CreateUser(newUser.User);

            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Authority
                                        + Url.Action("ConfirmEmail", new { code = code }).ToString();
            string body = String.Format(ResourceText.EmailAddressConfirmation, url);

            Utility.Email.SendAnEmail("CARA request for e-mail address confirmation", body, "cara@mpaa.org", "CARA", newUser.User.EmailAddress , true);

            return View("NotificationWasSent", "", newUser.User.EmailAddress );
        }


        public ActionResult MyProfile(string message)
        {
            CaraOnlineForms.Session session = new Session();
            User user = session.User;
            if (user == null)
                return View("Logon", new LoginViewModel { });

            
            user = new UserRepository().GetUser(user.UserId );

            RegistrationViewModel registration = new RegistrationViewModel { User=user, ErrorMessage=message}; 

            return View("Registration", registration);
        }


        public ActionResult UpdateProfile(RegistrationViewModel updatedUser)
        {
            CaraOnlineForms.Session session = new Session();
            User user = session.User;
            if (user == null)
                return View("Logon");

            updatedUser.User.UserId = user.UserId;
            var rep = new UserRepository();
            rep.UpdateUser(updatedUser.User);

            session.User = updatedUser.User;

            return RedirectToAction("MyProfile", new { message = "Profile has been updated." });
            
        }

        public ActionResult ConfirmEmail(string code)
        {
            bool res = new UserRepository().ConfirmEmail(code); 
            
            return View(res);
        }

    }
}
