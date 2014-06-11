using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaraEntites;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ScreenerViewModel
    {
        public ScreenerViewModel()
        {
            Screener = new Screener();
            Screener.ScreenerId = -1; // it indicates to Viewer that the screener is new 
            Screener.PostScreeningActionId = 1;
            SelectedGenres = new List<int>();
            SelectedAttributes = new List<int>();
            Duration = new TimePickerViewModel() { TimeHR = -1, Prefix = "Duration", IsNullable = true, HideAmPm = true };
        }

        public TimePickerViewModel Duration { get; set; }
        /// <summary>
        /// list of all available formats
        /// </summary>
        public List<Format> ListOfFormats { get; set; }

        /// <summary>
        /// list of all available genres
        /// </summary>
        public List<Genre> ListOfGenres { get; set; }
        public List<int> SelectedGenres { get; set; }

        /// <summary>
        /// list of all available attributes
        /// </summary>
        public List<CaraEntites.Attribute> ListOfAttributes { get; set; }

        public List<int> SelectedAttributes { get; set; }

        public Screener Screener { get; set; }
    }

}