

using Trecom.Shared.Models;

namespace Trecom.Api.Identity.Application.Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsPremium { get; set; } = false;
        public bool Status { get; set; } = true;

        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public User():base()
        {
            UserOperationClaims = new HashSet<UserOperationClaim>();
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public User(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash,bool isPremium=false,
            bool status=true) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            IsPremium = isPremium;
            Status = status;
        }
    }
}