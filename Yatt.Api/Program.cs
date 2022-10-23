using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yatt.Repo.Data;
using Yatt.Repo.Handlers;
using Yatt.Repo.Interfaces;
using Yatt.Repo.Repositories;
using Yatt.Repo.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<AppDbContext>();

#region JWT SETTING
    var jwtSettingsSection = builder.Configuration.GetSection("JWTSettings");
    var jwtSettings = jwtSettingsSection.Get<JWTSettings>();
   
    var secretKey = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.ValidIssuer,
            ValidAudience = jwtSettings.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
      
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) &&
                    (path.StartsWithSegments("/chathub")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

    builder.Services.Configure<JWTSettings>(jwtSettingsSection);

#endregion JWT SETTING

#region CORES CONFIGURATION
    var RequestAllowedOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: RequestAllowedOrigins, builder =>
        {
            builder.WithOrigins("https://localhost:7112")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders("X-Pagination", "access_token");
        });
    });
#endregion CORES CONFIGURATION

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserTokenRepository, UserTokenRepository>();
// CLIENT SERVICE
builder.Services.AddScoped<ICandidateRepository,CandidateRepository>();
builder.Services.AddScoped<ICompanyRepository,CompanyRepository>();
builder.Services.AddScoped<IDomainRepository,DomainRepository>();
// CANDIDATE INFO SERVICE
builder.Services.AddScoped<IExperianceRepository, ExperianceRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();

// COMPANY MEMBERSHIP SERVICE
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// JOB SERVICE
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobEducationRepository, JobEducationRepository>();

builder.Services.AddScoped<IJobDutyRepository, JobDutyRepository>();
//builder.Services.AddScoped<IJobQualificationRepository, JobQualificationRepository>();
builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
                            Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
    RequestPath = new PathString("/StaticFiles")
}); 
app.UseHttpsRedirection();

app.UseCors(RequestAllowedOrigins);

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
