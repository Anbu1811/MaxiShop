using MaxiShop.Infrastructue;
using MaxiShop.Application;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using MaxiShop.Infrastructue.Common;
using MaxiShop.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using MaxiShop.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

#region DataBase Connectivity

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.SignIn.RequireConfirmedEmail = false;
	options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<ApplicationDbContext>();

#endregion

#region CORS Add

builder.Services.AddCors(options =>
{
	options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion

builder.Services.AddResponseCaching();

builder.Services.AddControllers(options =>
{
	options.CacheProfiles.Add("Default", new CacheProfile
	{
		Duration = 60
	});
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateIssuerSigningKey = true,
		ValidateAudience = true,
		ClockSkew = TimeSpan.Zero,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
	};
});

builder.Services.AddSwaggerGen(options =>
{
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
		  Name = "Authorization",
		  In = ParameterLocation.Header,
		  Type = SecuritySchemeType.ApiKey,
		  Scheme  = "Bearer",
		  Description = @"jwt authorization header using the Bearer schema.
                          Enter 'Bearer' [Space] and then your token in the input below.
                          Example: 'Bearer 12345abcdef' "
      });

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "Oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			}, new List<string>()
		}
	});
});

#region config for seedingData to DataBase

static async void UpdateDataBaseAsync(IHost host)
{
	using(var scope = host.Services.CreateScope())
	{
		var service = scope.ServiceProvider;

		try
		{
			var context = service.GetRequiredService<ApplicationDbContext>();

			if (context.Database.IsSqlServer())
			{
				context.Database.Migrate();
			}

			await SeedData.SeedDataAsync(context);
		}
		catch (Exception ex)
		{

			var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An Error occoured while Migration or Seeding the Data base");
		}
	}
}

#endregion


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

UpdateDataBaseAsync(app);

var serviceProvider = app.Services;

await SeedData.SeedRoles(serviceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
