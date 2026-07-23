# Deucarian Media API Integration Agent Notes

Package ID: `com.deucarian.media.api-integration`
Repository: `Deucarian/Media-API-Integration`

Follow the canonical [Deucarian Architecture Rules](https://github.com/Deucarian/Package-Registry/blob/main/ARCHITECTURE.md).

## Ownership

This integration package adapts Deucarian API requests and results into typed
Deucarian Media texture, text, and byte load results.

It must not own authentication policy, endpoint construction, media playback,
application attachment models, or an independent HTTP framework.

## Dependencies

- `com.deucarian.media`: media contracts and resource leases.
- `com.deucarian.api`: API request and transport contracts.

## Validation

```powershell
python C:/Repositories/Package-Registry/Tools/deucarian_package_validator.py --registry-root C:/Repositories/Package-Registry --repository-root . --config deucarian-package.json
```

Work on `develop`. Promote to `main` only as a deliberate stable-channel
operation. Do not edit `Library/PackageCache`.

