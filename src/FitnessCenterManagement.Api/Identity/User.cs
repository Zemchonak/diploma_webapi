using Microsoft.AspNetCore.Identity;

namespace FitnessCenterManagement.Api.Identity
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; }

        public int LanguageId { get; set; }

        public string AvatarName { get; set; }

        public override string NormalizedEmail => Email.ToUpperInvariant();

        public override string NormalizedUserName => UserName.ToUpperInvariant();
    }
}
