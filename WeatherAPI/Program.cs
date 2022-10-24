using WeatherAPI.Helper;
using WeatherAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IGeoService, GeoService>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IAirQualityParticulateMatterService, AirQualityParticulateMatterService>();

builder.Services.AddHttpClient<IAirQualityPollenService, AirQualityPollenService>(client => 
    client.BaseAddress = new Uri(ConstantsHelper.AQ_BASE));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
