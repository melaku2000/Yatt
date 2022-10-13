using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Security.Claims;
using Yatt.Models.Dtos;
using Yatt.Web.Pages.Candidate;
using Yatt.Web.Repositories;

namespace Yatt.Web.Pages.Employeer
{
    public partial class CompanySubscription
    {
        private string? userId { get; set; } = String.Empty;
        private SubscriptionDto? SubscriptionDto { get; set; }//=new CandidateDto();
        private List<SubscriptionDto> subscriptions { get; set; } = new List<SubscriptionDto>();
        private List<MembershipDto> memberships { get; set; } = new List<MembershipDto>();
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        [Inject]
        public IYattRepository<MembershipDto> memberRepo { get; set; }
        [Inject]
        public IYattRepository<SubscriptionDto> subscriptionRepo { get; set; }
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
            subscriptions = await subscriptionRepo.GetLists($"Subscriptions/listByCompanyId/{userId}");
            memberships = await memberRepo.GetLists("Memberships/list");
        }
        private async Task Create()
        {
            var parameters = new DialogParameters();
            parameters.Add("subscription", new SubscriptionDto() { CompanyId = userId,Memberships=memberships});
            var dialog = await _dialogService.Show<SubscriptionDialog>("Create Subscription", parameters).Result;

            if (dialog.Data != null)
            {
                SubscriptionDto subscription = dialog.Data as SubscriptionDto;

                var response = await subscriptionRepo.Create("Subscriptions", subscription);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        private async Task Update(SubscriptionDto subscription)
        {
            var parameters = new DialogParameters();
            subscription.Memberships = memberships;

            parameters.Add("subscription", subscription);
            var dialog = await _dialogService.Show<SubscriptionDialog>("Edit subscription", parameters).Result;

            if (dialog.Data != null)
            {
                SubscriptionDto subs = dialog.Data as SubscriptionDto;

                var response = await subscriptionRepo.Update($"Subscriptions/{subs.Id}", subs);
                if (response.Status == Models.Enums.ResponseStatus.Success)
                    await LoadInitial();
            }
        }
        async Task DeleteAsync(SubscriptionDto subs)
        {
            var response = await subscriptionRepo.Delete("Subscriptions", subs.Id);
            if (response.Status == Models.Enums.ResponseStatus.Success)
                await LoadInitial();
        }
    }
}
