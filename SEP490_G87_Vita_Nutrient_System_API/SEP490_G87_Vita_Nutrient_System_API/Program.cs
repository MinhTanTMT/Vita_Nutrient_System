using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels;
using SEP490_G87_Vita_Nutrient_System_API.Hubs;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddSignalR();*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Sep490G87VitaNutrientSystemContext>();
builder.Services.AddDbContext<Sep490G87VitaNutrientSystemContext>(
    options =>
        options.UseLazyLoadingProxies());
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        builder => builder.WithOrigins("https://localhost:7069")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

builder.Services.AddScoped<IChatRepositories, ChatRepositories>();
builder.Services.AddScoped<IRoomRepositories, RoomRepositories>();
builder.Services.AddScoped<IMessageRepositories, MessageRepositories>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
/*app.MapHub<ChatHub>("/chatHub");*/
app.UseHttpsRedirection();
app.UseCors("AllowClient");
app.UseAuthorization();

app.MapControllers();

app.Run();
