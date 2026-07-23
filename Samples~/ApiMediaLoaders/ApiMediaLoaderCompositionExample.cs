using Deucarian.API.Core;

namespace Deucarian.Media.APIIntegration.Samples
{
    public sealed class ApiMediaLoaderCompositionExample
    {
        public ApiMediaLoaderCompositionExample(IApiClient apiClient)
        {
            TextureLoader = new ApiTextureMediaLoader(apiClient);
            TextLoader = new ApiTextMediaLoader(apiClient);
            BytesLoader = new ApiBytesMediaLoader(apiClient);
        }

        public ApiTextureMediaLoader TextureLoader { get; }

        public ApiTextMediaLoader TextLoader { get; }

        public ApiBytesMediaLoader BytesLoader { get; }
    }
}
