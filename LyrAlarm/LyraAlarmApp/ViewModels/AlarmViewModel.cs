using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyraAlarmApp.ViewModels
{
    public class AlarmViewModel
    {
        public Guid Guid { get; set; }
        public int Time { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string TimeString
        {
            get
            {
                return String.Format("{0:00}:{1:00}", this.Time / 60, this.Time % 60);
            }
        }
    }
}