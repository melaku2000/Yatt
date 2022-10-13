using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Employeer
{
    public partial class CompanyJobs
    {
        private string? userId { get; set; } = String.Empty;
        private JobDto? jobDto { get; set; }//=new CandidateDto();
        private List<JobDto> jobs { get; set; } = new List<JobDto>();
        private List<SubscriptionDto> subscriptions { get; set; } = new List<SubscriptionDto>();
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<SubscriptionDto> subscriptionRepo { get; set; }
        [Inject]
        public IYattRepository<JobDto> jobRepo { get; set; }
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
            jobs = await jobRepo.GetLists($"Jobs/listByCompany/{userId}");
            subscriptions = await subscriptionRepo.GetLists($"Subscriptions/listByCompanyId/{userId}");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            parameters.Add("job", new JobDto() { DeadLineDate = DateTime.UtcNow, Subscriptions = subscriptions });
            var dialog = await _dialogService.Show<JobDialog>("Create Job", parameters).Result;

            if (dialog.Data != null)
            {
                JobDto job = dialog.Data as JobDto;
                job!.Subscriptions = null;
                var response = await jobRepo.Create("Jobs", job);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(JobDto job)
        {
            var parameters = new DialogParameters();
            job.Subscriptions = subscriptions;
            parameters.Add("job", job);
            var dialog = await _dialogService.Show<JobDialog>("Edit Job", parameters).Result;

            if (dialog.Data != null)
            {
                JobDto jo = dialog.Data as JobDto;
                jo!.Subscriptions = null;

                var response = await jobRepo.Update($"Jobs/{jo.Id}", jo);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(JobDto vacancy)
        {
            var response = await jobRepo.Delete("Jobs", vacancy.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
