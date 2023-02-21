
using Microsoft.AspNetCore.Identity;

namespace infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName, string firstName, string lastName) : base(userName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}





