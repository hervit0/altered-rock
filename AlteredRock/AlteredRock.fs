module AlteredRock

open System

[<EntryPoint>]
let main (argv:string[]) =
    let result = argv |> Array.toList |> Dealer.getResult
    printfn "%s" result
    Console.ReadLine() |> ignore
    0
