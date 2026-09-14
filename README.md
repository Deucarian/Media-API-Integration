# Deucarian Media API Integration

Adapters connecting Deucarian API transport to Deucarian Media loaders.

Current package version: `0.1.1`

The package provides typed texture, text, and byte loaders. Applications remain
responsible for constructing `ApiRequest` values, including authentication,
headers, endpoint policy, and redaction policy.

Set `ApiRequest.UseIncrementalTextureUpload` for display-only WebGL textures.
The adapter preserves this option, request policy, and suppressed logging while
cloning the request. The option defaults off for callers that need CPU pixel access.

Stable:

```json
"com.deucarian.media.api-integration": "https://github.com/Deucarian/Media-API-Integration.git#main"
```

Development:

```json
"com.deucarian.media.api-integration": "https://github.com/Deucarian/Media-API-Integration.git#develop"
```

The canonical architecture standard is maintained by the
[Deucarian Package Registry](https://github.com/Deucarian/Package-Registry/blob/main/ARCHITECTURE.md).

