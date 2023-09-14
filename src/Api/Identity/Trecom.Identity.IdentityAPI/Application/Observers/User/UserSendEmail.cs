namespace Trecom.Api.Identity.Application.Observers.User
{
    public class UserSendEmail : IUserObserver
    {
        private readonly IServiceProvider sp;

        private readonly Models.Entities.User user;

        public UserSendEmail(IServiceProvider sp)
        {
            this.sp = sp;
        }

        public void Execute()
        {
            UserCreated(user);
        }

        public void UserCreated(Models.Entities.User user)
        {
            var logger = sp.GetRequiredService<ILogger<UserSendEmail>>();

            logger.LogInformation($"{user.FirstName} için {this.GetType().FullName}'de observer pattern deneniyor");
        }
    }
}
