using Library.Infrastructure.Repositories;
using Library.Infrastructure.Services;
using LibraryDomain;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);






builder.Services.AddCors(opts =>
{
    opts.AddPolicy("UIPolicy", p => p
    .WithOrigins("http://localhost:5500")
    .AllowAnyHeader()
    .AllowAnyMethod());
});

//Jwt auth
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    var cfg = builder.Configuration.GetSection("Jwt");
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidIssuer = cfg["Issuer"],
//        ValidateAudience = true,
//        ValidAudience = cfg["Audience"],
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Secret"])),
//        ValidateLifetime = true
//    };
//});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });

    //var jwtScheme = new OpenApiSecurityScheme
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "bearer",
    //    BearerFormat = "JWT",
    //    In = ParameterLocation.Header,
    //    Description = "Enter 'Bearer {token}'"
    //};
    //c.AddSecurityDefinition("Bearer", jwtScheme);

    //c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    //{
    //    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    //});
});
// in Program.cs before building the app or in startup
var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Library.db");
Console.WriteLine($"DB path: {dbPath}, Exists: {System.IO.File.Exists(dbPath)}");

builder.Services.AddScoped<IAccountRepository, AccountRepositry>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

builder.Services.AddDbContext<LibraryDomain.Models.LibraryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryDb") ?? "Data Source=Library.db"));

var app = builder.Build();

app.UseCors("UIPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API V1"));
}


app.MapControllers();

app.Run();
