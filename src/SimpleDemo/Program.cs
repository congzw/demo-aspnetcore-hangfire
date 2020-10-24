using System;
using Hangfire;
using Hangfire.SqlServer;

namespace SimpleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Hangfire!");

            //SetupSqlServer.Config();
            SetupMysql.Config();

            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, BackgroundJob At " + DateTime.Now));

            ////test for server
            //using (var server = new BackgroundJobServer())
            //{
            //    Console.WriteLine("Hello BackgroundJobServer!");
            //    Console.ReadLine();
            //}
        }
    }
}
