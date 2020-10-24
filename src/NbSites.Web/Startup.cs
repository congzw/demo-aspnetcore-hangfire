using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NbSites.Web.Demo;
using NbSites.Web.Helpers;

namespace NbSites.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            DemoLogHelper.Instance.LogToFile = true;
            services.AddSingleton<DemoService>();
            services.AddTransient<DemoCommand>();
            services.AddTransient<IBackgroundCommand, DemoCommand>();
            services.AddTransient<DemoDelayCommand>();
            services.AddTransient<IBackgroundCommand, DemoDelayCommand>();
            services.AddTransient<DemoRecurringCommand>();
            services.AddTransient<IBackgroundCommand, DemoRecurringCommand>();

            services.AddConfigHangfire(Configuration);
            // Add the processing server as IHostedService
            services.AddHangfireServer(opt =>
            {
                opt.ServerName = "MyHangfireServer";
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyDashboardAuthorizationFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
