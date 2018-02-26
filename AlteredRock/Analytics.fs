module Analytics

open FSharp.Data
open FSharp.Data.JsonExtensions
open System

let getMostSold' (httpCaller:string -> seq<JsonValue>) : string =
    let (mostSoldItem, _) =
        Purchases.getPurchases' httpCaller
        |> Seq.countBy (fun purchase -> purchase.GetProperty("item"))
        |> Seq.maxBy (fun (_, count) -> count)
    mostSoldItem.AsString()

let getMostSold : string =
    getMostSold' HttpCaller.fetchAll

let getMostLoyal : string =
    let (mostLoyalUserId, _) =
        Purchases.getPurchases
        |> Seq.countBy (fun purchase -> purchase.GetProperty("user_id"))
        |> Seq.maxBy (fun (_, count) -> count)
    Users.getById(mostLoyalUserId.AsString())

let getTotalSpend (email:string) : string =
    let userId = Users.getByEmail(email).GetProperty("id").AsString()
    let belongToUser =
        fun (purchase:JsonValue) ->
            match purchase.GetProperty("user_id").AsString() with
            | id when id = userId -> Some(purchase)
            | _ -> None

    let totalSpend =
        Purchases.getPurchases
        |> Seq.choose belongToUser
        |> Seq.sumBy (fun purchase -> purchase.GetProperty("spend").AsFloat())

    Math.Round (totalSpend, 2) |> sprintf "%f"
