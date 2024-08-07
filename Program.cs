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

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Services.CreateScope().ServiceProvider.GetRequiredService<AulaDbContext>().Database.Migrate();

app.Run();
