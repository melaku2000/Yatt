using Microsoft.AspNetCore.Components;
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
                navigationManager.NavigateTo("/candidate/profile", true);
            }
        }
        void HideError()
        {
            ShowAuthError = false;
        }
    }
}
