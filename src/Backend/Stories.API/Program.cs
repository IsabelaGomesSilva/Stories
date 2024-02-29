using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Stories.API.Handlers;
using Stories.Data.Context;
using Stories.Service.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy( policy  =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                                              
                      });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<CreateStoryHandler>();
builder.Services.AddTransient<GetAllStoryHandler>();
builder.Services.AddTransient<GetByIdStoryHandler>();
builder.Services.AddTransient<DeleteStoryHandler>();
builder.Services.AddTransient<UpdateStoryHandler>();

builder.Services.AddMediatR( add => add.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(
    x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DepartmentService>();
builder.Services.AddScoped<StoryService>();


var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
