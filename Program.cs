
using WebApi_Task.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApi_Task
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string tex = "HI";
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddNewtonsoftJson(
				x=>x.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore);
			builder.Services.AddDbContext<ITI_MVCContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("conect")));
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			builder.Services.AddCors(options =>
			{
				options.AddPolicy(tex,
				builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCors(tex);

			app.MapControllers();

			app.Run();
		}
	}
}