# ETSMobile-WindowsPhone
### English verson follows.

Bienvenue au projet Windows Phone de Ets Mobile.

## Pré-requis du projet 
* Visual Studio 2013 avec les **dernières mises-à-jours** ([Update 4](http://www.microsoft.com/en-ca/download/details.aspx?id=44921))
* Windows 8 et ultérieur
* Windows Phone SDK (téléchargeable [ici](http://dev.windows.com/en-us/develop/download-phone-sdk) (Devrais être installé en même temps que Visual Studio 2013 mentionné ci-dessus))

## Packages de Visual Studio
* [Shared Project Manager](https://visualstudiogallery.msdn.microsoft.com/315c13a7-2787-4f57-bdf7-adae6ed54450)
* [VSCommands](http://vscommands.squaredinfinity.com/)
* [XAMLSpy](http://xamlspy.com/)


## Packages utilisés dans les Solutions
* [Reactive Extensions (know as Rx)](https://msdn.microsoft.com/en-ca/data/gg577609.aspx)
* [Mvvmlight](https://mvvmlight.codeplex.com/)
* [Bcl (Microsoft)](https://www.nuget.org/packages/Microsoft.Bcl/)
* [Facebook.NET](https://facebooknet.codeplex.com/)
* [Entity Framework](https://msdn.microsoft.com/en-ca/data/ef.aspx)
* [CommonServiceLocator (Microsoft)](https://commonservicelocator.codeplex.com/)
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)

### Reactive Extensions
#### Rx-Core
Classes « Core » pour implémenter une architecture basé sur les « Schedulers ».
#### Rx-Interfaces
Interfaces « Core » pour implémenter une architecture basé sur les « Schedulers ».
#### Rx-Linq
Permet l'utilisation de [LINQ](https://msdn.microsoft.com/en-CA/library/bb397926.aspx) avec Rx
#### Rx-PlatformServices
Permet de différencier les platformes et interfaces pour obtenir les « Schedulers » approprié pour celle-ci.
#### Rx-Main
Combines les implémentions, classes, Linq et Platform Services pour implémenter une architecture basé sur les « Schedulers ».

**[En savoir plus](https://msdn.microsoft.com/en-ca/data/gg577609.aspx)**


### Mvvmlight
Permet la création d'application en utilisant le patron Model-View-ViewModel.
**[En savoir plus](https://www.nuget.org/packages/Newtonsoft.Json)**

### Microsoft Bcl
Permet les classes dites non-portables de s'adapter aux nouvelles plateformes.
**[En savoir plus](https://www.nuget.org/packages/Microsoft.Bcl/)**

### Facebook.NET
Accès à l'Api de Facebook via C#.
**[En savoir plus](http://facebooksdk.net/)**

### Entity Framework
Mapping objet-relationnel (Object-relational mapping (ORM)) recommandé et conçu par Microsoft.
**[En savoir plus](https://msdn.microsoft.com/en-ca/data/ef.aspx)**

### CommonServiceLocator
Crée un espace commun et partagé pour accéder aux services d'une application.
**[En savoir plus](https://commonservicelocator.codeplex.com/)**

### Newtonsoft.Json
Permet la manipulation d'objets ou de strings Json.
**[En savoir plus](http://www.newtonsoft.com/json)**

## Glossary
| Name          | Description   | |
| :-------------: |:-------------:| :-----:|
| Shared Projects | Permet le code d'être partagé entre projets | [Link](http://blogs.msdn.com/b/somasegar/archive/2014/04/02/visual-studio-2013-update-2-rc-universal-projects-for-windows-and-windows-phone.aspx) |
| JSON | échange légers de données |   [Link](http://json.org/) |
| Mapping objet-relationnel | converti les données non-compatibles entre systèmes à des objets de type POO en classes de langages de programmation |   [Link](http://en.wikipedia.org/wiki/Object-relational_mapping) |


#// English Version ///

#Welcome to the Windows Phone project of Ets Mobile.

## Project requirements
* Visual Studio 2013 with **Latest Update** ([Update 4](http://www.microsoft.com/en-ca/download/details.aspx?id=44921))
* Windows 8 and above
* Windows Phone SDK (downloadable [here](http://dev.windows.com/en-us/develop/download-phone-sdk) (Should be installed at the same time as Visual Studio 2013 above-mentionned))

## Visual Studio Packages
* [Shared Project Manager](https://visualstudiogallery.msdn.microsoft.com/315c13a7-2787-4f57-bdf7-adae6ed54450)
* [VSCommands](http://vscommands.squaredinfinity.com/)
* [XAMLSpy](http://xamlspy.com/)


## In-Solution Packages
* [Reactive Extensions (know as Rx)](https://msdn.microsoft.com/en-ca/data/gg577609.aspx)
* [Mvvmlight](https://mvvmlight.codeplex.com/)
* [Bcl (Microsoft)](https://www.nuget.org/packages/Microsoft.Bcl/)
* [Facebook.NET](https://facebooknet.codeplex.com/)
* [Entity Framework](https://msdn.microsoft.com/en-ca/data/ef.aspx)
* [CommonServiceLocator (Microsoft)](https://commonservicelocator.codeplex.com/)
* [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json)

### Reactive Extensions
#### Rx-Core
Core classes for implementing scheduler architecture.
#### Rx-Interfaces
Core interfaces for implementing scheduler architecture.
#### Rx-Linq
Allows the use of [LINQ](https://msdn.microsoft.com/en-CA/library/bb397926.aspx) with Rx
#### Rx-PlatformServices
Differienciates platforms and interfaces to match the appropriate scheduler.
#### Rx-Main
Combines above mentionned implementions, classes, linq and platform services for implementing scheduler architecture.

**[Learn more](https://msdn.microsoft.com/en-ca/data/gg577609.aspx)**


### Mvvmlight
Allows creation of apps using the Model-View-ViewModel pattern.
**[Learn more](https://www.nuget.org/packages/Newtonsoft.Json)**

### Microsoft Bcl
Allows non-portable classes to be usable in the newest platforms.
**[Learn more](https://www.nuget.org/packages/Microsoft.Bcl/)**

### Facebook.NET
Access the Facebook Api.
**[Learn more](http://facebooksdk.net/)**

### Entity Framework
Object-relational mapping (ORM) recommended (made) by Microsoft.
**[Learn more](https://msdn.microsoft.com/en-ca/data/ef.aspx)**

### CommonServiceLocator
Shared service location for an application.
**[Learn more](https://commonservicelocator.codeplex.com/)**

### Newtonsoft.Json
Allows manipulation of Json strings.
**[Learn more](http://www.newtonsoft.com/json)**

## Glossary
| Name          | Description   | |
| :-------------: |:-------------:| :-----:|
| Shared Projects | Allows code to be shared accross projects | [Link](http://blogs.msdn.com/b/somasegar/archive/2014/04/02/visual-studio-2013-update-2-rc-universal-projects-for-windows-and-windows-phone.aspx) |
| JSON | lightweight data-interchange format |   [Link](http://json.org/) |
| Object-relational mapping | converts data between incompatible type systems in object-oriented programming languages |   [Link](http://en.wikipedia.org/wiki/Object-relational_mapping) |