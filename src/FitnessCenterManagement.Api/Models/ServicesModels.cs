namespace FitnessCenterManagement.Api.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string SpecializationInfo { get; set; }

        public int SpecializationId { get; set; }

        public string Description { get; set; }
    }
}
