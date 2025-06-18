using System.Linq;
using garbageDetetionApi.Context;
using garbageDetetionApi.Models;

namespace garbageDetetionApi.Helper;

static class ApiKeyHelper
{
    public static ApiKeyType CheckApiKeyType(string apiKeyString, GarbageDbContext context)
    {
        if (!Guid.TryParse(apiKeyString, out var apiKeyGuid))
        {
            return ApiKeyType.None;
        }
        var apiKey = context.ApiKeys.FirstOrDefault(x => x.Id == apiKeyGuid);
        if (apiKey == null)
        {
            return ApiKeyType.None;
        }
        return apiKey.Type.Equals("read", StringComparison.OrdinalIgnoreCase) ? ApiKeyType.Read :
            apiKey.Type.Equals("write", StringComparison.OrdinalIgnoreCase) ? ApiKeyType.Write :
            ApiKeyType.None;
    }
}

public enum ApiKeyType
{
    None,
    Read,
    Write,
}