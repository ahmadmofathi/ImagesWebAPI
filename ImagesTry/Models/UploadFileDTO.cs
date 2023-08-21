using System.Globalization;

namespace ImagesTry
{
    public class UploadFileDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }

        public UploadFileDTO(bool isSuccess,string message,string url = "")
        {
            IsSuccess = isSuccess;
            Message = message;
            URL = url;
        }
    }
}
