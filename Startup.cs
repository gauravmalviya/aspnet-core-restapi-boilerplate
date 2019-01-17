using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CWG.API.Workflow.Data;
using CWG.API.Workflow.Data.Seed;
using CWG.API.Workflow.Halpers;
using CWG.API.Workflow.ServiceContracts.Auth;
using CWG.API.Workflow.ServiceContracts.ClientModule;
using CWG.API.Workflow.Services.Auth;
using CWG.API.Workflow.Services.ClientModule;
using Swashbuckle.AspNetCore.Swagger;

namespace CWG.API
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CWGDbContext>(options => options.UseSqlite(String.Format("Filename={0}/democwg.db", AppDomain.CurrentDomain.BaseDirectory)));


            //MSSQLSEREVR
            // services.AddDbContext<CWGDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("SCONN"),
            //         opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API + profiler integrated on top left page", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            // Adding automapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new AutoMapperProfileConfiguration());
                });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            
            //Client Module
            services.AddScoped<IClientService, ClientService>();
            

            services.AddCors();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration.GetSection("JwtIssuerOptions:SecurityKey").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
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
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.  
          
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            app.UseSwaggerUI(c =>
            {
            c.RoutePrefix = "api-doc";
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();



        }
    }
}
