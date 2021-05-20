using System;
using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("WeeklyEvent")]
    public class WeeklyEvent : IBasicEntity
    {
        public int Id { get; set; }

        public int FitnessEventId { get; set; }

        public int DayOfWeek { get; set; }

        public DateTimeOffset Time { get; set; }

        public int VisitorCapacity { get; set; }
    }
}
