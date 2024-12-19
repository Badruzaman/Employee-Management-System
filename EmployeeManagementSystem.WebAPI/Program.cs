using EmployeeManagementSystem.DataAccess;
using EmployeeManagementSystem.DataAccess.Implementations;
using EmployeeManagementSystem.DataAccess.Interfaces;
using EmployeeManagementSystem.Services.Implementations;
using EmployeeManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmpMgtSysContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EMSConnection")));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPerformanceReviewRepository, PerformanceReviewRepository>();
builder.Services.AddScoped<IPerformanceReviewService, PerformanceReviewService>();

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
