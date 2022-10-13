using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;
using Yatt.Web.Pages.Candidate;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Admins
{
    public partial class AdminMembership
    {
        private MembershipDto? membershipDto { get; set; }//=new CandidateDto();
        private List<MembershipDto> memberships { get; set; } = new List<MembershipDto>();

        [Inject]
        public IYattRepository<MembershipDto> membershipepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        async Task LoadInitial()
        {
            memberships = await membershipepo.GetLists("Memberships/list");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            parameters.Add("membership", new MembershipDto());
            var dialog = await _dialogService.Show<MembershipDialog>("Create membership", parameters).Result;

            if (dialog.Data != null)
            {
                MembershipDto mem = dialog.Data as MembershipDto;

                var response = await membershipepo.Create("Memberships", mem);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(MembershipDto membership)
        {
            var parameters = new DialogParameters();
            parameters.Add("membership", membership);
            var dialog = await _dialogService.Show<MembershipDialog>("Edit membership", parameters).Result;

            if (dialog.Data != null)
            {
                MembershipDto edu = dialog.Data as MembershipDto;

                var response = await membershipepo.Update($"Memberships/{edu.Id}", edu);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(MembershipDto edu)
        {
            var response = await membershipepo.Delete("Memberships", edu.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
