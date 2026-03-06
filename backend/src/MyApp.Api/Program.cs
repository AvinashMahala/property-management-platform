using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using MyApp.Api; // for AddApplicationServices extension

var builder = WebApplication.CreateBuilder(args);

// wire up application services and repositories
builder.Services.AddApplicationServices();

// Add services to the container; swagger configured but no endpoints defined so UI will be empty.
builder.Services.AddEndpointsApiExplorer();

// centralized API versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// register MVC controllers
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    // Provide at least one document with version info so the swagger UI has a valid definition
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Property Management API",
        Version = "v1"
    });
    // include all actions in Swagger; versioning is visible through URL segment
    // if you need per-version grouping use the Microsoft.AspNetCore.Mvc.ApiExplorer
    // package and AddVersionedApiExplorer instead of a custom predicate.
    // For now we leave the default behavior which shows every controller.
    // however, exclude the root redirect endpoint from the docs
    c.DocInclusionPredicate((docName, apiDesc) =>
    {
        // apiDesc.RelativePath is null for non-routed endpoints
        if (string.IsNullOrEmpty(apiDesc.RelativePath))
            return false;
        // swagger strips leading slash, so check for empty or index
        return apiDesc.RelativePath != "" && apiDesc.RelativePath != "/";
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline with swagger only in development.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// map controller endpoints so swagger can see them
app.MapControllers();

// redirect root to swagger UI so developer hitting the base URL sees the blank page
app.MapGet("/", () => Results.Redirect("/swagger", permanent: false));

// controllers provide operations; if none exist swagger will still be blank.

app.Run();
