using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Components.Client
{
    public partial class ConfirmationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public string? Content { get; set; }
        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
    }
}
