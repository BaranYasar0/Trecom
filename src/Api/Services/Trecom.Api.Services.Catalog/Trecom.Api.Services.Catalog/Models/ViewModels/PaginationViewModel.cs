using System.Runtime.CompilerServices;

namespace Trecom.Api.Services.Catalog.Models.ViewModels
{
    public class PaginationViewModel<TEntity> where TEntity : class
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public int Count
        {
            get;
            set;
        }
        public List<TEntity> Data { get; set; } = new List<TEntity>();

        public PaginationViewModel()
        {
            Data = new List<TEntity>();
        }

        public PaginationViewModel(List<TEntity> data)
        {
            Data = data;
            this.GeneratePropsExceptData();
        }

        public PaginationViewModel(List<TEntity> data, int count, int pageSize, int page)
        {
            PageSize = pageSize;
            Page = page;
            Data = data;
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
}
