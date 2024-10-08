

using SEP490_G87_Vita_Nutrient_System_API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Sep490G87VitaNutrientSystemContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



//using SEP490_G87_Vita_Nutrient_System_API.Models;

//namespace SEP490_G87_Vita_Nutrient_System_API
//{
//	public class Program
//	{
//		public static void Main(string[] args)
//		{
//			var builder = WebApplication.CreateBuilder(args);

//			// Add services to the container.
//			builder.Services.AddControllers();
//			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//			builder.Services.AddDbContext<Sep490G87VitaNutrientSystemContext>();
//			builder.Services.AddEndpointsApiExplorer();
//			builder.Services.AddSwaggerGen();
//			builder.Services.AddCors(options =>
//			{
//				options.AddPolicy("AllowClient",
//					builder => builder.WithOrigins("http://localhost:5275")
//									  .AllowAnyMethod()
//									  .AllowAnyHeader()
//									  .AllowCredentials());
//			});

//			var app = builder.Build();

//			// Configure the HTTP request pipeline.
//			if (app.Environment.IsDevelopment())
//			{
//				app.UseSwagger();
//				app.UseSwaggerUI();
//				app.UseDeveloperExceptionPage();
//			}

//			app.UseStaticFiles();
//			app.UseRouting();
//			app.UseCors("AllowClient");
//			app.UseAuthorization();

//			app.MapControllers();

//			app.Run();
//		}
//	}
//}