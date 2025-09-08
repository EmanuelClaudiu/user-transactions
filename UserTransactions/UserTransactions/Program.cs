using Microsoft.EntityFrameworkCore;
using UserTransactions.API.Middlewares;
using UserTransactions.Application.Interfaces;
using UserTransactions.Application.Mappers;
using UserTransactions.Application.Services;
using UserTransactions.Infrastructure.Data;
using UserTransactions.Infrastructure.Interfaces;
using UserTransactions.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(cfg => { }, typeof(AutomapperMappingProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();

app.Run();
