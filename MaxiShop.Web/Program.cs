using MaxiShop.Infrastructue;
using MaxiShop.Application;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using MaxiShop.Infrastructue.Common;
using MaxiShop.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

#region DataBase Connectivity

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

#endregion

#region CORS Add

builder.Services.AddCors(options =>
{
	options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
