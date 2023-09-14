namespace Trecom.Api.Identity.Application.Observers
{
    public abstract class ObserverBuilder<T> where T : IObserver
    {

        public abstract void RegisterObserver(T observer);

        public abstract void RemoveObserver(T observer);

        public abstract void NotifyObservers(object? item);
    }
}
