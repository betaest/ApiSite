using System;
using ApiSite.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ApiSite {
    public class Startup {
        #region Private Fields

        private static readonly LoggerFactory factory = new LoggerFactory(new[] {new DebugLoggerProvider()});

        #endregion Private Fields

        #region Public Constructors

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        #endregion Public Constructors

        #region Public Properties

        public IConfiguration Configuration { get; }

        #endregion Public Properties

        #region Public Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var conn = Configuration.GetConnectionString("ApiSite");
            var verify = Configuration.GetConnectionString("Verify");

            services.AddDbContext<ProjectManagerContext>(o =>
                    o.UseMySql(conn,
                            mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 4, 6), ServerType.MariaDb))
                        .UseLoggerFactory(factory))
                .AddDbContext<VerifyContext>(o => o.UseMySql(verify,
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(10, 4, 6), ServerType.MariaDb)));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<ApiConf>(Configuration.GetSection("Settings"));
            services.AddCors(setup =>
                setup.AddPolicy("cors",
//                    policy => policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader()
                    policy => policy.WithOrigins("http://132.232.28.32:8080").AllowAnyMethod().AllowAnyHeader()
                        .AllowCredentials()));
        }

        #endregion Public Methods
    }
}