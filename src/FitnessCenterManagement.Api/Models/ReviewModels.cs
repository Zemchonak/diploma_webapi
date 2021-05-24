namespace FitnessCenterManagement.Api.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public bool IsAnonymous { get; set; }

        public bool IsHidden { get; set; }
    }
}
