using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("CustomerCategory")]
    public class CustomerCategory : IBasicEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal SaleCoefficient { get; set; }
    }
}
