using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Servicos;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProjetoRecepcao.Conversores;
using ProjetoRecepcao.Roteamento;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.Converters.Add(new DateOnlyConverterr());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));//String para a conexão

builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<IReposicaoService, ReposicaoService>();


builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("date", typeof(DateOnlyRouteConstraint));
    options.ConstraintMap.Add("customConstraint", typeof(CustomRouteConstraint));
});



//licença global
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // Definindo globalmente





builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3000");

app.UseAuthorization();

app.MapControllers();

app.Run();
