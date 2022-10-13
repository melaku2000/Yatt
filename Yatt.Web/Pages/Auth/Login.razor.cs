using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Yatt.Models.Constants;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Web.Services;

namespace Yatt.Web.Pages.Auth
{
    public partial class Login
    {
        private LoginDto loginDto { get; set; } = new LoginDto();
        private bool ShowAuthError { get; set; }
        private string? Error { get; set; }
      
        [Inject]
        public IAuthService authService { get; set; }
        [CascadingParameter]
        private Task<AuthenticationState> authState { get; set; }
        async Task OnSubmit()
        {
            ShowAuthError = false;
            var response = await authService.Login(loginDto);
            if (response.Status != ResponseStatus.Success)
            {
                Error = response!.Message;
                ShowAuthError = true;
            }
            else
            {
                var auth = ((await authState).User);
                if (auth != null && auth.Identity!.IsAuthenticated)
                {
                    if(auth.IsInRole(RoleType.Candidate.ToString()))
                        navigationManager.NavigateTo("/candidate/profile", true);
                    if (auth.IsInRole(RoleType.Employeer.ToString()))
                        navigationManager.NavigateTo("/employeer/profile", true);
                    if (auth.IsInRole(RoleType.SuperAdmin.ToString()))
                        navigationManager.NavigateTo("/", true);
                }
            }
        }
        void HideError()
        {
            ShowAuthError = false;
        }
    }
}
