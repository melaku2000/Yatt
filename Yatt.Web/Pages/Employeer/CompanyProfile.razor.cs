using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Employeer
{
    public partial class CompanyProfile
    {
        private string? userId { get; set; } = String.Empty;
        private CompanyDto? companyDto { get; set; }//=new CandidateDto();
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<CompanyDto> companyRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var auth = ((await authState).User);
            if (auth != null)
            {
                userId = auth.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value;
            }

            await LoadInitial();
        }
        async Task LoadInitial()
        {
            companyDto = await companyRepo.GetById("Companies", userId);
        }
    }
}
