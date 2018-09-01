using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SportStore.Log;
using SportStore.Log.Events;
using SportStore.Model;

namespace SportStore.Log.PlainText
{
    public class PlainTextLog : ILog
    {
        private readonly string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private string fileRelativePath;

        public string LogFilePath => Path.Combine(Directory.GetCurrentDirectory(), this.fileRelativePath);

        public PlainTextLog(string logFileRelativePath)
        {
            this.fileRelativePath = logFileRelativePath;
        }

        public IEnumerable<LogEvent> FindEvents(Predicate<LogEvent> predicate)
        {
            var logEvents = new List<LogEvent>();
            var logEventTypeCache = new Dictionary<string, Type>();
            var logFileContents = File.ReadAllText(this.LogFilePath);

            foreach (var line in logFileContents.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    var aux = line.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                    var typeName = aux[0].Split(']').First().Trim(new char[] { '[', ']' });
                    var dateTime = DateTime.ParseExact(aux[0].Split(']').Last().Trim(), this.dateTimeFormat, CultureInfo.InvariantCulture);
                    var user = new User() { UserName = aux[1].Trim() };
                    var content = aux.Count() > 2 ? aux[2].Trim() : "";

                    var logEventType = typeof(Nullable);
                    
                    if (logEventTypeCache.ContainsKey(typeName))
                    {
                        logEventType = logEventTypeCache[typeName];
                    }
                    else
                    {
                        var assembly = AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == "SportStore.Log");
                        var type = assembly.GetTypes().Single(t => t.FullName == "SportStore.Log.Events." + typeName);

                        logEventType = type;
                        logEventTypeCache.Add(typeName, type);
                    }

                    var logEvent = Activator.CreateInstance(logEventType, user);
                    var logEventTyped = logEvent as LogEvent;

                    logEventType.GetProperty("DateTime").SetValue(logEvent, dateTime, null);
                    logEventType.GetProperty("Content").SetValue(logEvent, content, null);

                    if (predicate(logEventTyped))
                    {
                        logEvents.Add(logEventTyped);
                    }
                }
                catch
                {
                }
            }

            return logEvents;
        }

        public void WriteEvent(LogEvent e)
        {
            var eventString = string.Format("[{0}] {1} - {2} - {3}", e.GetType().Name, e.DateTime.ToString(this.dateTimeFormat, CultureInfo.InvariantCulture), e.User.UserName, e.Content);

            File.AppendAllText(this.LogFilePath, eventString + Environment.NewLine);
        }
    }
}
