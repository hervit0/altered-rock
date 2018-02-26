module Dealer

open FSharp.Data.JsonExtensions
open FSharp.Data

type Command =
    | MostSold
    | TotalSpend of string
    | MostLoyal
    | None

let getCommand (commands:string list) =
    match commands with
    | ["most_sold"] -> MostSold
    | ["total_spend"; email] -> TotalSpend(email)
    | ["most_loyal"] -> MostLoyal
    | _ -> None

let getResult (commands:string list) =
    match getCommand(commands) with
    | MostSold -> Analytics.getMostSold
    | TotalSpend(email) -> "Some secrets need to stay in the shadow."
    | MostLoyal -> Analytics.getMostLoyal
    | None -> Users.getById("ZZLB-4HCN-OA3N-LGWB")
    // | None -> HttpCaller.getResponse 1 Seq.empty |> Seq.map(fun x -> x.AsString()) |> Seq.toList |> String.concat(" ")
