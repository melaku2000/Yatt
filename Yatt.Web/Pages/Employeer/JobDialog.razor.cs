using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Pages.Employeer
{
    public partial class JobDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public JobDto job { get; set; } = new JobDto();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Submit()
        {
            MudDialog.Close(DialogResult.Ok<JobDto>(job));
        }
    }
}
