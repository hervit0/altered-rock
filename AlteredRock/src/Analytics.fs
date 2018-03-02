module Analytics

open FSharp.Data
open System

let displayMultipleItems (items:seq<JsonValue>) : string =
    items
    |> Seq.map (fun item -> item.AsString())
    |> String.concat "\n"

let getMostSold' (httpCall:string -> seq<JsonValue>) : string =
    let mostSoldItems = Purchases.getMaxBy' httpCall "item"
    match mostSoldItems with
    | items when items = Seq.empty -> "No most sold item found"
    | _ -> displayMultipleItems mostSoldItems

let getMostSold : string = getMostSold' HttpCaller.fetchAll

let getMostLoyal' (httpCall:string -> seq<JsonValue>) : string =
    let mostLoyalUserIds = Purchases.getMaxBy' httpCall "user_id"
    match mostLoyalUserIds with
    | ids when ids = Seq.empty -> "No loyal user found"
    | _ -> mostLoyalUserIds
        |> Seq.map (fun item -> item.AsString())
        |> Seq.map (fun id -> Users.getById' httpCall id)
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
