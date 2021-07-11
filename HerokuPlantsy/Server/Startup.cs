using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Plantsy.Server.Data;
using System;
using System.Linq;

namespace HerokuPlantsy.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlite("filename = plantsyTestDb.db"));

			//var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
			//var databaseUri = new Uri(databaseUrl);
			//var userInfo = databaseUri.UserInfo.Split(':');

			//var builder = new NpgsqlConnectionStringBuilder
			//{
			//	Host = databaseUri.Host,
			//	Port = databaseUri.Port,
			//	Username = userInfo[0],
			//	Password = userInfo[1],
			//	SslMode = SslMode.Require,
			//	TrustServerCertificate = true,
			//	Database = databaseUri.LocalPath.TrimStart('/'),
			//};

			//var connectionString =  builder.ToString();
			//services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

			services.AddControllersWithViews();
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			using var scope = app.ApplicationServices.CreateScope();
			var services = scope.ServiceProvider;
			try
			{
				//Get the DBcontext
				var context = services.GetRequiredService<ApplicationDbContext>();

				context.Database.EnsureCreated();
				if (!context.Plants.Any())
				{
					SeedData.Initialize(context);
				}


			}
			catch (Exception ex)
			{
				var logger = services.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An error occurred creating the DB.");
			}

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
