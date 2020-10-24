using System;
using System.Threading.Tasks;
using Hangfire;

namespace NbSites.Web.Demo
{
    public class DemoCommand : IBackgroundCommand
    {
        private readonly DemoService _demoService;

        public DemoCommand(DemoService demoService)
        {
            _demoService = demoService;
        }

        public object Args { get; set; }

        public Task Invoke(object methodArgs)
        {
            return DemoLogHelper.Instance.Log(this.GetType(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " with args " + methodArgs + " with service " + _demoService.Id);
        }

        public string Enqueue()
        {
            return BackgroundJob.Enqueue(() => Invoke(this.Args));
        }
    }
}
