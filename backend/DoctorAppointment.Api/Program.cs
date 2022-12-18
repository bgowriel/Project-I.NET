using DoctorAppointment.Api;
using DoctorAppointment.Api.Exceptions;
using DoctorAppointment.Api.Middleware;
using DoctorAppointment.Api.Services;
using DoctorAppointment.Application;
using DoctorAppointment.Application.Interfaces;
using DoctorAppointment.DataAccess;
using DoctorAppointment.DataAccess.Repositories;
using DoctorAppointment.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;


var MyRules = "IMakeTheRules";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<IMedicalVisitRepository, MedicalVisitRepository>();
builder.Services.AddScoped<IOfficeRepository,OfficeRepository>();
builder.Services.AddScoped<IAvailableDateRepository, AvailableDateRepository>();


builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyRules,
                      policy =>
                      {
                          //allow only http://localhost:4200
                          policy.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    if (builder.Configuration["JWT:Secret"] == null)
    {
        throw new JWTException("JWT:Secret not found");
    }
    
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ContentEditor", policy => policy.RequireRole("Admin"));
    options.AddPolicy("ApproveAppointments", policy => policy.RequireRole("Doctor"));
});


if (typeof(AssemblyMarker) == null)
{
    throw new AssemblyException("MediatR assembly not found");
}

builder.Services.AddMediatR(Assembly.GetAssembly(typeof(AssemblyMarker)));
builder.Services.AddAutoMapper(typeof(DoctorAppointmentPresentation));

// add SeedDBService to the container
builder.Services.AddScoped<SeedDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors(MyRules);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
