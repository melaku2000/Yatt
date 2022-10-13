using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Pages.Candidate;
using Yatt.Web.Repositories;

namespace Yatt.Web.Components.Job
{
    public partial class JobEducationComponent
    {
        [Parameter]
        public string? JobId { get; set; } = String.Empty;
        private JobEducationDto? educationDto { get; set; }
        private List<JobEducationDto> educations { get; set; } = new List<JobEducationDto>();
     
        [Inject]
        public IYattRepository<JobEducationDto> jobEducationRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if(firstRender && educations != null)   
        //}
        async Task LoadInitial()
        {
            educations = await jobEducationRepo.GetLists($"JobEducations/listByJobId/{JobId}");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            var job = new JobEducationDto()
            {
                JobId = JobId,
            };
            parameters.Add("jobEducation", job);
            var dialog = await _dialogService.Show<JobEducationDialog>("Add education", parameters).Result;

            if (dialog.Data != null)
            {
                JobEducationDto jobEducation = dialog.Data as JobEducationDto;

                var response = await jobEducationRepo.Create("JobEducations", jobEducation);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(JobEducationDto jobDto)
        {
            var parameters = new DialogParameters();
            
            parameters.Add("jobEducation", jobDto);
            var dialog = await _dialogService.Show<JobEducationDialog>("Edit education", parameters).Result;

            if (dialog.Data != null)
            {
                JobEducationDto eduction = dialog.Data as JobEducationDto;

                var response = await jobEducationRepo.Update($"JobEducations/{eduction.Id}", eduction);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(JobEducationDto exp)
        {
            var response = await jobEducationRepo.Delete("JobEducations", exp.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
