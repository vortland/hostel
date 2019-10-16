using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Administration.API.Infrastructure.Authentication;
using Administration.API.Infrastructure.Filter;
using Administration.Core.Repositories;
using Administration.DataAccessLayer;
using Administration.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StructureMap;
using Swashbuckle.AspNetCore.Swagger;

namespace Administration.API
{
	public class Startup
	{
		private const string SigningKey = "asdfghjklqwertyuiop123456";
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			var signingKey = new SigningSymmetricKey(SigningKey);
			services
				.AddCustomMvc()
				.AddCustomDbContext(Configuration)
				.AddCustomConfiguration()
				.AddCustomSwagger()
				.AddCustomAuthentication(signingKey);

			return services.AddCustomIoC(signingKey);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "Administration API V1");
					
				});
		}
	}

	public static class CustomExtensionsMethods
	{

		public static IServiceProvider AddCustomIoC(this IServiceCollection services, IJwtSigningEncodingKey signingEncodingKey)
		{
			var container = new Container();
			
			container.Configure(config =>
			{
				config.For(typeof(IUserRepository)).Use(typeof(UserRepository));
				config.For(typeof(IRoomRepository)).Use(typeof(RoomRepository));
				config.For(typeof(IJwtSigningEncodingKey)).Use(signingEncodingKey).Singleton();

				config.Populate(services);
			});

			return container.GetInstance<IServiceProvider>();
		}

		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.SwaggerDoc("v1", new Info
				{
					Title = "Administration HTTP API",
					Version = "v1",
					Description = "The Administration Service HTTP API",
					TermsOfService = "Terms Of Service"
				});
				options.AddSecurityDefinition("Bearer", new ApiKeyScheme
				{
					In = "header",
					Description = "Please insert JWT with Bearer into field",
					Name = "Authorization",
					Type = "apiKey"
				});

				options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
				{
					{ "Bearer", new string[] { } }
				});
			});

			return services;
		}

		public static IServiceCollection AddCustomMvc(this IServiceCollection services)
		{
			services
				.AddMvc(options => { options.Filters.Add(typeof(GlobalExceptionFilter)); })
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			return services;
		}

		public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AdministrationContext>(
				options => options.UseSqlServer(configuration.GetConnectionString("GlobalDbContext"),
					b => b.MigrationsAssembly(typeof(AdministrationContext).Assembly.FullName)));

			services.AddDbContext<AppIdentityContext>(
				options => options.UseSqlServer(configuration.GetConnectionString("GlobalDbContext"),
					b => b.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName)));
			return services;
		}

		public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IJwtSigningDecodingKey signingDecodingKey)
		{
			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityContext>()
				.AddDefaultTokenProviders();

			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

				})
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = signingDecodingKey.GetKey(),

						ValidateIssuer = true,
						ValidIssuer = "DemoApp",

						ValidateAudience = true,
						ValidAudience = "DemoAppClient",

						ValidateLifetime = true,

						ClockSkew = TimeSpan.FromSeconds(5)
					};
				});

			return services;
		}

		public static IServiceCollection AddCustomConfiguration(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = context =>
				{
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Instance = context.HttpContext.Request.Path,
						Status = StatusCodes.Status400BadRequest,
						Detail = "Please refer to the errors property for additional details"
					};

					return new BadRequestObjectResult(problemDetails)
					{
						ContentTypes = { "application/problem+json", "application/problem+xml" }
					};
				};
			});
			return services;
		}
	}
}
