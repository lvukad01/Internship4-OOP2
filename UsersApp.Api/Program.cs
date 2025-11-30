using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Application.UseCases.Users;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Application.UseCases.Companies;
using UsersApp.Domain.Repositories;
using UsersApp.Infrastructure.Database.Contexts.Users;
using UsersApp.Infrastructure.Database.Contexts.Companies;
using UsersApp.Infrastructure.Repositories.Users;
using UsersApp.Infrastructure.Repositories.Companies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connString = builder.Configuration.GetConnectionString("UsersConnection");
    return new NpgsqlConnection(connString);
});


builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersConnection")));

builder.Services.AddDbContext<CompaniesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersConnection")));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();


builder.Services.AddScoped<ICreateUser, CreateUserHandler>();
builder.Services.AddScoped<IUpdateUser, UpdateUserHandler>();
builder.Services.AddScoped<IGetAllUsers, GetAllUsersHandler>();
builder.Services.AddScoped<IGetUserById, GetUserByIdHandler>();
builder.Services.AddScoped<IActivateUser, ActivateUserHandler>();
builder.Services.AddScoped<IDeactivateUser, DeactivateUserHandler>();
builder.Services.AddScoped<IDeleteUser, DeleteUserHandler>();
builder.Services.AddScoped<IImportUsers, ImportUsersHandler>();


builder.Services.AddScoped<IGetAllCompanies, GetAllCompaniesHandler>();
builder.Services.AddScoped<IGetCompanyID, GetCompanyIDHandler>();
builder.Services.AddScoped<IDeleteCompany, DeleteCompanyHandler>();

builder.Services.AddHttpClient();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


