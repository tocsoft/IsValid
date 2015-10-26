@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

if "%nuget%" == "" (  
  set nuget=nuget.exe
)

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild IsValid.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

if not "%GallioEcho%" == "" (  
  REM Unit tests
  %GallioEcho% IsValid\IsValid.Tests\bin\%config%\IsValid.Tests.dll
  if not "%errorlevel%"=="0" goto failure
)

REM Package
mkdir Build
call %nuget% pack "IsValid\IsValid.csproj" -IncludeReferencedProjects -o Build -p Configuration=%config% %version%
if not "%errorlevel%"=="0" goto failure


:success
exit 0
goto end

:failure
exit -1
goto end

:end