using FitnessCenterManagement.BusinessLogic.Enums;

namespace FitnessCenterManagement.BusinessLogic.Dtos
{
    public class AbonementDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Coefficient { get; set; }

        public int Attendances { get; set; }

        public AbonementStatus Status { get; set; }

        public string ImageName { get; set; }
    }
}
