using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Message { get; set; }
        public string Email { get; set; }
        public bool NotificationWasSent { get; set; }
    
    }
}