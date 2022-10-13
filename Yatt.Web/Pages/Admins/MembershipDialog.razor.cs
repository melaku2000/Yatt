using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Pages.Admins
{
    public partial class MembershipDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public MembershipDto membership { get; set; } = new MembershipDto();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Submit()
        {
            MudDialog.Close(DialogResult.Ok<MembershipDto>(membership));
        }
    }
}
