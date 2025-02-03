using API.Abstract;
using API.Data;
using API.Data.Repository;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CodeReviewHubDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CodeReviewHubDbContext)));
    }
);

builder.Services.AddScoped<ICodePublicationRepository, CodePublicationRepository>();
builder.Services.AddScoped<ICodePublicationService, CodePubicationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => 
{
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();