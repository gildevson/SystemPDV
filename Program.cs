
using SistemaCaixa.Data;
using SistemaCaixa.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔥 CARREGAR appsettings.json (ESSENCIAL)
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ========================
// INJEÇÃO DE DEPENDÊNCIA
// ========================
builder.Services.AddSingleton<DbContextDapper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// ========================
// CORS – permitir Angular
// ========================
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy =>
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

// ========================
// Controllers e Swagger
// ========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "FinanblueBackend API",
        Version = "v1",
        Description = "API do Gilson usando .NET + Dapper"
    });
});

var app = builder.Build();

// ========================
// Pipeline
// ========================
app.UseCors("AllowAngular");

// ❗ Não usar HTTPS enquanto testa local
// app.UseHttpsRedirection();

app.UseRouting();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanblueBackend API v1");
    });
}

app.MapControllers();

app.Run();
