using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Linq;
using Yatt.Web.Repositories;
using Yatt.Models.Dtos;


namespace Yatt.Web.Pages.Candidate
{
    public partial class CandidateEducation
    {
        private string? userId { get; set; } = String.Empty;
        private EducationDto? educationDto { get; set; }//=new CandidateDto();
        private List<EducationDto> educations { get; set; }
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
            educations = await educationRepo.GetLists($"educations/{userId}");
        }
    }
}
