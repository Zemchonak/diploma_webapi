using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FitnessCenterManagement.Api.Resources;
using FitnessCenterManagement.BusinessLogic.Dtos;

namespace FitnessCenterManagement.Api.Models
{
    public class ProfileModel
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }

        public string Language { get; set; }

        public string LanguageName { get; set; }

        public string RoleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public IReadOnlyCollection<LanguageDto> Languages { get; set; }
    }

    public class ProfileEditModel
    {
        [DataType(DataType.Text)]
        [MaxLength(256, ErrorMessageResourceName = "MaxAddressLength", ErrorMessageResourceType = typeof(AppRes))]
        [Display(ResourceType = typeof(AppRes), Name = "AddressLabel")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceName = "SurnameRequired", ErrorMessageResourceType = typeof(AppRes))]
        [DataType(DataType.Text)]
        [MaxLength(256, ErrorMessageResourceName = "MaxSurnameLength", ErrorMessageResourceType = typeof(AppRes))]
        [Display(ResourceType = typeof(AppRes), Name = "SurnameLabel")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(AppRes))]
        [DataType(DataType.Text)]
        [MaxLength(256, ErrorMessageResourceName = "MaxFirstNameLength", ErrorMessageResourceType = typeof(AppRes))]
        [Display(ResourceType = typeof(AppRes), Name = "FirstNameLabel")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(256, ErrorMessageResourceName = "MaxLastNameLength", ErrorMessageResourceType = typeof(AppRes))]
        [Display(ResourceType = typeof(AppRes), Name = "LastNameLabel")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(ResourceType = typeof(AppRes), Name = "BalanceLabel")]
        public decimal Balance { get; set; }

        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(AppRes))]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(ResourceType = typeof(AppRes), Name = "EmailLabel")]
        public string Email { get; set; }
    }

    public class ProfileChangeBalanceModel
    {
        [Required]
        [DataType(DataType.Currency)]
        [Display(ResourceType = typeof(AppRes), Name = "BalanceLabel")]
        public decimal Balance { get; set; }
    }
}
