using System;
using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Enums;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("AbonementCard")]
    public class AbonementCard : IBasicEntity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int AbonementId { get; set; }

        public int Visits { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public AbonementCardStatus Status { get; set; }
    }
}
