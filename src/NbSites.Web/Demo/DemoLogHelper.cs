using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NbSites.Web.Demo
{
    public class DemoLogHelper
    {
        public Task Log(Type callerType, string log)
        {
            if (callerType != null)
            {
                log = callerType.FullName + " => " + log;
            }
            InvokeLogs.Add(log);
            if (LogToFile)
            {
                var logPath = Path.Combine(Directory.GetCurrentDirectory(), @"demo_command_logs.txt");
                return File.AppendAllTextAsync(logPath, log + Environment.NewLine);
            }
            return Task.CompletedTask;
        }
        public bool LogToFile { get; set; }
        public IList<string> InvokeLogs { get; set; } = new List<string>();

        public static DemoLogHelper Instance = new DemoLogHelper();
    }
}