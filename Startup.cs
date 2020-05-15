using Google.Apis.Auth.OAuth2;
using GraphQl.Models;
using GraphQl.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using VServe_Google_API;

namespace GraphQl
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
            services.AddControllers().AddNewtonsoftJson();

            FileStream serviceAccount = new FileStream(@"firebaseconfig.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            FirebaseAdmin.AppOptions options = new FirebaseAdmin.AppOptions
            {
                Credential = GoogleCredential.FromStream(serviceAccount),
                ServiceAccountId = "firebase-adminsdk-w2m8q@vserve-app.iam.gserviceaccount.com",
            };

            FirebaseAdmin.FirebaseApp.Create(options);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/vserve-app";
                    options.Audience = "https://securetoken.google.com/vserve-app";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/vserve-app",
                        ValidateAudience = true,
                        ValidAudience = "vserve-app",
                        ValidateLifetime = true
                    };
                });

            services.Configure<MongoAppSettings>(Configuration.GetSection(nameof(MongoAppSettings)));
            services.AddSingleton<IMongoAppSettings>(sp => sp.GetRequiredService<IOptions<MongoAppSettings>>().Value);
            services.AddScoped<MongoContext>();
            services.AddScoped<JobDetailRepository>();
            services.AddScoped<AssignmentsRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<UserDetailsRepo>();
            services.AddScoped<AuthenticationApi>();
            services.AddScoped<FireBaseRepo>();
            services.AddScoped<PlaceApi>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerDocument();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

            });

            if (env.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
