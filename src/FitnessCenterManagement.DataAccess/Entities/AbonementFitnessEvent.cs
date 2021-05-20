using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("AbonementFitnessEvent")]
    public class AbonementFitnessEvent : IBasicEntity
    {
        public int Id { get; set; }

        public int FitnessEventId { get; set; }

        public int AbonementId { get; set; }
    }
}
