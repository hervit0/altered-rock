module Analytics

open FSharp.Data
open FSharp.Data.JsonExtensions

let getMostSold : string =
    let (mostSoldItem, _) =
        Purchases.getPurchases
        |> Seq.countBy (fun purchase -> purchase.GetProperty("item"))
        |> Seq.maxBy (fun (_, count) -> count)
    mostSoldItem.AsString()

let getMostLoyal : string =
    let (mostLoyalUserId, _) =
        Purchases.getPurchases
        |> Seq.countBy (fun purchase -> purchase.GetProperty("user_id"))
        |> Seq.maxBy (fun (_, count) -> count)
    Users.getById(mostLoyalUserId.AsString())

let getTotalSpend (email:string) : string =
    let userId = Users.getByEmail(email).GetProperty("id")
    let belongToUser =
        fun (purchase:JsonValue) ->
            match purchase.GetProperty("user_id").AsString() with
            | userId -> Some(purchase)
            | _ -> None

    Purchases.getPurchases
    |> Seq.collect belongToUser

    ""
