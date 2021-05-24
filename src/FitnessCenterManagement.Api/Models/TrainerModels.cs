namespace FitnessCenterManagement.Api.Models
{
    public class TrainerModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int SpecializationId { get; set; }

        public string Description { get; set; }
    }
}
