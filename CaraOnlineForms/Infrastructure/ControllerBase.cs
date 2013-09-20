using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaraEntites;

namespace CaraOnlineForms
{
    [Filters.CaraAuthorize(Order = -1)] //verify user logged in at all times. Runs first in line of filters.
    [OutputCache(CacheProfile = "ZeroCacheProfile")]  //prevent output caching on the server. You can override this per action method if needed.
    public class ControllerBase:Controller
    {

        public new User User {
            get { 
                Session session = new Session();
                return session.User;
            }
            
            set {} 
        }

        // currently selected film
        public FilmSubmission CurrentFilm
        {
            get
            {
                Session session = new Session();
                return session.Film;
            }

            set
            {
                Session session = new Session();
                session.Film = value;
            }
        }
    }
}