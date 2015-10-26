# IsValid

[![tocsoft MyGet Build Status](https://www.myget.org/BuildSource/Badge/tocsoft?identifier=da851231-8a12-4239-8d22-30b4d7f9dc8f)](https://www.myget.org/)

A library influenced by [AlexArchive/Validator](https://github.com/AlexArchive/Validator) and [chriso/validator.js](https://github.com/chriso/validator.js) but provides the functions via a fluent extension based api.

Some/most of the initial validation code is taken directly from [AlexArchive/Validator](https://github.com/AlexArchive/Validator) but repackaged into a more fluent/extension based validation api.


##Usage

To activate all you have to do is add `` Using IsValid; ``
to your using statements at the top of each page then call one of the below tests


##Validators

####Credit card numbers
```c#
bool isValid = "CredicCardNumber".IsValid().CreditCard(); 
```

####Email addresses
```c#
bool isValid = "mail@example.com".IsValid().Email(); 
```

####IP address 
You can validate IP Addresses as either IPv4 and IPv6 with out caring which type 
```c#
bool isValidV4 = "127.0.0.1".IsValid().IPAddress(); 
bool isValidV6 = "2001:db8:0000:1:1:1:1:1".IsValid().IPAddress(); 
```
or by validating them as one other the other
```c#
bool isValidV4 = "127.0.0.1".IsValid().IPAddressV4(); 
bool isValidV6 = "2001:db8:0000:1:1:1:1:1".IsValid().IPAddressV4(); 
```
or its event possible to validate the addresses as other address families
```c#
bool isValidIP = "127.0.0.1".IsValid().IPAddress(AddressFamily.InterNetwork);
```

####Lowercase
```c#
bool isValid = "lower".IsValid().Lowercase(); 
```

####Uppercase
```c#
bool isValid = "UPPER".IsValid().Uppercase(); 
```


####Mobile Phone
will validate based on a supported locale.
Supported locales are:

* zh-CN
* zh-TW
* en-ZA
* en-AU
* fr-FR
* en-HK
* pt-PT
* el-GR
* en-GB
* en-US
* en-ZM
* ru-RU
* nb-NO
* nn-NO

```c#
bool isValid = "07700956823".IsValid().MobilePhone("en-GB"); 
bool isValid = "07700956823".IsValid().MobilePhone();  // defaults to CurrentUICulture 
```


####Numeric
will validate strings that look like valid integers , doesn't use ``int.Parse``
```c#
bool isValid = "956823".IsValid().Numeric(); 
```