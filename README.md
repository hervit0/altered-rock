# Altered Rock

## Overview

Little console app written in F# with love.

## Prerequisites

 - .NET SDK, especially [.NET Core 2.1.4](https://www.microsoft.com/net/learn/get-started/macos)

OR just Docker :smirk: ...

For the next paragraphs, let's define a function:
```
def installDeps (dir:Directory) : Unit = {
    cd dir
    paket install
    dotnet restore
    dotnet build
    echo "All good mate!"
}
```

## Install dependencies

Did you read the previous paragraph?
```
installDeps("AlteredRock")
```

Add one library: `dotnet add package Fuchu --version 1.0.3`

## Run unit tests

```
installDeps("AlteredRockTest")
dotnet run
```

## Try it!

```
22:59:37 Hervé:~/sandbox/altered-rock/AlteredRock 2.4.2
|> dotnet run most_loyal
Travis Kshlerin

23:32:02 Hervé:~/sandbox/altered-rock/AlteredRock 2.4.2
|> dotnet run most_sold
Heavy Duty Concrete Watch
```

:v:
