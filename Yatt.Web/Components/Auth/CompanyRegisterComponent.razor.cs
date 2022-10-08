using Microsoft.AspNetCore.Components;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Web.Repositories;
using Yatt.Web.Services;

namespace Yatt.Web.Components.Auth
{
    public partial class CompanyRegisterComponent
    {
        private RegisterCompanyDto registerDto { get; set; } = new RegisterCompanyDto();
        private bool ShowAuthError { get; set; }
        private string? Error { get; set; }

        [Inject]
        public IAuthService authService { get; set; }

        async Task OnSubmit()
        {
            ShowAuthError = false;
            var response = await authService.RegisterCompany(registerDto);
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
