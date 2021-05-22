using Microsoft.AspNetCore.Http;

namespace FitnessCenterManagement.Api.Models
{
    public class ChangeLanguageModel
    {
        public string LanguageCode { get; set; }
    }

    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }

        public string ErrorAttribute { get; set; }
    }

    public class ImageUploadModel
    {
        public IFormFile File { get; set; }
    }

    public class UserModel
    {
        public string Id { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class MessageModel
    {
        public string Text { get; set; }
    }
}
