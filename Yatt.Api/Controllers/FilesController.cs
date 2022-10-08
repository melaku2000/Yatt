using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yatt.Models.Constants;
using Yatt.Models.Dtos;

namespace Yatt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("getprofile/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(), FileConstants.GetUserFileDirectory(id));

            FileData fileData = new FileData { UserId = id };

            try
            {
                // get the local filename
                var filePath = Path.Combine(userDirPath, FileConstants.PROFILE_IMAGE_NAME);

                // if file is not exist load default image
                if (System.IO.File.Exists(filePath))
                {
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    fileData.FileBase64data = Convert.ToBase64String(memory.ToArray());
                    fileData.DataType = Path.GetExtension(filePath);
                    fileData.Offset = 0;
                    fileData.IsFirst = false;
                }
                else
                {
                    filePath = Path.Combine(Directory.GetCurrentDirectory(), FileConstants.DEFAULT_PROFILE_IMAGE);
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    fileData.FileBase64data = Convert.ToBase64String(memory.ToArray());
                    fileData.DataType = Path.GetExtension(filePath);
                    fileData.Offset = 0;
                    fileData.IsFirst = false;
                }
                // open for writing

                return Ok(fileData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("profile")]
        public async Task<IActionResult> UploadProfile(FileData fileData)
        {
            if (fileData == null || fileData.FileBase64data!.Length == 0)
            {
                return BadRequest();
            }

            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(),
                FileConstants.GetUserFileDirectory(fileData.UserId));

            DirectoryInfo di = new DirectoryInfo(userDirPath);
            if (!di.Exists)
                di.Create();

            try
            {
                // get the local filename
                var filePath = Path.Combine(userDirPath, FileConstants.PROFILE_IMAGE_NAME);

                // delete the file exists
                if (fileData.IsFirst && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                var buf = Convert.FromBase64String(fileData.FileBase64data);
                await System.IO.File.WriteAllBytesAsync(filePath, buf);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpPost("UploadResume")]
        public async Task<IActionResult> UploadResume(FileData fileData)
        {
            if (fileData == null || fileData.FileBase64data!.Length == 0)
            {
                return BadRequest();
            }

            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(),
                FileConstants.GetUserFileDirectory(fileData.UserId));

            DirectoryInfo di = new DirectoryInfo(userDirPath);
            if (!di.Exists)
                di.Create();

            try
            {
                // get the local filename
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    $"{FileConstants.GetFileNamePath(fileData.UserId)}");

                // delete the file exists
                if (fileData.IsFirst && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                var buf = Convert.FromBase64String(fileData.FileBase64data);
                await System.IO.File.WriteAllBytesAsync(filePath, buf);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [HttpGet("GetResume/{id:long}")]
        public async Task<IActionResult> GetResume(string id)
        {
            // get the local filename
            var userDirPath = Path.Combine(Directory.GetCurrentDirectory(),
                FileConstants.GetFileNamePath(id));

            try
            {
                FileData fileData = new FileData();
                // if file is not exist load default image
                if (System.IO.File.Exists(userDirPath))
                {

                    var memory = new MemoryStream();
                    using (var stream = new FileStream(userDirPath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    fileData.UserId = id;
                    fileData.FileBase64data = Convert.ToBase64String(memory.ToArray());
                    fileData.DataType = Path.GetExtension(userDirPath);
                    fileData.Offset = 0;
                    fileData.IsFirst = false;
                }
                // open for writing

                return Ok(fileData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
