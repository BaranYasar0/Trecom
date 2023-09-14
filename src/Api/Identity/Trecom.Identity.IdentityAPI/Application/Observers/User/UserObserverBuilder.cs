namespace Trecom.Api.Identity.Application.Observers.User
{
    public class UserObserverBuilder : ObserverBuilder<IUserObserver>
    {
        private readonly List<IUserObserver> observers;

        public UserObserverBuilder()
        {
            observers = new List<IUserObserver>();
        }

        public override void RegisterObserver(IUserObserver observer) => observers.Add(observer);

        public override void RemoveObserver(IUserObserver observer) => observers.Remove(observer);

        public override void NotifyObservers(object? item) =>
            observers.ForEach(observer => observer.UserCreatedAsync(item as Models.Entities.User));
    }
}
