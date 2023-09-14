namespace Trecom.Api.Identity.Application.Events
{
    public class UserCreatedEvent
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        public UserCreatedEvent(string email, string fullName)
        {
            Email = email;
            FullName = fullName;
        }

        public static UserCreatedEvent Create(string email, string fullName) => new UserCreatedEvent(email, fullName);
    }
}
