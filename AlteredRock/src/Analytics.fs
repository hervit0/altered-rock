module Analytics

open FSharp.Data
open System

let displayMultipleItems (items:seq<JsonValue>) : string =
    items
    |> Seq.map (fun item -> item.AsString())
    |> String.concat "\n"

let getMostSold' (httpCall:string -> seq<JsonValue>) : string =
    let mostSoldItem = Purchases.getMaxBy' httpCall "item"
    match mostSoldItem with
    | m when m = Seq.empty -> "No most sold item found"
    | _ -> displayMultipleItems mostSoldItem

let getMostSold : string = getMostSold' HttpCaller.fetchAll

let getMostLoyal' (httpCall:string -> seq<JsonValue>) : string =
    let mostLoyalUserIds = Purchases.getMaxBy' httpCall "user_id"
    mostLoyalUserIds
    |> Seq.map (fun item -> Users.getById(item.AsString()))
    |> String.concat "\n"

let getMostLoyal : string = getMostLoyal' HttpCaller.fetchAll

let getTotalSpend' (httpCall:string -> seq<JsonValue>) (email:string) : string =
    let userId =
        (Users.getByEmail' httpCall email).GetProperty("id").AsString()

    let belongToUser =
        fun (purchase:JsonValue) ->
            match purchase.GetProperty("user_id").AsString() with
            | id when id = userId -> Some(purchase)
            | _ -> None

    let totalSpend =
        Purchases.getPurchases' httpCall
        |> Seq.choose belongToUser
        |> Seq.sumBy (fun purchase -> purchase.GetProperty("spend").AsFloat())

    Math.Round (totalSpend, 2) |> sprintf "%f"

let getTotalSpend (email:string) : string = getTotalSpend' HttpCaller.fetchAll email
