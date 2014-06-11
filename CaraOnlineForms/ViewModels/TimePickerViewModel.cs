using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TimePickerViewModel
    {
        /// <summary>
        /// this property represents value shown in TimePicker
        /// </summary>
        public TimeSpan? TimeSpan
        {
            get
            {
                if (TimeHR == -1)
                    return null;
                if (AmPm == "AM" && TimeHR == 12)
                {
                    TimeHR -= 12;
                }

                if (AmPm == "PM" && TimeHR < 12)
                {
                    TimeHR += 12;
                }
                return new TimeSpan(TimeHR, TimeMin, 0);
            }
            set
            {
                AmPm = "AM";
                if (value.HasValue)
                {
                    TimeHR = value.Value.Hours;
                    TimeMin = value.Value.Minutes;
                    AmPm = (TimeHR >= 12 ? "PM" : "AM");  // 12:30 = 12:30PM
                    if (value.Value.Hours > 12) // 13:22 = 1:22PM
                    {
                        TimeHR -= 12;
                    }
                    if (value.Value.Hours == 0) // 00:13 = 12:13AM 
                    {
                        TimeHR = 12;
                    }

                }
                else
                    TimeHR = -1;
            }
        }

        /// <summary>
        /// represent number of minutes shown in TimePicker  
        /// </summary>
        public long? NumberOfMinutes
        {
            get
            {
                if (TimeHR == -1)
                    return null;
                return TimeHR * 60 + TimeMin;
            }
            set
            {
                AmPm = "AM";
                if (value.HasValue)
                {
                    TimeHR = (int)value.Value / 60;
                    TimeMin = (int)(value.Value - TimeHR * 60);
                }
                else
                {
                    TimeHR = -1;
                }
            }
        }

        public int TimeHR { get; set; }

        public int TimeMin { get; set; }

        public string AmPm { get; set; }

        public string Prefix { get; set; }

        // if true then timeHR may be set into -1
        public bool IsNullable { get; set; }

        public bool HideAmPm { get; set; }

    }
}