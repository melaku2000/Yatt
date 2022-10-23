using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using System.Linq;
using Yatt.Web.Repositories;
using Yatt.Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Yatt.Models.Constants;
using System.Reflection.Metadata;

namespace Yatt.Web.Pages.Candidate
{
    public partial class CandidateProfile
    {
        private string? userId { get; set; } = String.Empty;
        private CandidateDto? candidateDto { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<CandidateDto> candidateRepo { get; set; }
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
            candidateDto = await candidateRepo.GetById("candidates", userId);
        }
    }
}
