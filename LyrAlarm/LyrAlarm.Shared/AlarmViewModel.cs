using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LyrAlarm.Shared
{
    [DataContract]
    public class AlarmViewModel
    {
        [DataMember]
        public Guid Guid { get; set; }
        [DataMember]
        public int Time { get; set; }
        [DataMember]
        public bool Monday { get; set; }
        [DataMember]
        public bool Tuesday { get; set; }
        [DataMember]
        public bool Wednesday { get; set; }
        [DataMember]
        public bool Thursday { get; set; }
        [DataMember]
        public bool Friday { get; set; }
        [DataMember]
        public bool Saturday { get; set; }
        [DataMember]
        public bool Sunday { get; set; }
        
        [IgnoreDataMember]
        public string TimeString
        {
            get
            {
                return String.Format("{0:00}:{1:00}", this.Time / 60, this.Time % 60);
            }
        }

        [IgnoreDataMember]
        public TimeSpan Timespan
        {
            get
            {
                return TimeSpan.FromMinutes(this.Time);
            }
            set
            {
                this.Time = (int)value.TotalMinutes;
            }
        }
    }
}