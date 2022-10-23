using Microsoft.AspNetCore.Components;
using Yatt.Models.Dtos;
using Yatt.Web.Repositories;

namespace Yatt.Web.Components.Client
{
    public partial class ClientJobDutiesComponent
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
    }
}
