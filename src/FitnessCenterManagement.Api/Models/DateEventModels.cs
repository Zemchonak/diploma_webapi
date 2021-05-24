using System;
using FitnessCenterManagement.Api.Enums;

namespace FitnessCenterManagement.Api.Models
{
    public class DateEventModel
    {
        public int Id { get; set; }

        public DateEventStatus Status { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public int WeeklyEventId { get; set; }

        public int TrainerId { get; set; }
    }
}
