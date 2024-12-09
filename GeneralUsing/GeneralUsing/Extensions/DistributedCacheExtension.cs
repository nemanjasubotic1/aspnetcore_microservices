using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GeneralUsing.Extensions;
public static class DistributedCacheExtension
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string key, T data, 
        TimeSpan? absoluteExpireTime = null, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions();

        options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(120);

        var jsonData = JsonConvert.SerializeObject(data);

        await cache.SetStringAsync(key, jsonData, cancellationToken);
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
