using AutoMapper;
using GeekShopping.ProductAPI.AutoMapperconfig;
using GeekShopping.ProductAPI.DTOs;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var con = builder.Configuration["MySQLConfig:ConnectionStrings"];

builder.Services.AddDbContext<MySQLContext>(options =>
{
    options.UseMySql(con, ServerVersion.AutoDetect(con));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/Products/GetAll", async (IProductRepository rep) =>
{
    return await rep.FindAll();

});

app.MapGet("/Products/GetById/{id}", async (IProductRepository rep, long id) =>
{
    return await rep.FindById(id) is ProductDTO prod ? Results.Ok(prod) : Results.NotFound("Opps, not found");

});

app.MapPost("/Products/Create", async (IProductRepository rep, ProductDTO prod) =>
{
    if (prod == null)
    {
        Results.BadRequest();
    }
    else
    {
        await rep.Create(prod);
        Results.Ok();
    }
});

app.MapPut("/Products/Update", async (IProductRepository rep, ProductDTO prod) =>
{
    if (prod is null) Results.BadRequest();
    await rep.Update(prod);
    return Results.Created("/Products/GetById/{id}", prod);
});

app.MapDelete("/Products/Delete/{id}", async (IProductRepository rep, long id) =>
{
    return await rep.Delete(id) is false ? Results.BadRequest() : Results.Ok($"Produto {id} Deletado");
});
app.UseAuthorization();

app.MapControllers();

app.Run();
