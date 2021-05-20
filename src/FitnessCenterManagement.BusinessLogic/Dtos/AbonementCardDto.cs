using System;
using FitnessCenterManagement.BusinessLogic.Enums;

namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class AbonementCardDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int AbonementId { get; set; }

        public int Visits { get; set; }

        public DateTimeOffset PurchaseDate { get; set; }

        public AbonementCardStatus Status { get; set; }
    }
}
