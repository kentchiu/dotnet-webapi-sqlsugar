using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.IdentityModel.Tokens;
using SqlSugar;
using TodoApi.Mappings;
using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logging =>
{
    logging.AddConsole(options =>
    {
        // options.Format = ConsoleLoggerFormat.Default; // Use the format you want here
    });
});

// Add services to the container.
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    // Add a logger
    var logger = s.GetRequiredService<ILogger<Program>>();

    SqlSugarScope sqlSugar = new SqlSugarScope(
        new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
            ConnectionString = "DataSource=sqlsugar-dev.db",
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute,
        },
        db =>
        {
            // Singleton parameter configuration, effective for all contexts
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                logger.LogWarning($"SQL: {sql}");
            };
        }
    );

    // Use CodeFirst to create database and table
    sqlSugar.CodeFirst.InitTables(typeof(Product));
    return sqlSugar;
});

#region 註冊驗証
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.IncludeErrorDetails = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Settings:JwtSettings:Issuer"),
            ValidateAudience = true,
            ValidAudience = "The Audience",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetValue<string>("Settings:JwtSettings:SignKey")
                )
            ),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization();
#endregion

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
