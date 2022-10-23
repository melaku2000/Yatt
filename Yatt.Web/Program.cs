using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Web;
using Yatt.Web.Extensions;
using Yatt.Web.Repositories;
using Yatt.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7248/api/")
});

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight; });
builder.Services.AddSingleton<Navigation>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<ITokenManagerService, TokenManagerService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IYattRepository<CandidateDto>, YattRepository<CandidateDto>>();
builder.Services.AddScoped<IYattRepository<DomainDto>, YattRepository<DomainDto>>();
builder.Services.AddScoped<IYattRepository<Country>, YattRepository<Country>>();
builder.Services.AddScoped<IYattRepository<EducationDto>, YattRepository<EducationDto>>();
builder.Services.AddScoped<IYattRepository<ExperianceDto>, YattRepository<ExperianceDto>>();
builder.Services.AddScoped<IYattRepository<CompanyDto>, YattRepository<CompanyDto>>();
builder.Services.AddScoped<IYattRepository<MembershipDto>, YattRepository<MembershipDto>>();
builder.Services.AddScoped<IYattRepository<SubscriptionDto>, YattRepository<SubscriptionDto>>();
// JOB SERVICES
builder.Services.AddScoped<IYattRepository<JobDto>, YattRepository<JobDto>>();
builder.Services.AddScoped<IYattRepository<JobEducationDto>, YattRepository<JobEducationDto>>();
builder.Services.AddScoped<IYattRepository<JobDutyDto>, YattRepository<JobDutyDto>>();

await builder.Build().RunAsync();
