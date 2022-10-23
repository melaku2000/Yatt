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
        private FileData? profileImageData { get; set; }
        private FileData? logoImageData { get; set; }

        bool isDisabled = false;
        string allowedImage = "image/png";
        
        string profileStringUrl = string.Empty;
        string profileStringData = string.Empty;
       
        string logoStringUrl = string.Empty;
        string logoStringData = string.Empty;
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
            var imageData = await fileService.GetProfileImage($"files/getprofile/{UserId}");
            var logoData = await fileService.GetProfileImage($"files/getlogo/{UserId}");
            if (imageData != null)
            {
                profileStringData = FileConstants.GetImageContent(imageData);
            }
            if (logoData != null)
            {
                logoStringData = FileConstants.GetImageContent(logoData);
            }
            profileStringUrl = String.Empty;
            logoStringUrl = String.Empty;
        }
        async Task OnProfileChange(InputFileChangeEventArgs e)
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
            profileStringUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buf)}";
            profileImageData = new FileData
            {
                FileBase64data = Convert.ToBase64String(buf),
                DataType = file.ContentType,
                IsFirst = true
            };
            StateHasChanged();
        }
        async Task OnLogoChange(InputFileChangeEventArgs e)
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
            logoStringUrl = $"data:{file.ContentType};base64,{Convert.ToBase64String(buf)}";
            logoImageData = new FileData
            {
                FileBase64data = Convert.ToBase64String(buf),
                DataType = file.ContentType,
                IsFirst = true
            };
            StateHasChanged();
        }
        async Task UploadProfile()
        {
            if(profileImageData != null)
            {
                profileImageData.UserId = UserId!;
                isDisabled = true;
                var response = await fileService.UploadProfileImage("files/uploadprofile", profileImageData);
                if (response)
                {
                    await LoadImage();
                }
                isDisabled = false;
            }
            profileImageData = null;
        }
        async Task UploadLogo()
        {
            if(logoImageData != null)
            {
                logoImageData.UserId = UserId!;

                isDisabled = true;
                var response = await fileService.UploadProfileImage("files/uploadlogo", logoImageData);
                if (response)
                {
                    await LoadImage();
                }
                isDisabled = false;
            }
            logoImageData = null;
        }
        void DeleteSelected()
        {
            profileStringUrl = String.Empty;
            profileImageData = null;
            logoStringUrl = String.Empty;
            logoImageData = null;
        }
        #endregion  PROFILE IMAGE
    }
}
