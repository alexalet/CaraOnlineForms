using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;

namespace ViewModels
{
    public class ScreenerInfoViewModel
    {
        public Screener Screener { get; set; }

        public string Attributes
        {
            get
            {
                string res = "";
                //foreach (CaraEntites.Attribute a in Screener.Attributes)
                //{
                //    res += (res == "" ? "" : ", ") + a.Description;
                //}
                return res;
            }
        }

        public string Genres
        {
            get
            {
                string res = "";
                foreach (ScreenerGenre g in Screener.ScreenerGenres)
                {
                    res += (res == "" ? "" : ", ") + g.Genre.Description;
                }
                return res;
            }
        }

        public string PostScreeningAction
        {
            get
            {
                return (Screener.PostScreeningAction != null ? Screener.PostScreeningAction.Description : "");
            }
        }
    }

}