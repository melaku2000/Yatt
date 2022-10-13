using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Components.Job
{
    public partial class JobEducationDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public JobEducationDto JobEducation { get; set; } = new JobEducationDto();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Submit()
        {
            MudDialog.Close(DialogResult.Ok<JobEducationDto>(JobEducation));
        }
    }
}
