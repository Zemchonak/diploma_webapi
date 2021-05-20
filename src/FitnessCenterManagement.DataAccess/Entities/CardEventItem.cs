using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("CardEventItem")]
    public class CardEventItem : IBasicEntity
    {
        public int Id { get; set; }

        public int AbonementCardId { get; set; }

        public int DateEventId { get; set; }
    }
}
