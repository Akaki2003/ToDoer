using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.Filters;
using ToDoer.API.Infrastructure.Auth.JWT;
using ToDoer.API.Infrastructure.Extensions;
using ToDoer.Persistence;
using ToDoer.Persistence.Context;
using Serilog;
using ToDoer.API.Infrastructure.Middlewares.ExceptionHandler;
using ToDoer.API.Infrastructure.Middlewares.ExceptionHandling;
using ToDoer.API.Infrastructure.Middlewares.CultureMIddleware;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToDo Api",
        Version = "v1",
        Description = "ToDoerApi",
        Contact = new OpenApiContact
        {
            Email = "akaki.ujarashvili@gmail.com",
            Name = "ToDo Application",
        }
    });
    option.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        //Type = SecuritySchemeType.Http,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "basic",
        In = ParameterLocation.Header,  
        Description = "Basic Authorization header using the Bearer scheme."
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
        }
                });
    option.CustomSchemaIds(type => type.ToString());
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine($"{AppContext.BaseDirectory}", xmlFile);

    option.IncludeXmlComments(xmlPath);
    option.ExampleFilters();
});



builder.Services.AddTokenAuthentication(builder.Configuration.GetSection(nameof(JWTConfiguration)).GetSection(nameof(JWTConfiguration.Secret)).Value);
builder.Services.AddServices();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
builder.Services.Configure<JWTConfiguration>(builder.Configuration.GetSection(nameof(JWTConfiguration)));

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddDbContext<ToDoerContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStrings.DefaultConnection))));

builder.Services.AddScoped<DbContext, ToDoerContext>();

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetExecutingAssembly());
var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestAndResponseLoggingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();        

app.UseAuthorization();
app.UseMiddleware<Culturemiddleware>();


app.MapControllers();

try
{
    Log.Information("Starting...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated");
}
finally
{
    Log.CloseAndFlush();
}

