namespace FitnessCenterManagement.Api.Models
{
    public class FitnessEventModel
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int VenueId { get; set; }

        public int Minutes { get; set; }
    }
}
