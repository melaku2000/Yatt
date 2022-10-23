using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;
using Yatt.Web.Components.Job;
using Yatt.Web.Repositories;

namespace Yatt.Web.Components.Client
{
    public partial class ClientJobEducationComponent
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
        async Task LoadInitial()
        {
            educations = await jobEducationRepo.GetLists($"JobEducations/listByJobId/{JobId}");
        }
       
    }
}
