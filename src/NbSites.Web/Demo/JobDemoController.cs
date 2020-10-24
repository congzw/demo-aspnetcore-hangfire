using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace NbSites.Web.Demo
{
    public class JobDemoController : Controller
    {
        public IList<string> GetLogs()
        {
            return DemoLogHelper.Instance.InvokeLogs;
        }

        public IActionResult Call([FromServices] DemoCommand demoCommand)
        {
            demoCommand.Args = "simple call args";
            var enqueue = demoCommand.Enqueue();
            return Json(demoCommand);
        }

        public IActionResult Call2([FromServices] DemoDelayCommand demoCommand)
        {
            demoCommand.Args = "simple call2";
            demoCommand.Enqueue();
            return Json(demoCommand);
        }

        public IActionResult Call3([FromServices] DemoRecurringCommand demoCommand)
        {
            demoCommand.Args = "simple call3 " + Guid.NewGuid().ToString("N");
            demoCommand.Enqueue();
            return Json(demoCommand);
        }

        public IActionResult Call4([FromServices] DemoBackupDb demoCommand, [FromServices] IWebHostEnvironment env)
        {
            demoCommand.Args = new BackupDbBatFile
            {
                FilePath = Path.Combine(env.ContentRootPath, "backup_database.bat")
            };
            demoCommand.Enqueue();
            return Json(demoCommand);
        }

        public IActionResult GetAllCommands([FromServices] IEnumerable<IBackgroundCommand> commands)
        {
            return Json(commands.Select(x => x.GetType().Name));
        }
    }
}
