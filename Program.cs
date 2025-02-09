using System.Text.Json;
using API.Abstract;
using API.Abstract.Auth;
using API.Abstract.Repository;
using API.Abstract.Service;
using API.Data;
using API.Data.Repository;
using API.Extensions;
using API.Infrastructure;
using API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CodeReviewHub API", Version = "v1" });
});

services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<CodeReviewHubDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CodeReviewHubDbContext)));
    }
);

//builder.Ыervices.AddScoped<ICodePublicationRepository, CodePublicationRepository>();
services.AddScoped<ICodePublicationRepository, CodePublicationRepository>();
services.AddScoped<ICodePublicationService, CodePubicationService>();
services.AddScoped<ICommentRepository, CommentRepository>();
services.AddScoped<ICommentService, CommentService>();
services.AddScoped<UsersRepository>();
services.AddScoped<UsersService>();
services.AddScoped<JwtProvider>();
services.AddScoped<RatingService>();
services.AddScoped<JwtService>();
services.AddHttpContextAccessor();
services.AddScoped<IPasswordHasher, PaswordHasher>();
services.AddScoped<CodeReviewHubDbContext>();

var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>();

services.AddApiAuthentication(jwtOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

    var swaggerProvider = app.Services.GetService<ISwaggerProvider>();
    var swaggerDoc = swaggerProvider.GetSwagger("v1");
    
    var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Docs", "swagger.json");
    var json = JsonSerializer.Serialize(swaggerDoc, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(outputPath, json);
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();