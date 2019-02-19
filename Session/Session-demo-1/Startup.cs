using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.DataProtection;
using StackExchange.Redis;
namespace Session_demo_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //分布式session
			var redis = ConnectionMultiplexer.Connect("47.107.30.29:6379,password=fanyou_redis_dev,defaultdatabase=7");
			services.AddDataProtection()
			.SetApplicationName("Test")
			.PersistKeysToRedis(redis, "Test-Keys");
	        services.AddDistributedRedisCache(options => {
				options.Configuration = "47.107.30.29:6379,password=fanyou_redis_dev,defaultdatabase=7";
				options.InstanceName = "session";
			});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
    	　　services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromHours(2);
			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
