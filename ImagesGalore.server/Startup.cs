using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
// using ImagesGalore.Repositories;
// using ImagesGalore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MySqlConnector;

namespace ImagesGalore.Server
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
      ConfigureCors(services);
      ConfigureAuth(services);

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImagesGalore.Server", Version = "v1" });
      });

      services.AddScoped<IDbConnection>(x => CreateDbConnection());

      // REPOS
      //   services.AddScoped<AccountsRepository>();
      //   services.AddTransient<BoardsRepository>();
      //   services.AddTransient<ImagesRepository>();
      //   // BL
      //   services.AddScoped<AccountsService>();
      //   services.AddTransient<BoardsService>();
      //   services.AddTransient<ImagesService>();


    }

    private void ConfigureCors(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsDevPolicy", builder =>
              {
                builder
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .WithOrigins(new string[]{
                        "http://localhost:8080", "http://localhost:8081"
                  });
              });
      });
    }

    private void ConfigureAuth(IServiceCollection services)
    {
      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        // NOTE this must match the object structure in appsettings.json
        options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
        options.Audience = Configuration["Auth0:Audience"];
      });

    }

    private IDbConnection CreateDbConnection()
    {
      // NOTE this must match the object structure in appsettings.json
      string connectionString = Configuration["DB:Connectionstring"];
      return new MySqlConnection(connectionString);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImagesGalore.Server v1"));
        app.UseCors("CorsDevPolicy");
      }

      app.UseHttpsRedirection();

      // NOTE use to serve your built client
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}