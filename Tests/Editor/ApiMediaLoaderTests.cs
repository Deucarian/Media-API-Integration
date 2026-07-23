using System.Threading;
using System.Threading.Tasks;
using Deucarian.API;
using Deucarian.API.Core;
using Deucarian.API.Models;
using NUnit.Framework;
using UnityEngine;

namespace Deucarian.Media.APIIntegration.Tests
{
    public sealed class ApiMediaLoaderTests
    {
        [Test]
        public async Task TextureLoader_ClonesRequestAndOwnsTexture()
        {
            Texture2D texture = new Texture2D(1, 1);
            FakeApiClient client = new FakeApiClient
            {
                Result = ApiResult<Texture2D>.Success(
                    texture,
                    HttpMethod.GET,
                    200,
                    "media/image",
                    null)
            };
            ApiRequest request = new ApiRequest("media/image");
            request.Headers["X-Tenant"] = "simultria";

            MediaLoadResult<Texture2D> result =
                await new ApiTextureMediaLoader(client).LoadAsync(
                    new ApiMediaLoadRequest(
                        new MediaSource("image-1", MediaKind.Image),
                        request),
                    CancellationToken.None);

            Assert.That(result.Succeeded, Is.True);
            Assert.That(client.LastRequest, Is.Not.SameAs(request));
            Assert.That(
                client.LastRequest.ResponseFormat,
                Is.EqualTo(ApiResponseFormat.Texture));
            Assert.That(request.ResponseFormat, Is.EqualTo(ApiResponseFormat.Auto));
            Assert.That(
                client.LastRequest.Headers["X-Tenant"],
                Is.EqualTo("simultria"));

            result.Dispose();
            Assert.That(texture == null, Is.True);
        }

        [Test]
        public async Task TextLoader_MapsApiFailure()
        {
            FakeApiClient client = new FakeApiClient
            {
                Result = ApiResult<string>.Failure(
                    new ApiError
                    {
                        Message = "Not found",
                        HttpStatusCode = 404
                    },
                    HttpMethod.GET)
            };

            MediaLoadResult<string> result =
                await new ApiTextMediaLoader(client).LoadAsync(
                    new ApiMediaLoadRequest(
                        new MediaSource("text-1", MediaKind.Text),
                        new ApiRequest("media/text")),
                    CancellationToken.None);

            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Error, Does.Contain("Not found"));
            Assert.That(result.Error, Does.Contain("404"));
        }

        [Test]
        public async Task BytesLoader_RespectsPreCancelledToken()
        {
            FakeApiClient client = new FakeApiClient();
            using (CancellationTokenSource cancellation =
                   new CancellationTokenSource())
            {
                cancellation.Cancel();

                MediaLoadResult<byte[]> result =
                    await new ApiBytesMediaLoader(client).LoadAsync(
                        new ApiMediaLoadRequest(
                            new MediaSource("bytes-1", MediaKind.Binary),
                            new ApiRequest("media/bytes")),
                        cancellation.Token);

                Assert.That(result.Status, Is.EqualTo(MediaLoadStatus.Cancelled));
                Assert.That(client.CallCount, Is.Zero);
            }
        }

        private sealed class FakeApiClient : IApiClient
        {
            public object Result { get; set; }

            public ApiRequest LastRequest { get; private set; }

            public int CallCount { get; private set; }

            public Task<ApiResult<TResponse>> SendAsync<TResponse>(
                ApiRequest request,
                CancellationToken cancellationToken = default)
            {
                LastRequest = request;
                CallCount++;
                return Task.FromResult((ApiResult<TResponse>)Result);
            }

            public Task<ApiResult<TResponse>> SendAsync<TResponse>(
                ApiEndpoint endpoint,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> SendAsync<TResponse>(
                ApiEndpoint endpoint,
                object body,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> GetAsync<TResponse>(
                string endpoint,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> PostAsync<TResponse>(
                string endpoint,
                object body,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> PutAsync<TResponse>(
                string endpoint,
                object body,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> PatchAsync<TResponse>(
                string endpoint,
                object body,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }

            public Task<ApiResult<TResponse>> DeleteAsync<TResponse>(
                string endpoint,
                CancellationToken cancellationToken = default)
            {
                throw new System.NotSupportedException();
            }
        }
    }
}
