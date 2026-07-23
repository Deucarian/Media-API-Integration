using System;
using Deucarian.API.Core;
using Deucarian.API.Models;
using Deucarian.Media.Unity;
using UnityEngine;

namespace Deucarian.Media.APIIntegration
{
    public sealed class ApiTextureMediaLoader :
        IMediaLoader<ApiMediaLoadRequest, Texture2D>
    {
        private readonly ApiMediaLoader<Texture2D> _loader;

        public ApiTextureMediaLoader(IApiClient apiClient)
        {
            _loader = new ApiMediaLoader<Texture2D>(
                apiClient,
                ApiResponseFormat.Texture,
                UnityMediaResourceLease.CreateOwned,
                "Image response did not contain a texture.");
        }

        public System.Threading.Tasks.Task<
            MediaLoadResult<Texture2D>> LoadAsync(
            ApiMediaLoadRequest request,
            System.Threading.CancellationToken cancellationToken)
        {
            return _loader.LoadAsync(
                request,
                cancellationToken);
        }
    }

    public sealed class ApiTextMediaLoader :
        IMediaLoader<ApiMediaLoadRequest, string>
    {
        private readonly ApiMediaLoader<string> _loader;

        public ApiTextMediaLoader(IApiClient apiClient)
        {
            _loader = new ApiMediaLoader<string>(
                apiClient,
                ApiResponseFormat.Text,
                MediaResourceLease<string>.Borrowed,
                "Text response did not contain content.");
        }

        public System.Threading.Tasks.Task<
            MediaLoadResult<string>> LoadAsync(
            ApiMediaLoadRequest request,
            System.Threading.CancellationToken cancellationToken)
        {
            return _loader.LoadAsync(
                request,
                cancellationToken);
        }
    }

    public sealed class ApiBytesMediaLoader :
        IMediaLoader<ApiMediaLoadRequest, byte[]>
    {
        private readonly ApiMediaLoader<byte[]> _loader;

        public ApiBytesMediaLoader(IApiClient apiClient)
        {
            _loader = new ApiMediaLoader<byte[]>(
                apiClient,
                ApiResponseFormat.Bytes,
                MediaResourceLease<byte[]>.Borrowed,
                "Binary response did not contain bytes.");
        }

        public System.Threading.Tasks.Task<
            MediaLoadResult<byte[]>> LoadAsync(
            ApiMediaLoadRequest request,
            System.Threading.CancellationToken cancellationToken)
        {
            return _loader.LoadAsync(
                request,
                cancellationToken);
        }
    }
}

