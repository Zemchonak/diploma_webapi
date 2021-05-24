namespace FitnessCenterManagement.Api.Models
{
    public class CardEventItemModel
    {
        public int Id { get; set; }

        public int AbonementCardId { get; set; }

        public int DateEventId { get; set; }

        public bool IsUsed { get; set; }
    }
}
