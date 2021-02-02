using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using PracticaExamen2Febrero.Data;
using PracticaExamen2Febrero.Helpers;
using PracticaExamen2Febrero.Repositories;

namespace PracticaExamen2Febrero
{
    public class Startup
    {
        IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            String cadenasql = this.configuration.GetConnectionString("cadenasqlcoches");
            String cadenamysql = this.configuration.GetConnectionString("cadenamysqlcoches");
            services.AddSingleton<IConfiguration>(this.configuration);
            services.AddSingleton<PathProvider>();
            //sql
            services.AddTransient<IRepositoryCoches, RepositoryCochesSQL>();
            services.AddDbContext<CocheContext>(options => options.UseSqlServer(cadenasql));
            //xml:
            //services.AddTransient<IRepositoryCoches, RepositoryCochesXML>();

            //mysql: NO FUNCIONAL
            //services.AddTransient<IRepositoryCoches, RepositoryCochesMySQL>();

            //services.AddDbContext<CocheContext>(options =>
            //options.UseMySql(cadenamysql, new MySqlServerVersion(new Version(8, 0, 22))
            //, mySqlOptions =>
            //mySqlOptions.CharSetBehavior(CharSetBehavior.NeverAppend)));
            //services.AddDbContextPool<CocheContext>
            //    (options => options.UseMySql(cadenamysql, ServerVersion.AutoDetect(cadenamysql)));


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name:"default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
