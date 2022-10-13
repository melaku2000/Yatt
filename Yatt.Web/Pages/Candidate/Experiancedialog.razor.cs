using Microsoft.AspNetCore.Components;
using MudBlazor;
using Yatt.Models.Dtos;

namespace Yatt.Web.Pages.Candidate
{
    public partial class Experiancedialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public ExperianceDto experiance { get; set; } = new ExperianceDto();

        private void Cancel()
        {
            MudDialog.Cancel();
        }

        private void Submit()
        {
            MudDialog.Close(DialogResult.Ok<ExperianceDto>(experiance));
        }
    }
}
