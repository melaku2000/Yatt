using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;

namespace Yatt.Web.Components.Job
{
    public partial class JobDutyComponent
    {
        [Parameter]
        public string? JobId { get; set; } = String.Empty;
        private JobDutyDto? dutyDto { get; set; }
        private List<JobDutyDto> jobDuties { get; set; } = new List<JobDutyDto>();

        [Inject]
        public IYattRepository<JobDutyDto> jobEducationRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        async Task LoadInitial()
        {
            jobDuties = await jobEducationRepo.GetLists($"JobDuties/listByJobId/{JobId}");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            var duty = new JobDutyDto()
            {
                JobId = JobId,
            };
            parameters.Add("dutyDto", duty);
            var dialog = await _dialogService.Show<JobDutyDialog>("Add duty", parameters).Result;

            if (dialog.Data != null)
            {
                JobDutyDto jobduty = dialog.Data as JobDutyDto;

                var response = await jobEducationRepo.Create("JobDuties", jobduty);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(JobDutyDto jobDto)
        {
            var parameters = new DialogParameters();

            parameters.Add("dutyDto", jobDto);
            var dialog = await _dialogService.Show<JobDutyDialog>("Edit duty", parameters).Result;

            if (dialog.Data != null)
            {
                JobDutyDto duty = dialog.Data as JobDutyDto;

                var response = await jobEducationRepo.Update($"JobDuties/{duty.Id}", duty);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(JobDutyDto exp)
        {
            var response = await jobEducationRepo.Delete("JobDuties", exp.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
