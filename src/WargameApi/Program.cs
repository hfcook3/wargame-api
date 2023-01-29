using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WargameApi.Data;
using WargameApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<KillTeamContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("KillTeamContext")));

builder.Services.AddTransient<IKillTeamService, KillTeamService>();

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

app.CreateDbIfNotExists();

app.Run();
