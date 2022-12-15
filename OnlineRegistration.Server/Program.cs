using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineRegisteration.Server;
using OnlineRegistration.Server.Configurations;
using OnlineRegistration.Server.Repos;
using OnlineRegistration.Server.ReposInterface;
using OnlineRegistration.Server.Services;
using OnlineRegistration.Server.ServicesInterfaces;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RegistrationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IRegPeriodService, RegPeriodService>();
builder.Services.AddScoped<IRegPeriodRepo, RegPeriodRepo>();
builder.Services.AddAuthorization();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{

    var Key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key:"JwtConfig:Secret").Value);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ValidateIssuer = false, //for dev
        ValidateAudience = false, //for dev
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = false //for dev
        
    };
});

builder.Services.AddDefaultIdentity<IdentityUser>(Options => Options.SignIn.RequireConfirmedEmail = false)
    .AddEntityFrameworkStores<RegistrationDBContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("Open");

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
