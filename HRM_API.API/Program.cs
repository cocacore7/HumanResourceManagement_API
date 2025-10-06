using HRM_API.API;
using HRM_API.Application;
using HRM_API.Configuration;
using HRM_API.Core.Interfaces.Authorization;
using HRM_API.Infraestructure.Repositories.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

//CORS
var builder = WebApplication.CreateBuilder(args);
_ = builder.Services.AddCors(options => { options.AddPolicy("AllowOrigins", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });

//AppSettings.json Configuration
var cfgAsm = typeof(Settings).Assembly;
using (var stream = cfgAsm.GetManifestResourceStream("HRM_API.Configuration.appsettings.json")) 
{
    if (stream is null)
    { throw new FileNotFoundException("No se encontro el recuso embebido 'appsettings.json' en la libreria de configuracion."); }
    builder.Configuration.AddJsonStream(stream);
}

builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddSingleton<ISettings>(sp => sp.GetRequiredService<IOptions<Settings>>().Value);

//Database Conection Configuration
builder.Services.AddScoped<IDbConnection>(sp => 
{
    var config = sp.GetRequiredService<IConfiguration>();
    var cs = config.GetConnectionString("localDB") ?? throw new InvalidOperationException("ConnectionString: localDB Not found in configuration");

    return new SqlConnection(cs);
});

//ADD JWT Configuration
builder.Services.AddSingleton<JwtService>();

//AddScoped Repositories
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();
builder.Services.AddScoped<AuthorizationService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Forza Delivery", Version = "v1" });

    //get tag for each controller
    options.DocInclusionPredicate((docname, apiDesc) => 
    { 
        return true; 
    });
    options.TagActionsBy(api => 
    { 
        var controllerName = api.ActionDescriptor.RouteValues.TryGetValue("controller", out var name) ? name : null; 
        return new[] { controllerName ?? "Default" }; 
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token. \n\nExample: `Bearer EncriptionToken123`"
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseJwtMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
