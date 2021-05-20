using System;

namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Text { get; set; }
    }
}
