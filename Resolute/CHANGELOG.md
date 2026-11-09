# Changelog

All notable changes to this project will be documented in this file.

## [1.0.2] - 2026-11-09
- Removed obsolete `ResoluteResultExtensions` compatibility wrapper.
- Kept `ResultHttpExtensions` as the HTTP extension entry point.
- Maintained HTTP result mapping coverage with focused tests.

## [1.0.1] - 2026-11-09
- Added HTTP result mapping tests for Minimal API integration.
- Introduced `ResultHttpExtensions` naming with non-breaking compatibility via `ResoluteResultExtensions` wrappers.
- Fixed `HttpFault` HTTP status mapping to use the explicit `StatusCode`.
- Improved packaging reliability by removing `GeneratePackageOnBuild` from Release configuration.

## [1.0.0] - Initial release
- Initial packaging of Result and Fault types for error modelling and functional-style flows.


