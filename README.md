# Altered Rock

WIP

## Prerequisites

 - .NET SDK, especially [.NET Core 2.1.4](https://www.microsoft.com/net/learn/get-started/macos)

OR just Docker :smirk: ...

## Install dependencies

```
cd AlteredRock
paket install
dotnet restore
dotnet build
```

Add one: `dotnet add package Fuchu --version 1.0.3`

## Run unit tests
```
cd AlteredRockTest
paket install
dotnet restore
dotnet build
dotnet run
```

## Try it

```
17:32:40 Hervé:~/AlteredRock 2.4.2
|> dotnet run wow so much fun
Did you ask for: wow! so! much! fun
```

:v:
