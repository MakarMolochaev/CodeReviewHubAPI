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

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<CodeReviewHubDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CodeReviewHubDbContext)));
    }
);

//builder.Ыervices.AddScoped<ICodePublicationRepository, CodePublicationRepository>();
services.AddScoped<ICodePublicationRepository, CodePublicationRepository>();
services.AddScoped<ICodePublicationService, CodePubicationService>();
services.AddScoped<UsersRepository>();
services.AddScoped<UsersService>();
services.AddScoped<JwtProvider>();
services.AddHttpContextAccessor();
services.AddScoped<IPasswordHasher, PaswordHasher>();
//services.AddApiAuthentification();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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