namespace Trecom.Shared.Pipelines.Catching;

public interface ICachableQueryRequest
{
    bool BypassCache { get; }
    string CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }
}