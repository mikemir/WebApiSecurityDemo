using FluentValidation.AspNetCore;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSecurityDemo.Model.Db;
using WebApiSecurityDemo.Model.Validations;
using WebApiSecurityDemo.Services;
using WebApiSecurityDemo.Utils;
using WebApiSecurityDemo.Utils.Middlewares;

namespace WebApiSecurityDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IApiVersionDescriptionProvider ApiDescription { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            //https://docs.automapper.org/en/latest/Getting-started.html
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=BlogDemo;Trusted_Connection=True;");
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(2, 0);
                options.ReportApiVersions = true; //Colocar True solo cuando la aplicación se corra en ambiente de desarrollo
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                                  {
                                      policy.WithOrigins("http://example.com",
                                                          "http://www.contoso.com");
                                  });
            });

            services.AddSingleton(LogManager.GetLogger("WebApiSecurityDemo"));

            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddSwaggerGen(options =>
            {
                if (ApiDescription == null)
                    return;

                foreach (var description in ApiDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo()
                    {
                        Title = "WebApiSecurityDemo",
                        Version = $"{description.ApiVersion} {(description.IsDeprecated ? "API VERSION IS DEPRECATED" : "")}",
                        Description = description.IsDeprecated ? "API VERSION IS DEPRECATED" : "",
                        Contact = new OpenApiContact()
                        {
                            Name = "Michael Emir Reynosa",
                            Email = "devmikemir@gmail.com"
                        }
                    });
                }
            });

            //FluentValidations
            services.AddControllers()
                .AddFluentValidation(val => val.RegisterValidatorsFromAssemblyContaining<UserValidator>());
        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiVersionDescription,
                              ILoggerService loggerService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //Implementacion de OpenAPI
                //https://docs.microsoft.com/es-es/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
                app.UseSwaggerUI(options =>
                {
                    ApiDescription = apiVersionDescription;

                    foreach (var description in apiVersionDescription.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpper());
                    }

                    options.RoutePrefix = string.Empty;
                });
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });
            }
            //Agregamos el middleware
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
            app.UseErrorHandler(loggerService);

            app.UseHttpsRedirection();

            app.UseRouting();

            //Implementación casera de RateRequest limit
            //https://github.com/stefanprodan/AspNetCoreRateLimit#readme
            app.UseRateLimit();

            //UseCors debe llamarse en el orden correcto.
            //Por ejemplo, UseCors se debe llamar a antes de UseResponseCaching
            //https://docs.microsoft.com/es-es/aspnet/core/security/cors?view=aspnetcore-3.1.
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}