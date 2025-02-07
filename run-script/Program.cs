using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using run_script.Data;
using run_script.Repositories;

var builder = WebApplication.CreateBuilder(args);
// tes 123

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// inject db context
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDefaultConnection")
));

// inject repositories
builder.Services.AddScoped<ITokenRepository, TokenRepository>(); // inject the ITokenRepository to the program


builder.Services.AddIdentityCore<IdentityUser>() // mendaftarkan layanan Identity Core ke dalam dependency injection (DI) container
    .AddRoles<IdentityRole>() // Menambahkan dukungan untuk roles-based authorization.
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("run-script") // Menambahkan token provider untuk menghasilkan dan memvalidasi token
    .AddEntityFrameworkStores<AuthDbContext>() // konteks database yang berisi tabel-tabel terkait Identity
    .AddDefaultTokenProviders();
// kebijakan password untuk pengguna
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
}
);

// add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters 
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });


var app = builder.Build();

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
