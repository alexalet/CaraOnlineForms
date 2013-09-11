using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class LoginViewModel
    {
        public string UserId { get; set; }
        public string Pwd { get; set; }
        public string ErrorMessage { get; set; }
    }
}