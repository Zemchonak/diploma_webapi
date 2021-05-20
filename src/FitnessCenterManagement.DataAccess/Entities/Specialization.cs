using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Specialization")]
    public class Specialization : IBasicEntity
    {
        public int Id { get; set; }

        public string Info { get; set; }
    }
}
