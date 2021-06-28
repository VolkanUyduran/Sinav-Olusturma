using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ExamProjectWebUI
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
            services.AddScoped<IExamService, ExamManager>();
            services.AddScoped<IExamDal, EfExamDal>();

            services.AddScoped<IQuestionService, QuestionManager>();
            services.AddScoped<IQuestionDal, EfQuestionDal>();

            services.AddScoped<IOptionService, OptionManager>();
            services.AddScoped<IOptionDal, EfOptionDal>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();


            services.AddControllersWithViews();

            //sayfayi yenileyince yenilikleri algilamasi icin
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(options =>
       {
           options.Cookie.Name = "ExamAuthCookie";
           options.LoginPath = "/Account/Login/";
           options.AccessDeniedPath = "/Home/AccessDenied";
           options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
       });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
               
            });
        }
    }
}
