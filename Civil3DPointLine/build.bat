@echo off
REM Build script for Civil3DPointLine Plugin
REM Simple batch file wrapper

echo === Civil3D Point to Line - Build Script ===
echo.

REM Check if PowerShell is available
where powershell >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: PowerShell not found!
    echo Please run the build from Visual Studio or install PowerShell.
    pause
    exit /b 1
)

REM Run PowerShell build script
powershell -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*

pause
