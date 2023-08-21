using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImagesTry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        public ActionResult<UploadFileDTO> Upload(IFormFile file)
        {
            #region Checking Extension
            var extension = Path.GetExtension(file.FileName);
            var allowedExtensions = new string[]
            {
                ".png",".jpg",".svg"
            };
            bool isExtensionAllowed = allowedExtensions.Contains(extension,StringComparer.InvariantCultureIgnoreCase);
            if (!isExtensionAllowed) {
                return BadRequest(new UploadFileDTO(false, "Extension is not allowed"));    
            }
            #endregion

            #region Checking Size
            bool isSizeAllowed = file.Length is > 0 and < 4_000_000;
            if(!isSizeAllowed)
            {
                return BadRequest(new UploadFileDTO(false, "Size is not allowed"));
            }
            #endregion

            #region GUID
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory,"Images");
            var FullFilePath = Path.Combine(imagesPath,newFileName);
            #endregion

            using var Stream = new FileStream(FullFilePath, FileMode.Create);
            file.CopyTo(Stream);
            var URL = $"{Request.Scheme}://{Request.Host}/Images/{newFileName}";
            return new UploadFileDTO(true, "Success", URL);
        }
    }
}
