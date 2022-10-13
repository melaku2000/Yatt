using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Linq;
using Yatt.Web.Repositories;
using Yatt.Models.Dtos;
using MudBlazor;
using System.Reflection.Metadata.Ecma335;

namespace Yatt.Web.Pages.Candidate
{
    public partial class CandidateEducation
    {
        private string? userId { get; set; } = String.Empty;
        private EducationDto? educationDto { get; set; }//=new CandidateDto();
        private List<EducationDto> educations { get; set; } = new List<EducationDto>();
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<EducationDto> educationRepo { get; set; }
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
            educations = await educationRepo.GetLists($"educations/list/{userId}");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            parameters.Add("education", new EducationDto() { CandidateId=userId, ComplitionYear=DateTime.UtcNow});
            var dialog = await _dialogService.Show<EducationDialog>("Create education", parameters).Result;

            if (dialog.Data != null)
            {
                EducationDto education = dialog.Data as EducationDto;

                var response= await educationRepo.Create("educations", education);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(EducationDto education)
        {
            var parameters = new DialogParameters();
            parameters.Add("education", education);
            var dialog = await _dialogService.Show<EducationDialog>("Edit education", parameters).Result;

            if (dialog.Data != null)
            {
                EducationDto edu = dialog.Data as EducationDto;
                
                var response= await educationRepo.Update($"educations/{edu.Id}", edu);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(EducationDto edu)
        {
            var response = await educationRepo.Delete("educations", edu.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
