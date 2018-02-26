module Dealer

type Command =
    | MostSold
    | TotalSpend of string
    | MostLoyal
    | None

let getCommand (commands:string list) : Command =
    match commands with
    | ["most_sold"] -> MostSold
    | ["total_spend"; email] -> TotalSpend(email)
    | ["most_loyal"] -> MostLoyal
    | _ -> None

let getResult (commands:string list) : string =
    match getCommand(commands) with
    | MostSold -> Analytics.getMostSold
    | TotalSpend(email) -> Analytics.getTotalSpend email
    | MostLoyal -> Analytics.getMostLoyal
    | None -> "Nope! Are you sure of what you're asking for?"
