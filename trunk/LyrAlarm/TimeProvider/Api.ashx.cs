using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace TimeProvider.Web
{
    public class Api : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            CultureInfo currentCulture = CultureInfo.GetCultureInfo("de-DE");

            switch (context.Request.QueryString["action"])
            {
                case "time":
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(DateTime.Now.ToString(currentCulture));
                    }

                    break;

                case "get-alarms":
                    {
                        List<Alarm> alarms = null;

                        using (TimeProviderEntities model = new TimeProviderEntities())
                        {
                            alarms = model.Alarms.AsNoTracking().ToList();
                        }

                        string json = JsonConvert.SerializeObject(alarms, Debugger.IsAttached == true ? Formatting.Indented : Formatting.None);

                        context.Response.ContentType = "text/javascript";
                        context.Response.Write(json);
                    }

                    break;

                case "get-alarms-tiny":
                    {
                        List<Alarm> alarms = null;

                        using (TimeProviderEntities model = new TimeProviderEntities())
                        {
                            alarms = model.Alarms.AsNoTracking().ToList();
                        }

                        string alarmString = String.Join(";", alarms.Select(x => this.GetAlarmString(x)));

                        context.Response.ContentType = "text/plain";
                        context.Response.Write(alarmString);
                    }

                    break;

                case "set-alarm":
                    {
                        Alarm alarm = JsonConvert.DeserializeObject<Alarm>(context.Request.Form["alarm"]);

                        using (TimeProviderEntities model = new TimeProviderEntities())
                        {
                            var existingAlarm = model.Alarms.FirstOrDefault(x => x.Guid == alarm.Guid);

                            if (existingAlarm == null)
                            {
                                model.Alarms.Add(alarm);
                            }
                            else
                            {
                                existingAlarm.Time = alarm.Time;
                                existingAlarm.Monday = alarm.Monday;
                                existingAlarm.Tuesday = alarm.Tuesday;
                                existingAlarm.Wednesday = alarm.Wednesday;
                                existingAlarm.Thursday = alarm.Thursday;
                                existingAlarm.Friday = alarm.Friday;
                                existingAlarm.Saturday = alarm.Saturday;
                                existingAlarm.Sunday = alarm.Sunday;
                            }

                            model.SaveChanges();
                        }

                        context.Response.Write("success");
                    }

                    break;
            }
        }

        private string GetAlarmString(Alarm alarm)
        {
            return String.Format(
                "{0}|{1}|{2}{3}{4}{5}{6}{7}{8}",
                alarm.Guid,
                alarm.Time,
                alarm.Monday ? "1" : "0",
                alarm.Tuesday ? "1" : "0",
                alarm.Wednesday ? "1" : "0",
                alarm.Thursday ? "1" : "0",
                alarm.Friday ? "1" : "0",
                alarm.Saturday ? "1" : "0",
                alarm.Sunday ? "1" : "0"
            );
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}