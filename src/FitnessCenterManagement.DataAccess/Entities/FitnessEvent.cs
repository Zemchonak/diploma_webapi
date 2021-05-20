using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Event")]
    public class FitnessEvent : IBasicEntity
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public int VenueId { get; set; }

        public int Minutes { get; set; }
    }
}
