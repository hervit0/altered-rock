module Purchases

open FSharp.Data

// TODO: Extract to environment variables
let baseUrl = "https://driftrock-dev-test.herokuapp.com/purchases?"

let getPurchases' (httpCall:string -> seq<JsonValue>) : seq<JsonValue> = httpCall baseUrl

let getPurchases : seq<JsonValue> = getPurchases' HttpCaller.fetchAll

let getMaxBy' (httpCall:string -> seq<JsonValue>) (property:string) : JsonValue =
    let maxBy (purchases:seq<JsonValue>) : JsonValue * int =
        purchases
        |> Seq.countBy (fun purchase -> purchase.GetProperty(property))
        |> Seq.maxBy (fun (_, count) -> count)

    let (item, _) =
        let purchases = getPurchases' httpCall
        match purchases with
        | purchases when purchases = Seq.empty -> (JsonValue.Null, 0)
        | _ -> maxBy purchases
    item
