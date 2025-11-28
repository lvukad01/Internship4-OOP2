using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Npgsql;
using UsersApp.Application.Interfaces;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Domain.Repositories;
using UsersApp.Infrastructure.Database.Contexts.Companies;
using UsersApp.Infrastructure.Database.Contexts.Users;
using UsersApp.Infrastructure.Repositories.Companies;
using UsersApp.Infrastructure.Repositories.Users;
using UsersApp.Infrastructure.External;
using UsersApp.Application.UseCases.Users;
using UsersApp.Application.UseCases.Companies;

namespace UsersApp.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string usersConnectionString, string companiesConnectionString)
        {
            services.AddDbContext<UsersDbContext>(options =>
                options.UseNpgsql(usersConnectionString));

            services.AddDbContext<CompaniesDbContext>(options =>
                options.UseNpgsql(companiesConnectionString));

            services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(usersConnectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<ICreateUser, CreateUserHandler>();
            services.AddScoped<IUpdateUser, UpdateUserHandler>();
            services.AddScoped<IActivateUser, ActivateUserHandler>();
            services.AddScoped<IDeactivateUser, DeactivateUserHandler>();
            services.AddScoped<IDeleteUser, DeleteUserHandler>();
            services.AddScoped<IGetAllUsers, GetAllUsersHandler>();
            services.AddScoped<IGetUserById, GetUserByIdHandler>();
            services.AddScoped<IImportUsers, ImportUsersHandler>();

            services.AddScoped<ICreateCompany, CreateCompanyHandler>();
            services.AddScoped<IGetAllCompanies, GetAllCompaniesHandler>();
            services.AddScoped<IGetCompanyID, GetCompanyIDHandler>();
            services.AddScoped<IUpdateCompany, UpdateCompanyHandler>();
            services.AddScoped<IDeleteCompany, DeleteCompanyHandler>();

            services.AddScoped<ExternalUserService>();

            return services;
        }
    }
}
