using Trecom.Api.Identity.Application.Models.Entities;

namespace Trecom.Api.Identity.Application.Observers.User;

public interface IUserObserver : IObserver
{
    public void UserCreated(Models.Entities.User user);
}