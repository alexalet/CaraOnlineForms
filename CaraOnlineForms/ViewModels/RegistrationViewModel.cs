using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;

namespace ViewModels
{
    public class RegistrationViewModel
    {

        public RegistrationViewModel() { 
            User = new User(); 
            ErrorMessage = "";
        }

        public User User { get; set; }

        public string Captcha { get; set; }
        
        public string ErrorMessage { get; set; }

    }
}