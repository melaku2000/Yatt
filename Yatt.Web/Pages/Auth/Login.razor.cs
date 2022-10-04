using Microsoft.AspNetCore.Components;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Web.Services;

namespace Yatt.Web.Pages.Auth
{
    public partial class Login
    {
        public LoginDto? loginDto { get; set; } = new LoginDto();
        private bool ShowAuthError { get; set; }
        private string? Error { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
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
                navigationManager.NavigateTo($"/profile/{response.Model!.Id}", true);
            }
        }
        void HideError()
        {
            ShowAuthError = false;
        }
    }
}
