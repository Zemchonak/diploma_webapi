using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Language")]
    public class Language : IBasicEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
