using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Yatt.Models.Constants;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;
using Yatt.Web.Repositories;
using Yatt.Web.Services;

namespace Yatt.Web.Pages.Employeer
{
    public partial class AddEditCompany
    {
        [Parameter]
        public string? UserId { get; set; }
        private bool ShowAuthError { get; set; }
        private string? Error { get; set; }

        #region PROFILE IMAGE
        private FileData imageData { get; set; } = new FileData();

        bool isDisabled = false;
        string allowedImage = "image/png";
        string imgUrl = string.Empty;
        string profileImgUrl = string.Empty;
        [Inject]
        public IFileService fileService { get; set; }
        #endregion PROFILE IMAGE
        private CompanyDto companyDto { get; set; } = new CompanyDto();
        private List<Country> countries { get; set; } = new List<Country>();
        private List<DomainDto> domains { get; set; } = new List<DomainDto>();
        [Inject]
        public IYattRepository<DomainDto> domainRepo { get; set; }
        [Inject]
        public IYattRepository<CompanyDto> companyRepo { get; set; }
        [Inject]
        public IYattRepository<Country> countryRepo { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadInitial();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && countries == null)
            {
                countries = await countryRepo.GetLists("Countries/GetList");
            }
        }
        async Task LoadInitial()
        {
            companyDto = await companyRepo.GetById("Companies", UserId);
            if (companyDto == null) companyDto = new CompanyDto { Id = UserId };

            countries = await countryRepo.GetLists("Countries/GetList");
            domains = await domainRepo.GetLists("Domains/GetList");
            await LoadImage();
        }
        async Task OnSubmit()
        {
            ResponseDto<CompanyDto> response;
            if (string.IsNullOrEmpty(companyDto.Email))
            {
                response = await companyRepo.Create("Companies", companyDto);
            }
            else
            {
                response = await companyRepo.Update($"Companies/{UserId}", companyDto);
            }
            if (response.Status != ResponseStatus.Success)
            {
                navigationManager.NavigateTo("/employeer/profile");
            }
            else
            {
                ShowAuthError = true;
                Error = response.Message;
            }
        }
        void HideError()
        {
            ShowAuthError = false;
        }
        void Cancel()
        {
            navigationManager.NavigateBack();
        }
        #region  PROFILE IMAGE
        async Task LoadImage()
        {
            imageData = await fileService.GetProfileImage(UserId!);
            if (imageData != null)
            {
                profileImgUrl = FileConstants.GeFileContent(imageData);
            }
            imgUrl = String.Empty;
        }
        async Task OnChange(InputFileChangeEventArgs e)
        {
            if (e.File.Size > FileConstants.MAX_IMAGE_SIZE)
            {
                Console.WriteLine($"Error occured. file size not allowed above {string.Format("{0:0}", FileConstants.MAX_IMAGE_SIZE / 1000)}kb");
                return;
            }
            var file = e.File; // get the files selected by the users

            var resizedFile = await file.RequestImageFileAsync(allowedImage, 640, 480); // resize the image file
            var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data
            using (var stream = resizedFile.OpenReadStream())
            {
                await stream.ReadAsync(buf); // copy the stream to the buffer
            }
            imgUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buf)}";
            imageData.FileBase64data = Convert.ToBase64String(buf);
            imageData.DataType = file.ContentType;
            imageData.IsFirst = true;
            StateHasChanged();
        }
        async Task Upload()
        {
            isDisabled = true;
            var response = await fileService.UploadProfileImage(imageData);
            if (response)
            {
                await LoadImage();
            }
            isDisabled = false;
        }
        void DeleteSelected()
        {
            imgUrl = String.Empty;
        }
        #endregion  PROFILE IMAGE
    }
}
