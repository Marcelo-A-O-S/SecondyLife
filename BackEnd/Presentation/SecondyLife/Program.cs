using Business.Repository;
using Business.Repository.Interfaces;
using Business.Services;
using Business.TokenJWT;
using Business.TokenJWT.ITokenJWT;
using Database.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelDomain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(ConnectionString));
builder.Services.AddScoped<IGerenciadorEnderecoRepository, GerenciadorEnderecoRepository>();
builder.Services.AddScoped<ITokenJwt, TokenJwt>();
//builder.Services.AddScoped<IGerenciadorEndereco, GerenciadorEndereco>();
builder.Services.AddScoped<IRepositoryGeneric<Endereco>, RepositoryGeneric<Endereco>>();
builder.Services.AddScoped<IRepositoryGeneric<Autenticacao>, RepositoryGeneric<Autenticacao>>();
builder.Services.AddScoped<GerenciadorEndereco>();
builder.Services.AddScoped<DataAuthentication>();
builder.Services.AddIdentity<Usuario, IdentityRole<int>>()
	.AddEntityFrameworkStores<Contexto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
