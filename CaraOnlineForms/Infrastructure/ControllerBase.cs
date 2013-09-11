using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaraOnlineForms
{
    [Filters.CaraAuthorize(Order = -1)] //verify user logged in at all times. Runs first in line of filters.
    [OutputCache(CacheProfile = "ZeroCacheProfile")]  //prevent output caching on the server. You can override this per action method if needed.
    public class ControllerBase:Controller
    {
    }
}