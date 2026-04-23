using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Common.Behaviors;

public interface ICacheable
{
    string CacheKey { get; }
    TimeSpan? CacheDuration { get; }
}

public class CachingBehavior<TRequest, TResponse>(IMemoryCache cache)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ICacheable cacheable)
            return await next(cancellationToken);

        if (cache.TryGetValue(cacheable.CacheKey, out TResponse? cached) && cached is not null)
            return cached;

        var response = await next(cancellationToken);

        cache.Set(cacheable.CacheKey, response, cacheable.CacheDuration ?? TimeSpan.FromMinutes(10));

        return response;
    }
}