using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Linq;
using Yatt.Web.Repositories;
using Yatt.Models.Dtos;
using MudBlazor;

namespace Yatt.Web.Pages.Candidate
{
    public partial class CandidateExperiances
    {
        private string? userId { get; set; } = String.Empty;
        private ExperianceDto? experianceDto { get; set; }//=new CandidateDto();
        private List<ExperianceDto> experiances { get; set; } = new List<ExperianceDto>();
        private List<DomainDto> domains { get; set; } = new List<DomainDto>();
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<ExperianceDto> experianceRepo { get; set; }
        [Inject]
        public IYattRepository<DomainDto> domainRepo { get; set; }
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
            experiances = await experianceRepo.GetLists($"Experiances/list/{userId}");
            domains = await domainRepo.GetLists("Domains/GetList");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            var exp = new ExperianceDto()
            {
                CandidateId = userId,
                HiringDate = DateTime.UtcNow,
                LastDate = DateTime.UtcNow,
                Domains = domains
            };
            parameters.Add("experiance", exp);
            var dialog = await _dialogService.Show<Experiancedialog>("Add experiance", parameters).Result;

            if (dialog.Data != null)
            {
                ExperianceDto experiance = dialog.Data as ExperianceDto;

                var response = await experianceRepo.Create("Experiances", experiance);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(ExperianceDto experiance)
        {
            var parameters = new DialogParameters();
            experiance.Domains = domains;
            parameters.Add("experiance", experiance);
            var dialog = await _dialogService.Show<Experiancedialog>("Edit experiance", parameters).Result;

            if (dialog.Data != null)
            {
                ExperianceDto exp = dialog.Data as ExperianceDto;

                var response = await experianceRepo.Update($"Experiances/{exp.Id}", exp);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(ExperianceDto exp)
        {
            var response = await experianceRepo.Delete("Experiances", exp.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
