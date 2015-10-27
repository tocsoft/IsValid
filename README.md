[![Build status](https://ci.appveyor.com/api/projects/status/45tp8madidw0ml9x/branch/master?svg=true)](https://ci.appveyor.com/project/tocsoft/isvalid/branch/master)

IsValid is a library for validating values against different specifications, which includes things like mobile phone numbers from various locales, credit card numbers, IP addresses etc.

##Getting the code

The library it can be installed using NuGet.

```PShell
Install-Package IsValid
```


Stable/RC/Beta release are available from http://nuget.org

**Continuos builds are availible from:**

V3 feed(VS 2015) : https://www.myget.org/F/tocsoft/api/v3/index.json

V2 feed (VS 2012+) : https://www.myget.org/F/tocsoft/api/v2


##Usage

See the [wiki](https://github.com/tocsoft/IsValid/wiki) for details about what things IsValid supports and how to use it.


##Other libraries

Other libraries that have influenced this code but are not a direct dependencies are

* [AlexArchive/Validator](https://github.com/AlexArchive/Validator) 
* [chriso/validator.js](https://github.com/chriso/validator.js) (not directly by via AlexArchive/Validator)

Some/most of the initial validation code is taken directly from [AlexArchive/Validator](https://github.com/AlexArchive/Validator) but repackaged into a more fluent/extension based validation api.

