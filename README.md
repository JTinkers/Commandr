# Commandr
Simple package that allows you to bind startup parameters directly to methods.

## Nuget Link
https://www.nuget.org/packages/Commandr_JonekCode/

## Table of Contents

-   [Installation](#installation)
-   [Usage](#usage)

## Installation
- run `Install-Package Commandr_JonekCode` via Package Manager.

All functionality is now accessible via 'Commandr' namespace.

## Usage

- Create a class that'll be your `Command Controller`, then add `[CmdController]` on top of its' declaration as seen here:
```csharp
[CmdController]
class StartupManager
{

}
```

- Add a method to your class for each parameter you want your controller to process:

**Make sure your methods are static and your arguments start with - or --.**

```csharp
[CmdHook("-one")]
public static void PrintOne(string a, string b)
{
    Console.WriteLine($"{a} - {b}");
}

[CmdHook("-two")]
public static void PrintTwo()
{
    Console.WriteLine("No argument there :)");
}
```

- Add this line anywhere in your Main() to let the controller know when to process the arguments:
```
CmdController.Process(args);
```

- The result of running this example app with `-one "yes" "no" -two` as the startup params is:

```
yes - no
No argument there :)
```
