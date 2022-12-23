using CutestCat.Configs;
using CutestCat.Repositories;
using CutestCat.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.Configure<CatRankingDatabaseConfig>(
	builder.Configuration.GetSection("CatRankingDatabase"));
builder.Services.AddSingleton<CatRankingRepository>();
builder.Services.AddSingleton<CatRankingService>();
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
