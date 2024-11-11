using APILegalizations.Data.Context;
using APILegalizations.Domain.Exceptions;
using APILegalizations.Domain.Mappers;
using APILegalizations.Domain.Usecases;
using APILegalizations.Domain.Utils;
using APILegalizations.Presenter.Mappers;
using JWT.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LegalizationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); // Esta es la referencia a la cadena de conexión que se encuentra en el archivo appsettings.json
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorHandler>(); // Agregar el filtro de excepciones personalizado
});

// Inject the UserDomainMapper, CreateUserUseCase and Helpper
try
{
    builder.Services.AddScoped<UserDomainMapper>();
    builder.Services.AddScoped<UserPresenterMapper>();

    builder.Services.AddScoped<Login>();
    builder.Services.AddScoped<RefreshTokenDomainMapper>();
    builder.Services.AddScoped<UtilsJwt>();
    builder.Services.AddScoped<LoginPresenterMapper>();


    builder.Services.AddScoped<CreateUser>();
    builder.Services.AddScoped<Helper>();

}
catch (Exception)
{
    Console.WriteLine("[Program.cs] Error al intentar inyectar tus dependencias");
}


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

app.UseAuthorization();

app.MapControllers();

app.Run();
