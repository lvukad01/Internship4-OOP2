using UsersApp.Application.UseCases.Companies;
using UsersApp.Application.UseCases.ICompanies;
using UsersApp.Application.UseCases.IUsers;
using UsersApp.Application.UseCases.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tvoje DI registracije
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
