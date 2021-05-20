using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessCenterManagement.DataAccess.Entities
{
    [Table("Message")]
    public class Message
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Text { get; set; }
    }
}
