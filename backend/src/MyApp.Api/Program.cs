using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container; swagger configured but no endpoints defined so UI will be empty.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline with swagger only in development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// redirect root to swagger UI so developer hitting the base URL sees the blank page
app.MapGet("/", () => Results.Redirect("/swagger", permanent: false));

// No other endpoints registered - swagger page will be blank.

app.Run();
