using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Web.Components.Client;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Clients
{
    public partial class ApplayJob
    {
        [Parameter]
        public string? JobId { get; set; } = String.Empty;
        private JobDto? jobDto { get; set; }

        [Inject]
        public IYattRepository<JobDto> jobRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        async Task LoadInitial()
        {
            jobDto = await jobRepo.GetById("Jobs", JobId);
        }
        async Task ChangeStateAsync(JobDto vacancy)
        {
            var response = await jobRepo.Delete("Jobs", vacancy.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
        private async void JobApplay()
        {
            var parameters = new DialogParameters();
            parameters.Add("Content", "Are you sure you want to applay for this job?");

           var dialog=await  _dialogService.Show<ConfirmationDialog>("Job applay", parameters).Result;
        }
    }
}
