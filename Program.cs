using SqlSugar;
using TodoApi.Mappings;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ISqlSugarClient>(s =>
{
    SqlSugarScope sqlSugar = new SqlSugarScope(
        new ConnectionConfig()
        {
            DbType = SqlSugar.DbType.Sqlite,
            ConnectionString = "DataSource=sqlsugar-dev.db",
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        },
        db =>
        {
            // Singleton parameter configuration, effective for all contexts
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                System.Console.Error.WriteLine($"🟥[1]: Program.cs:20: sql={sql}");
            };
        }
    );

    // Use CodeFirst to create database and table
    sqlSugar.CodeFirst.InitTables(typeof(Product));
    return sqlSugar;
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

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
