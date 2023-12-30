using BackEnd.Api.Models;
using BackEnd.Api.Services.Contrato;
using BackEnd.Api.Services.Implementacion;
using BackEnd.Api.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbempleadoContext>(option =>
                                option.UseSqlServer(builder.Configuration.GetConnectionString("sqlString"))
                                );

builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => options.AddPolicy("AllCors", builder =>
                                                                    builder.AllowAnyHeader()
                                                                           .AllowAnyMethod()
                                                                           .AllowAnyOrigin()));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.UseCors("AllCors");
app.Run();
