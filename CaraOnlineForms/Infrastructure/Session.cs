using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;

namespace CaraOnlineForms
{
    
    /// <summary>
    /// Set or Get items in/from Http Session
    /// </summary>
        public class Session
        {
            private static T GetFromSession<T>(string key)
            {
                object obj = HttpContext.Current.Session[key];
                if (obj == null)
                {
                    return default(T);
                }
                return (T)obj;
            }

            private static void SetInSession<T>(string key, T value)
            {
                if (value == null)
                {
                    HttpContext.Current.Session.Remove(key);
                }
                else
                {
                    HttpContext.Current.Session[key] = value;
                }
            }

            /// <summary>
            /// The logged in, authenticated application user.
            /// </summary>
            public User User
            {
                get { return GetFromSession<User>("user"); }
                set { SetInSession<User>("user", value); }
            }

   }
}
