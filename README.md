# Photosphere.ServiceLocatorGeneration
Simple design-time ServiceLocator generator. May be useful for simple applications with minimum dependencies.

## Status
[![Windows build Status](https://ci.appveyor.com/api/projects/status/github/sunloving/photosphere-servlocgen?retina=true&svg=true)](https://ci.appveyor.com/project/sunloving/photosphere-servlocgen)
[![NuGet](https://img.shields.io/nuget/v/Photosphere.ServiceLocatorGeneration.svg)](https://www.nuget.org/packages/Photosphere.ServiceLocatorGeneration/)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/sunloving/photosphere-servlocgen/blob/master/LICENSE)

## Install via NuGet
```
PM> Install-Package Photosphere.ServiceLocatorGeneration
```

## How to use in T4 template?
```
<#@ template debug="false" hostspecific="true" language="C#" #>
<#@ output extension=".generated.cs" #>
<#@ assembly name="System.Core" #>
<#@ assembly name="System.Collections" #>
<#@ assembly name="$(ProjectDir)$(OutDir)Photosphere.ServiceLocatorGeneration.dll" #>
<#@ import namespace="System.Collections.Generic" #>
<#@ import namespace="Photosphere.ServiceLocatorGeneration" #>
<#=
new ServiceLocatorGenerator(new ServiceLocatorConfiguration
{
	HostProvidedPath = Host.ResolvePath(string.Empty),
	Parameters = new Dictionary<string, string> { { "IConfiguration", "configuration" } },
	ServicesTypesNames = new [] { "IFoo", "IBar" }
}).Generate()
#>
```
In a result we'll be have something like this
``` C#
using System;
using System.Collections.Generic;

namespace TestAssemblies
{
	internal class ServiceLocator
	{
		private readonly IDictionary<Type, object> _map = new Dictionary<Type, object>();

		public ServiceLocator(IConfiguration configuration)
		{
			var qiz = new Qiz();
			var bar = new Bar(qiz);
			_map.Add(typeof(IBar), bar);
			var foo = new Foo(bar, buz);
			_map.Add(typeof(foo), foo);
		}

		public T Get<T>() where T : class => (T) _map[typeof(T)];
	}
}
```

## Example of using
``` C#
var serviceLocatpr = new ServiceLocator(new Configuration());

var bar = serviceLocator.Get<IBar>();
var foo = serviceLocator.Get<IFoo>();
```

## Warning!
Friend, remember: service locator pattern is evil! It's only for projects that have to be a fast start. Service locator instance must be used only near entry point or root of application.

## Another example
You can see [my DI framework code](https://github.com/sunloving/photosphere-di/tree/master/src/Photosphere.DependencyInjection): [`ServiceLocator.cs`](https://github.com/sunloving/photosphere-di/blob/master/src/Photosphere.DependencyInjection/ServiceLocator.cs).
