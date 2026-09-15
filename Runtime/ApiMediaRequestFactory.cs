using System.Collections.Generic;
using Deucarian.API.Models;

namespace Deucarian.Media.APIIntegration
{
    internal static class ApiMediaRequestFactory
    {
        public static ApiRequest CopyWithResponseFormat(
            ApiRequest source,
            ApiResponseFormat responseFormat)
        {
            var copy = new ApiRequest(
                source.Endpoint,
                source.Method,
                source.Authentication)
            {
                Body = source.Body,
                BodyFormat = source.BodyFormat,
                TimeoutSeconds = source.TimeoutSeconds,
                ResponseFormat = responseFormat,
                UseIncrementalTextureUpload = source.UseIncrementalTextureUpload,
                SuppressLogging = source.SuppressLogging,
                JsonPropertyNamingOverride = source.JsonPropertyNamingOverride,
                RequestPolicy = source.RequestPolicy,
                AssetBundleOptions = source.AssetBundleOptions,
                TransferProgress = source.TransferProgress,
                BearerTokenOverride = source.BearerTokenOverride
            };

            CopyEntries(source.Headers, copy.Headers);
            CopyEntries(
                source.QueryParameters,
                copy.QueryParameters);
            return copy;
        }

        private static void CopyEntries(
            IReadOnlyDictionary<string, string> source,
            IDictionary<string, string> destination)
        {
            if (source == null)
            {
                return;
            }

            foreach (KeyValuePair<string, string> pair in source)
            {
                destination[pair.Key] = pair.Value;
            }
        }
    }
}

