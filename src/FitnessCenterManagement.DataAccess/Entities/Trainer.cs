using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Trainer")]
    public class Trainer : IBasicEntity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int SpecializationId { get; set; }

        public string Description { get; set; }
    }
}
