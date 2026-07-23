using System;
using Deucarian.API.Models;

namespace Deucarian.Media.APIIntegration
{
    public sealed class ApiMediaLoadRequest
    {
        public ApiMediaLoadRequest(
            MediaSource source,
            ApiRequest apiRequest)
        {
            Source = source ??
                     throw new ArgumentNullException(nameof(source));
            ApiRequest = apiRequest ??
                         throw new ArgumentNullException(nameof(apiRequest));
        }

        public MediaSource Source { get; }
        public ApiRequest ApiRequest { get; }
    }
}

