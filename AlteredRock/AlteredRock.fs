module AlteredRock

open System

[<EntryPoint>]
let main (argv:string[]) =
    let commands = argv |> Array.toList |> String.concat("! ")
    printfn "Did you ask for: %s" commands
    Console.ReadLine() |> ignore
    0
