using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
        
        public IActionResult GetAllCommands([FromServices] IEnumerable<IBackgroundCommand> commands)
        {
            return Json(commands.Select(x => x.GetType().Name));
        }
    }
}
