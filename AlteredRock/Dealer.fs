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
    | TotalSpend(email) -> Analytics.getTotalSpend email
    | MostLoyal -> Analytics.getMostLoyal
    | None -> "Nope! Are you sure of what you're asking for?"
