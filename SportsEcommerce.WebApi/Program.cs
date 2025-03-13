using Core.Tokens.Configurations;
using Core.Tokens.Services;
using FocusList.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using SportsEcommerce.DataAccess.Contexts;
using SportsEcommerce.DataAccess.Extensions;
using SportsEcommerce.Models.Entities;
using SportsEcommerce.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(60);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowMyOrigin", policy =>
  {
    policy.WithOrigins("http://localhost:50599")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
  });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDataAccessDependencies(builder.Configuration);
builder.Services.AddServiceDependencies();

builder.Services.AddScoped<DecoderService>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<TokenOption>(builder.Configuration.GetSection("TokenOption"));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
  opt.User.RequireUniqueEmail = true;
  opt.Password.RequireNonAlphanumeric = true;
  opt.Password.RequireDigit = true;
  opt.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<BaseDbContext>();

var tokenOption = builder.Configuration.GetSection("TokenOption").Get<TokenOption>();

builder.Services.AddAuthentication(opt =>
{
  opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
  opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = tokenOption.Issuer,
    ValidAudience = tokenOption.Audience[0],
    IssuerSigningKey = SecurityKeyHelper.GetSecurityKey(tokenOption.SecurityKey)
  };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.UseStaticFiles();

app.UseSession();
app.UseCors("AllowMyOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
