using HRM_API.API;
using HRM_API.Application;
using HRM_API.Application.Helpers;
using HRM_API.Application.Services;
using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Authorization;
using HRM_API.Core.Interfaces.Form;
using HRM_API.Core.Interfaces.JobVacancy;
using HRM_API.Core.Interfaces.PreApplication;
using HRM_API.Infraestructure.Repositories.Authorization;
using HRM_API.Infraestructure.Repositories.Form;
using HRM_API.Infraestructure.Repositories.JobVacancy;
using HRM_API.Infraestructure.Repositories.PreApplication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

#region CORS
//CORS Dev
const string DevCors = "DevCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(DevCors, policy =>
        policy
            .WithOrigins("http://localhost:5173") // agrega más orígenes si hace falta
            .AllowAnyMethod()
            .AllowAnyHeader()
    // OJO: solo si usas cookies/sesión:
    //.AllowCredentials()
    );
});

//CORS Estandar
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowOrigins", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});
#endregion

#region AppSettings embebido
var cfgAsm = typeof(Settings).Assembly;
using (var stream = cfgAsm.GetManifestResourceStream("HRM_API.Configuration.appsettings.json"))
{
    if (stream is null)
        throw new FileNotFoundException("No se encontró el recurso embebido 'appsettings.json' en la librería de configuración.");

    builder.Configuration.AddJsonStream(stream);
}

// Configuración de Settings
builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddSingleton<ISettings>(sp => sp.GetRequiredService<IOptions<Settings>>().Value);
#endregion

#region Conexión a base de datos (Dapper)
builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var cs = config.GetConnectionString("localDB")
             ?? throw new InvalidOperationException("ConnectionString 'localDB' no encontrada en configuración.");

    return new SqlConnection(cs);
});
#endregion

#region JWT Service, Middleware y Helpers
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<ConversionHelper>();
#endregion

#region Repositorios y Servicios
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<FormService>();
builder.Services.AddScoped<IJobVacancyRepository, JobVacancyRepository>();
builder.Services.AddScoped<JobVacancyService>();
builder.Services.AddScoped<IPreApplicationRepository, PreApplicationRepository>();
builder.Services.AddScoped<PreApplicationService>();
#endregion

#region Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "HRM Adminsitration", Version = "v1" });

    options.DocInclusionPredicate((docName, apiDesc) => true);
    options.TagActionsBy(api =>
    {
        var controllerName = api.ActionDescriptor.RouteValues.TryGetValue("controller", out var name) ? name : null;
        return new[] { controllerName ?? "Default" };
    });

    // Configuración de Bearer token en Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Ingrese 'Bearer' [espacio] y luego su token JWT.\n\nEjemplo: `Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

var app = builder.Build();

#region Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
//app.UseCors("AllowOrigins"); //CORS Estandar
app.UseCors(DevCors); //CORS Dev
app.UseJwtMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion
