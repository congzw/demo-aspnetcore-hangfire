using System;
using System.Threading.Tasks;
using Hangfire;

namespace NbSites.Web.Demo
{
    public class DemoDelayCommand : IBackgroundCommand
    {
        public object Args { get; set; }
        public Task Invoke(object methodArgs)
        {
            return DemoLogHelper.Instance.Log(this.GetType(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " with args " + methodArgs);
        }

        public string Enqueue()
        {
            var jobId = BackgroundJob.Schedule(() => Invoke(Args), TimeSpan.FromSeconds(3));
            return jobId;
        }
    }
}