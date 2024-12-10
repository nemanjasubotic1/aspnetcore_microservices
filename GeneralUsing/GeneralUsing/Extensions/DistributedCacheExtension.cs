using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GeneralUsing.Extensions;
public static class DistributedCacheExtension
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string key, T data, 
        TimeSpan? absoluteExpireTime = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);

        var jsonData = JsonConvert.SerializeObject(data);

        await cache.SetStringAsync(key, jsonData, options, cancellationToken);
    }

    public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string key, CancellationToken cancellationToken = default)
    {
        var jsonData = await cache.GetStringAsync(key, cancellationToken);

        if (jsonData == null)
        {
            return default(T);
        }

        return JsonConvert.DeserializeObject<T>(jsonData);
    }
}
