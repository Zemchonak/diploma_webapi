using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Enums;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Abonement")]
    public class Abonement : IBasicEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Coefficient { get; set; }

        public int Attendances { get; set; }

        public AbonementStatus Status { get; set; }

        public string ImageName { get; set; }
    }
}
