using System.ComponentModel.DataAnnotations;
using FitnessCenterManagement.Api.Resources;

namespace FitnessCenterManagement.Api.Models
{
    public class SpecializationModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Display(ResourceType = typeof(AppRes), Name = "InfoLabel")]
        public string Info { get; set; }
    }
}
