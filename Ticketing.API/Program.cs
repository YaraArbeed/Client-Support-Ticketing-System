using Masegat.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories.Implementation;
using Repositories.Interface;
using System.Reflection;
using System.Text;
using Ticketing.API.GlobalExceptionHandler;
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

builder.Services.AddSwaggerGen(options =>
{

options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Client Support Ticketing System APIs",
    Description = "API for managing support tickets on different company products",
    Contact = new OpenApiContact
    {
        Name = "Yara",
        Email = "yaraxd@gmail.com",
        Url = new Uri("https://www.linkedin.com/in/yara-al-arbeed-94730b231/")
    } 
    
});
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

//About Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TicketingDbContext>(options =>
options.UseSqlServer(connectionString));

//About Generic Repository 
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
//About User Service&User Repository
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
//About Ticket Rpositry
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
//About Product Rpositry
builder.Services.AddTransient<IProductRepository, ProductRepository>();
//About Status Rpositry
builder.Services.AddTransient<IStateRepository, StateRepository>();
//About Ticket Service
builder.Services.AddTransient<ITicketService, TicketService>();
//About Product Service
builder.Services.AddTransient<IProductService, ProductService>();
//About Token Service
builder.Services.AddTransient<ITokenService, TokenService>();
//About token 
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

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
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: builder.Configuration.GetValue<string>("Origins:OriginName"),
                      policy =>
                      {
                          policy.WithOrigins( builder.Configuration.GetValue<string>("Origins:WebUrl")).AllowAnyMethod().AllowAnyHeader();
                      });
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
    //Insert Data into product tabel
    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
        new Product { Name = "Avilo" }

    );
        // Save changes to the UserType table
        dbContext.SaveChanges();
    }
    //Insert Data into Status tabel
    if (!dbContext.States.Any())
    {
        dbContext.States.AddRange(
        new State { Name = "New" },
        new State {Name="Approve"},
        new State { Name = "To Do" },
        new State { Name = "In Progress" },
        new State { Name = "Done" },
        new State { Name = "Closed" }

    );
        // Save changes to the State table
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
    //Insert Data into Ticket tabel

    if (!dbContext.Tickets.Any())
    {
        dbContext.Tickets.AddRange(
        new Ticket { Title = "Problem 1", Description = "I can't use thsi product", StateId = 1, AssigneeId = 1, ProductId = 1, CustomerId = 1, Attachments = "picture" }

    );

        // Save changes to the UserType table
        dbContext.SaveChanges();
    }
}

//---------------------------------------------------------------------

// Configure the HTTP request pipeline.
 
 app.UseSwagger();
 app.UseSwaggerUI();
app.AddGlobalErrorHandler();
app.UseHttpsRedirection();

app.UseCors(builder.Configuration.GetValue<string>("Origins:OriginName"));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.MapControllers();

app.Run();
