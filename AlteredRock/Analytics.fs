module Analytics

open FSharp.Data
open System

let getMostSold' (httpCall:string -> seq<JsonValue>) : string =
    let mostSoldItem = Purchases.getMaxBy' httpCall "item"
    match mostSoldItem with
    | JsonValue.Null -> "No most sold item found"
    | _ -> mostSoldItem.AsString()

let getMostSold : string = getMostSold' HttpCaller.fetchAll

let getMostLoyal' (httpCall:string -> seq<JsonValue>) : string =
    let mostLoyalUserId = Purchases.getMaxBy' httpCall "user_id"
    Users.getById(mostLoyalUserId.AsString())

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
