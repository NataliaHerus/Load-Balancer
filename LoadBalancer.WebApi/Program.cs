using AutoMapper;
using LoadBalancer.IdenityServer.Data.Repositories;
using LoadBalancer.IdenityServer.Data.Repositories.Interfaces;
using LoadBalancer.WebApi;
using LoadBalancer.WebApi.Data;
using LoadBalancer.WebApi.Repositories;
using LoadBalancer.WebApi.Repositories.Interfaces;
using LoadBalancer.WebApi.Services;
using LoadBalancer.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LoadBalancerDbContext>(options =>
    options.UseSqlServer(connectionString!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var identityUrl = builder.Configuration.GetValue<string>("IdentityUrl");

        opt.RequireHttpsMetadata = false;
        opt.Authority = identityUrl;
        opt.Audience = "LoadBalancerAPI";
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            // As issuer is HTTPS localhost, and authority is HTTP docker, but should be the same
            ValidateIssuer = false,
        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IPuzzleRepository, PuzzleRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();

builder.Services.AddScoped<IPuzzleService, PuzzleService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
