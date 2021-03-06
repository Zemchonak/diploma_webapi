using System.ComponentModel.DataAnnotations.Schema;
using FitnessCenterManagement.DataAccess.Interfaces;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Review")]
    public class Review : IBasicEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public bool IsAnonymous { get; set; }

        public bool IsHidden { get; set; }
    }
}
