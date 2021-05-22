using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

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
