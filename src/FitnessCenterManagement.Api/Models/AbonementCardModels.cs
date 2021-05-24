using System;
using FitnessCenterManagement.Api.Enums;

namespace FitnessCenterManagement.Api.Models
{
    public class AbonementCardModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int AbonementId { get; set; }

        public int Visits { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public AbonementCardStatus Status { get; set; }
    }
}
