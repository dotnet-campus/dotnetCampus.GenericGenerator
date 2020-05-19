# Generic Generator

![.NET Build & Test](https://github.com/dotnet-campus/dotnetCampus.GenericGenerator/workflows/.NET%20Build%20&%20Test/badge.svg) ![NuGet Push](https://github.com/dotnet-campus/dotnetCampus.GenericGenerator/workflows/NuGet%20Push/badge.svg)

You may have seen this kind of code:

```csharp
public struct ValueTuple<T1> { }
public struct ValueTuple<T1, T2> { }
public struct ValueTuple<T1, T2, T3> { }
public struct ValueTuple<T1, T2, T3, T4> { }
public struct ValueTuple<T1, T2, T3, T4, T5> { }
public struct ValueTuple<T1, T2, T3, T4, T5, T6> { }
public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7> { }
public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, T8> { }
```

Emmmm...Be boring writing so much similar code? Then dotnetCampus.GenericGenerator is your choice.

## Getting started

Just add an attribute on your class and all the else is done by our package.

```csharp
using dotnetCampus.Runtime.CompilerServices;

[GenerateGenericFromThis(From = 2, To = 8)]
public struct ValueTuple<T> { }
```

Then you'll get so much generic types as something like follows:

```csharp
[System.CodeDom.Compiler.GeneratedCode("dotnetCampus.GenericGenerator", "0.1.0")]
public struct ValueTuple<T1, T2, T3, T4> { }
```
