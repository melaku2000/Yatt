using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Pages.Candidate
{
    public partial class EducationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public EducationDto education { get; set; } = new EducationDto();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Submit()
        {
            MudDialog.Close(DialogResult.Ok<EducationDto>(education));
        }

    }
}
