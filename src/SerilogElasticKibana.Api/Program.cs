using Newtonsoft.Json;
using Serilog;
using SerilogElasticKibana.Api.StartupConfiguration;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog(builder.Configuration);
builder.Host.UseSerilog();
builder.Services.AddControllers()//(opt => { opt.Filters.Add(typeof(ExceptionFilterAttribute)); })
                .AddNewtonsoftJson(opt => { opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddCustomApiVersioning();
builder.Services.AddCors();

var app = builder.Build();
app.UseSerilog();
app.UseCustomSwagger(builder.Configuration);
app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();