using Masegat.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Implementation;
using Repositories.Interface;
using System.Text;
using Ticketing.BuisinessLayer.Implementation;
using Ticketing.BuisinessLayer.Interface;
using Ticketing.DataAccess.Models;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services;
using Ticketing.Services.Implementation;
using Ticketing.Services.Interface;
using Tickiting.Utility;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//About Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TicketingDbContext>(options =>
options.UseSqlServer(connectionString));

//About Generic Repository 
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
//About User Service
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IUserService, UserService>();
//About Ticket Rpositry
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
//About Ticket Service
builder.Services.AddTransient<ITicketService, TicketService>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
//About token 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };

    });

//----------------------------Scribt-----------------------------------
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<TicketingDbContext>();
    if (!dbContext.UserTypes.Any())
    {
        // Insert data into UserType table
        dbContext.UserTypes.AddRange(
        new UserType { Name = "Manager" },
        new UserType { Name = "Client" },
        new UserType { Name = "Team Member" }
    );

        // Save changes to the UserType table
        dbContext.SaveChanges();
    }
    // Insert data into User table
    var managerUserType = dbContext.UserTypes.FirstOrDefault(u => u.Name == "Manager");

    if (!dbContext.Users.Any())
    {
        dbContext.Users.AddRange(
            new User
            {
                Name = "Manager 1",
                Email = "manager1@example.com",
                Password = Helpers.CreateMD5("1234"),
                RoleId = managerUserType.Id,
                MobileNumber = "1234567890",
                Address = "Manager 1 Address",
                DateOfBirth = DateTime.Now,
                UserName = "manager1",
                IsActive = true
            },
            new User
            {
                Name = "Manager 2",
                Email = "manager2@example.com",
                Password = Helpers.CreateMD5("123"),
                RoleId = managerUserType.Id,
                MobileNumber = "9876543210",
                Address = "Manager 2 Address",
                DateOfBirth = DateTime.Now,
                UserName = "manager2",
                IsActive = true
            }

        // Add more users as needed
        );

        // Save changes to the User table
        dbContext.SaveChanges();
    }
}

//---------------------------------------------------------------------

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
