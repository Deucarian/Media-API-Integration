using System;
using System.Threading;
using System.Threading.Tasks;
using Deucarian.API.Core;
using Deucarian.API.Models;

namespace Deucarian.Media.APIIntegration
{
    internal sealed class ApiMediaLoader<TResource> :
        IMediaLoader<ApiMediaLoadRequest, TResource>
    {
        private readonly IApiClient _apiClient;
        private readonly ApiResponseFormat _responseFormat;
        private readonly Func<
            TResource,
            IMediaResourceLease<TResource>> _leaseFactory;
        private readonly string _missingResourceError;

        public ApiMediaLoader(
            IApiClient apiClient,
            ApiResponseFormat responseFormat,
            Func<TResource, IMediaResourceLease<TResource>>
                leaseFactory,
            string missingResourceError)
        {
            _apiClient = apiClient ??
                         throw new ArgumentNullException(nameof(apiClient));
            _responseFormat = responseFormat;
            _leaseFactory = leaseFactory ??
                            throw new ArgumentNullException(
                                nameof(leaseFactory));
            _missingResourceError =
                string.IsNullOrWhiteSpace(missingResourceError)
                    ? "Media response did not contain a resource."
                    : missingResourceError;
        }

        public async Task<MediaLoadResult<TResource>> LoadAsync(
            ApiMediaLoadRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return MediaLoadResult<TResource>.Failure(
                    "API media load request is required.");
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return MediaLoadResult<TResource>.Cancelled();
            }

            ApiRequest apiRequest =
                ApiMediaRequestFactory.CopyWithResponseFormat(
                    request.ApiRequest,
                    _responseFormat);
            try
            {
                ApiResult<TResource> result =
                    await _apiClient.SendAsync<TResource>(
                        apiRequest,
                        cancellationToken);
                if (cancellationToken.IsCancellationRequested)
                {
                    return MediaLoadResult<TResource>.Cancelled();
                }

                if (result == null || !result.IsSuccess)
                {
                    return MediaLoadResult<TResource>.Failure(
                        BuildError(result));
                }

                if (ReferenceEquals(result.Data, null))
                {
                    return MediaLoadResult<TResource>.Failure(
                        _missingResourceError);
                }

                return MediaLoadResult<TResource>.Success(
                    _leaseFactory(result.Data));
            }
            catch (OperationCanceledException)
            {
                return MediaLoadResult<TResource>.Cancelled();
            }
            catch (Exception exception)
            {
                return MediaLoadResult<TResource>.Failure(
                    exception.Message);
            }
        }

        private static string BuildError(ApiResult<TResource> result)
        {
            string message = result?.Error?.Message;
            if (string.IsNullOrWhiteSpace(message))
            {
                return "API media request failed.";
            }

            if (result.Error.HttpStatusCode.HasValue)
            {
                message +=
                    " HTTP " +
                    result.Error.HttpStatusCode.Value +
                    ".";
            }

            return message;
        }
    }
}

