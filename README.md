# KLibrary.Testing
[![license](https://img.shields.io/github/license/sakapon/KLibrary.Testing.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/KLibrary.Testing.svg)](https://www.nuget.org/packages/KLibrary.Testing/)
[![NuGet](https://img.shields.io/nuget/dt/KLibrary.Testing.svg)](https://www.nuget.org/packages/KLibrary.Testing/)

A library for unit testing using MSTest.

## Setup
KLibrary.Testing is published to [NuGet Gallery](https://www.nuget.org/packages/KLibrary.Testing/). Install the library to test projects by NuGet.

## Features
- Extends the Assert class
  - AreNearlyEqual, etc.
- Creates a test method to be simplified
  - Wrapping AreEqual or AreNearlyEqual
- Creates random data
- Measures execution time

## Usage
Firstly, add `using` directive for `KLibrary.Testing` namespace.
```c#
using System;
using KLibrary.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
```

### AreNearlyEqual Method
Asserts the specified values are nearly equal, with specifying the upper bound of the absolute error `10^d`.
```c#
Assert2.AreNearlyEqual(3.14, Math.PI, -2);
Assert2.AreNearlyEqual(Math.E, Math.Pow(1.0000001, 10000000), -6);
```

### Test Method Wrapping
Creates a function from the function to be tested.
```c#
// Before:
Assert.AreEqual(81, Math.Pow(3, 4));
Assert.AreEqual(64, Math.Pow(4, 3));

// After:
var test = TestHelper.CreateAreEqual<double, double, double>(Math.Pow);
test(3, 4, 81);
test(4, 3, 64);
```

## Target Frameworks
- .NET Standard 2.0
- .NET Framework 4.5

### Dependencies
- [MSTest.TestFramework](https://www.nuget.org/packages/MSTest.TestFramework/) (≥ 1.3.2)

## Release Notes
- **v4.0.6** The first release.
