using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Employeer
{
    public partial class JobDetail
    {
        [Parameter]
        public string? jobId { get; set; } = String.Empty;
        private JobDto? jobDto { get; set; }
        
        [Inject]
        public IYattRepository<JobDto> jobRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        async Task LoadInitial()
        {
            jobDto = await jobRepo.GetById("Jobs",jobId);
        }
        async Task ChangeStateAsync(JobDto vacancy)
        {
            var response = await jobRepo.Delete("Jobs", vacancy.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
