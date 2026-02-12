# Build script for Civil3DPointLine Plugin
# Requires Visual Studio with MSBuild

param(
    [string]$Configuration = "Release",
    [switch]$Clean,
    [switch]$Install,
    [string]$InstallPath = "C:\ProgramData\Autodesk\ApplicationPlugins\"
)

Write-Host "=== Civil3D Point to Line - Build Script ===" -ForegroundColor Cyan
Write-Host ""

# Find MSBuild
$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
)

$msbuild = $null
foreach ($path in $msbuildPaths) {
    if (Test-Path $path) {
        $msbuild = $path
        break
    }
}

if (-not $msbuild) {
    Write-Host "ERROR: MSBuild not found!" -ForegroundColor Red
    Write-Host "Please install Visual Studio 2019 or newer." -ForegroundColor Yellow
    exit 1
}

Write-Host "Found MSBuild: $msbuild" -ForegroundColor Green
Write-Host ""

# Clean if requested
if ($Clean) {
    Write-Host "Cleaning project..." -ForegroundColor Yellow
    & $msbuild "Civil3DPointLine.csproj" /t:Clean /v:minimal
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: Clean failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "Clean completed." -ForegroundColor Green
    Write-Host ""
}

# Build
Write-Host "Building Civil3DPointLine ($Configuration)..." -ForegroundColor Yellow
& $msbuild "Civil3DPointLine.csproj" /p:Configuration=$Configuration /v:minimal

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Build completed successfully!" -ForegroundColor Green
$dllPath = "bin\$Configuration\Civil3DPointLine.dll"
Write-Host "DLL location: $dllPath" -ForegroundColor Cyan

# Check if DLL exists
if (-not (Test-Path $dllPath)) {
    Write-Host "WARNING: DLL not found at expected location!" -ForegroundColor Yellow
}

# Install if requested
if ($Install) {
    Write-Host ""
    Write-Host "Installing plugin..." -ForegroundColor Yellow
    
    $bundlePath = Join-Path $InstallPath "Civil3DPointLine.bundle"
    $contentsPath = Join-Path $bundlePath "Contents"
    
    # Create bundle structure
    if (-not (Test-Path $bundlePath)) {
        New-Item -ItemType Directory -Path $bundlePath -Force | Out-Null
    }
    if (-not (Test-Path $contentsPath)) {
        New-Item -ItemType Directory -Path $contentsPath -Force | Out-Null
    }
    
    # Copy DLL
    Write-Host "Copying DLL to $contentsPath..." -ForegroundColor Yellow
    Copy-Item $dllPath -Destination $contentsPath -Force
    
    # Copy PackageContents.xml
    Write-Host "Copying PackageContents.xml..." -ForegroundColor Yellow
    Copy-Item "PackageContents.xml" -Destination $bundlePath -Force
    
    Write-Host ""
    Write-Host "Installation completed!" -ForegroundColor Green
    Write-Host "Bundle installed at: $bundlePath" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Please restart Civil 3D to load the plugin." -ForegroundColor Yellow
}

Write-Host ""
Write-Host "=== Build Process Complete ===" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "  1. Open AutoCAD Civil 3D 2026" -ForegroundColor White
Write-Host "  2. Type NETLOAD and select $dllPath" -ForegroundColor White
Write-Host "  3. Type POINTLINE to use the plugin" -ForegroundColor White
Write-Host ""
Write-Host "Or run: .\build.ps1 -Install" -ForegroundColor Yellow
Write-Host "to install for automatic loading." -ForegroundColor Yellow
Write-Host ""
