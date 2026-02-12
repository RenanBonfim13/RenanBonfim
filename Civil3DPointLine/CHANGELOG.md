# Changelog

All notable changes to the Civil3D Point to Line Plugin will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-02-12

### Added
- Initial release of Civil3D Point to Line plugin
- `POINTLINE` command to create continuous polyline from selected CogoPoints
- `POINTLINE_SEGMENTS` command to create individual line segments between consecutive points
- Support for multiple point selection with CogoPoint filtering
- Detailed output showing:
  - Point coordinates (Easting, Northing, Elevation)
  - Point numbers
  - Total length of created lines
  - Number of segments
- Color coding:
  - Red (ColorIndex 1) for POINTLINE polylines
  - Green (ColorIndex 3) for POINTLINE_SEGMENTS lines
- Comprehensive documentation:
  - README.md with full feature description
  - GUIA_RAPIDO.md (Quick Start Guide in Portuguese)
  - INSTALLATION.md (Detailed installation instructions)
  - EXAMPLES.md (Practical usage examples)
- Build automation:
  - PowerShell build script (build.ps1)
  - Batch file wrapper (build.bat)
- Deployment package:
  - PackageContents.xml for bundle deployment
  - .gitignore for build artifacts
- Visual Studio solution file (.sln)
- MIT License
- Changelog

### Features
- Works with AutoCAD Civil 3D 2026
- .NET Framework 4.8 compatible
- Easy installation via NETLOAD or bundle deployment
- User-friendly command-line interface with Portuguese messages
- Error handling and validation
- Support for any number of points (minimum 2)
- Preserves elevation from first point in polyline
- Transaction-based database operations for reliability

### Documentation
- Complete API documentation in code comments
- Multi-language support (Portuguese for end-users, English for developers)
- Step-by-step installation guide
- Troubleshooting section
- Real-world usage examples
- FAQ section

### Development
- Clean, maintainable code structure
- Follows AutoCAD .NET API best practices
- Type-safe CogoPoint filtering
- Proper resource disposal and transaction management

## [Unreleased]

### Planned for Future Versions
- Support for closed polylines (polygon creation)
- Option to select curve fitting for polylines
- Custom color selection
- Layer assignment options
- Export to CSV with segment information
- Batch processing multiple sets of points
- 3D polyline support with elevation variation
- Integration with Civil 3D alignments
- Point filtering by description/elevation
- Undo/Redo support with custom messages

---

## Version History

- **1.0.0** (2026-02-12) - Initial Release

---

## How to Update

To update from an older version:

1. Unload the old version from Civil 3D (close application)
2. Replace the old DLL with the new version
3. If using bundle deployment, replace the entire bundle folder
4. Restart Civil 3D or use NETLOAD to load the new version
5. Check this changelog for breaking changes and new features

---

**Note**: This project follows semantic versioning:
- **Major** version: Breaking changes
- **Minor** version: New features, backwards compatible
- **Patch** version: Bug fixes, backwards compatible

For support or to report issues:
- Email: renanbonfim13@gmail.com
- GitHub: https://github.com/RenanBonfim13/RenanBonfim

---

Developed by Renan Bonfim © 2026
