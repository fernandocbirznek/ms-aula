using Microsoft.EntityFrameworkCore;
using ms_aula;
using ms_aula.Domains;
using ms_aula.Extensions;
using ms_aula.Interface;
using ms_aula.services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Configurar explicitamente a porta HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Porta HTTP
    //options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // Porta HTTPS (opcional)
});

builder.WebHost.UseUrls("http://*:5000");


// Add services to the container.

builder.Services.AddControllers();
builder.Services.SetupDbContext(builder.Configuration.GetValue<string>("ConnectionStrings:DbContext"));
builder.Services.SetupRepositories();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AreaFisica).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AreaFisicaDivisao).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(ArquivoPdf).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Aula).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AulaComentario).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AulaFavoritada).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AulaSessao).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AulaSessaoFavoritada).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AulaTag).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Tag).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(UsuarioAulaCurtido).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(WidgetConcluido).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(WidgetCursando).Assembly));
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(WidgetCursar).Assembly));

// Acessar outro MS
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Services:UsuarioService"));
});

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null;
});

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseRouting();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    c.RoutePrefix = "swagger";  // Isso vai permitir acessar o Swagger via http://localhost:8100/
});

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Services.CreateScope().ServiceProvider.GetRequiredService<AulaDbContext>().Database.Migrate();

app.Run();
