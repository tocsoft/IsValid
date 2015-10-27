@echo Off
ECHO Starting build

set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)
if not "%GitVersion_NuGetVersion%" == "" (
	if "%version%" == "" (
		set version=-Version %GitVersion_NuGetVersion%
	)
)


if "%nuget%" == "" (  
  set nuget=nuget.exe
)

REM Restoring Packages
ECHO Restoring Packages
call "%nuget%" restore "IsValid.sln"
if not "%errorlevel%"=="0" goto failure

ECHO Running MSBUILD
REM Build
"%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild" IsValid.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
ECHO Building pacakges
mkdir Build
call "%nuget%" pack "IsValid\IsValid.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%
if not "%errorlevel%"=="0" goto failure


:success
ECHO successfully built project
exit 0
goto end

:failure
ECHO failed to build.
exit -1
goto end

:end