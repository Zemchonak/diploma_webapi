namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class WeeklyEventDto
    {
        public int Id { get; set; }

        public int FitnessEventId { get; set; }

        public int DayOfWeek { get; set; }

        public DateTimeOffset Time { get; set; }

        public int VisitorCapacity { get; set; }
    }
}
