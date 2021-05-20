namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public bool IsAnonymous { get; set; }

        public bool IsHidden { get; set; }
    }
}
