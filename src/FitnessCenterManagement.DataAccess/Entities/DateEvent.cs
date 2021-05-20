using System;
using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Enums;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("DateEvent")]
    public class DateEvent : IBasicEntity
    {
        public int Id { get; set; }

        public DateEventStatus Status { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public int WeeklyEventId { get; set; }

        public int TrainerId { get; set; }
    }
}
