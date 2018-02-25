module Purchases

open FSharp.Data

let baseUrl = "https://driftrock-dev-test.herokuapp.com/purchases?"

let getPurchases : seq<JsonValue> =
    HttpCaller.fetchAllItems baseUrl 1 Seq.empty

let getMostSold : string =
    let (mostSoldItem, _) =
        getPurchases
        |> Seq.countBy (fun purchase -> purchase.GetProperty("item"))
        |> Seq.maxBy (fun (_, count) -> count)
    mostSoldItem.AsString()
