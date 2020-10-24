using System;
using System.Threading.Tasks;
using Hangfire;

namespace NbSites.Web.Demo
{
    public class DemoRecurringCommand : IBackgroundCommand
    {
        public object Args { get; set; }
        public Task Invoke(object methodArgs)
        {
            return DemoLogHelper.Instance.Log(this.GetType(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " with args " + methodArgs);
        }

        public string Enqueue()
        {
            RecurringJob.AddOrUpdate(() => Invoke(Args), Cron.Minutely);
            return null;
        }
    }
}