using System;

namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class WeeklyFitnessEventDto
    {
        public int WeeklyEventId { get; set; }

        public int DayOfWeek { get; set; }

        public DateTimeOffset Time { get; set; }

        public int VisitorCapacity { get; set; }

        public int FitnessEventId { get; set; }

        public int ServiceId { get; set; }

        public int VenueId { get; set; }

        public int Minutes { get; set; }
    }
}
