using Microsoft.AspNetCore.Components;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Web.Repositories;
using Yatt.Web.Services;

namespace Yatt.Web.Components.Auth
{
    public partial  class CandidateRegisterComponent
    {
        private RegisterDto registerDto { get; set; } = new RegisterDto();
        private bool ShowAuthError { get; set; }
        private string? Error { get; set; }
     
        [Inject]
        public IAuthService authService { get; set; }
       
        async Task OnSubmit()
        {
            ShowAuthError = false;
            var response = await authService.Register(registerDto);
            if (response.Status != ResponseStatus.Success)
            {
                Error = response!.Message;
                ShowAuthError = true;
            }
            else
                navigationManager.NavigateTo("/");

        }
        void HideError()
        {
            ShowAuthError = false;
        }
    }
}
