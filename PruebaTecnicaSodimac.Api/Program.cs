using Microsoft.OpenApi.Models;
using PruebaTecnicaSodimac.Application;
using PruebaTecnicaSodimac.Infrastructure;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Cambiar la cultura predeterminada a español de Colombia o España
var cultureInfo = new CultureInfo("es-CO"); // o "es-ES"
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

string versionApi = "v1.0";
string? AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
// Add services to the container.

builder.Services.AddControllers();
builder.AddDbContext();

var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(",");

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders("cantidad-total-registros");
    });
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc(versionApi, new OpenApiInfo
    {
        Title = $"Prueba Tecnica Sodimac Back",
        Version = versionApi,
        Description = "Api ",
        Contact = new OpenApiContact
        {
            Name = ""
        }
    });
    // Get the XML file path for your API project
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"../swagger/{versionApi}/swagger.json", $"API {versionApi}");
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
