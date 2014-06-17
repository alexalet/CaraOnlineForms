using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class CertificateMailingViewModel
    {
        public CertificateMailing Recipient { get; set; }
        public int Counter { get; set; }
    }
}