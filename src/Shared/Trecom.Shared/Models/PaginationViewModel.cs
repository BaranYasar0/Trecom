namespace Trecom.Shared.Models;

public class PaginationViewModel<TEntity> where TEntity : class
{
    public int PageSize { get; set; }
    public int Page { get; set; }

    public int Count
    {
        get;
        set;
    }
    public List<TEntity> Items { get; set; } = new();

    public PaginationViewModel()
    {
        Items = new List<TEntity>();
    }

    public PaginationViewModel(List<TEntity> items)
    {
        Items = items;
        this.GeneratePropsExceptData();
    }

    public PaginationViewModel(List<TEntity> items, int count, int pageSize, int page)
    {
        PageSize = pageSize;
        Page = page;
        Items = items;
        Count = count;
    }

    private void GeneratePropsExceptData()
    {
        this.PageSize = 10;
        this.Page = 0;
        Count = this?.Count ?? 0;
    }

    public static PaginationViewModel<TEntity> Create(List<TEntity> data, int count, int pageSize = 10, int page = 1)
    {
        return new PaginationViewModel<TEntity>(data, count, pageSize, page);
    }
}