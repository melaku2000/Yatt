using Microsoft.AspNetCore.Components;
using Yatt.Models.Constants;
using Yatt.Models.Dtos;
using Yatt.Web.Services;

namespace Yatt.Web.Components.User
{
    public partial class ProfileImageComponent
    {
        [Parameter]
        public string? UserId { get; set; }
        private FileData imageData { get; set; } = new FileData();
        string profileImgUrl = string.Empty;
        [Inject]
        public IFileService fileService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadImage();
        }
        async Task LoadImage()
        {
            imageData = await fileService.GetProfileImage(UserId!);
            if (imageData != null)
            {
                profileImgUrl = FileConstants.GeFileContent(imageData);
                //profileImgUrl = $"data:{imageData.DataType};base64,{imageData.FileBase64data}";
            }
        }
    }
}
