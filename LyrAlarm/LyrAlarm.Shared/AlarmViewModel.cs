using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace LyrAlarm.Shared
{
    [DataContract]
    public class AlarmViewModel : INotifyPropertyChanged
    {
        private Guid guid;
        [DataMember]
        public Guid Guid
        {
            get
            {
                return this.guid;
            }
            set
            {
                this.guid = value;
                this.NotifyPropertyChanged("Guid");
            }
        }

        private int time;
        [DataMember]
        public int Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
                this.NotifyPropertyChanged("Time");
            }
        }

        private bool monday;
        [DataMember]
        public bool Monday
        {
            get
            {
                return this.monday;
            }
            set
            {
                this.monday = value;
                this.NotifyPropertyChanged("Monday");
            }
        }

        private bool tuesday;
        [DataMember]
        public bool Tuesday
        {
            get
            {
                return this.tuesday;
            }
            set
            {
                this.tuesday = value;
                this.NotifyPropertyChanged("Tuesday");
            }
        }

        private bool wednesday;
        [DataMember]
        public bool Wednesday
        {
            get
            {
                return this.wednesday;
            }
            set
            {
                this.wednesday = value;
                this.NotifyPropertyChanged("Wednesday");
            }
        }

        private bool thursday;
        [DataMember]
        public bool Thursday
        {
            get
            {
                return this.thursday;
            }
            set
            {
                this.thursday = value;
                this.NotifyPropertyChanged("Thursday");
            }
        }

        private bool friday;
        [DataMember]
        public bool Friday
        {
            get
            {
                return this.friday;
            }
            set
            {
                this.friday = value;
                this.NotifyPropertyChanged("Friday");
            }
        }

        private bool saturday;
        [DataMember]
        public bool Saturday
        {
            get
            {
                return this.saturday;
            }
            set
            {
                this.saturday = value;
                this.NotifyPropertyChanged("Saturday");
            }
        }

        private bool sunday;
        [DataMember]
        public bool Sunday
        {
            get
            {
                return this.sunday;
            }
            set
            {
                this.sunday = value;
                this.NotifyPropertyChanged("Sunday");
            }
        }

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
                this.NotifyPropertyChanged("Timespan");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}