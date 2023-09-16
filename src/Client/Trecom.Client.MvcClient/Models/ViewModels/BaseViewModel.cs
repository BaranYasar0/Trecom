namespace Trecom.Client.MvcClient.Models.ViewModels;

public class BaseViewModel<T> where T : class
{
    public List<string> Errors { get; set; }

    public bool IsSuccess
    {
        get
        {
            if (Errors is not null && this.Errors.Count > 0)
                return false;
            return true;
        }
        set
        {

        }
    }

    public T Data { get; set; }
    public static BaseViewModel<T> Create(T data)
    {
        return new BaseViewModel<T>
        {
            Data = data
        };
    }
}