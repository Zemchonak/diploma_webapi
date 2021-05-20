using FitnessCenterManagement.DataAccess.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Customer")]
    public class Customer : IBasicEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? CustomerCategoryId { get; set; }
    }
}
