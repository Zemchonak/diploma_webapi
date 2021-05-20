using System;
using FitnessCenterManagement.BusinessLogic.Enums;

namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class DateEventDto
    {
        public int Id { get; set; }

        public DateEventStatus Status { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public int WeeklyEventId { get; set; }

        public int TrainerId { get; set; }
    }
}
