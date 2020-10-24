using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Hangfire;

namespace NbSites.Web.Demo
{
    public class DemoBackupDb : IBackgroundCommand
    {
        //backup_database
        public object Args { get; set; }
        public Task Invoke(object methodArgs)
        {
            var backupBat = methodArgs as BackupDbBatFile;
            if (backupBat == null)
            {
                throw new ArgumentException("参数不合法！必须提供非空的参数: " + typeof(BackupDbBatFile).Namespace);
            }

            var runResult = backupBat.Run();
            DemoLogHelper.Instance.Log(this.GetType(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " with args " + backupBat.FilePath);
            DemoLogHelper.Instance.Log(this.GetType(), runResult);

            return Task.CompletedTask;
        }

        public string Enqueue()
        {
            RecurringJob.AddOrUpdate(() => Invoke(Args), Cron.Daily);
            return null;
        }
    }

    public class BackupDbBatFile
    {
        public string FilePath { get; set; }

        public string Run()
        {
            if (!File.Exists(FilePath))
            {
                return "shell run failed: not exist: " + FilePath;
            }

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = FilePath,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();

            //var result = process.StandardOutput.ReadToEnd();

            //解决中文乱码的问题
            using var reader = new StreamReader(process.StandardOutput.BaseStream, System.Text.Encoding.UTF8, true);
            reader.BaseStream.Flush();
            var result = reader.ReadToEnd();

            process.WaitForExit();
            return result;
        }
    }
}
