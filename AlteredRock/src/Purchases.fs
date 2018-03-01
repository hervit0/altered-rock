module Purchases

open FSharp.Data

// TODO: Extract to environment variables
let baseUrl = "https://driftrock-dev-test.herokuapp.com/purchases"

let getPurchases' (httpCall:string -> seq<JsonValue>) : seq<JsonValue> = httpCall baseUrl

let getPurchases : seq<JsonValue> = getPurchases' HttpCaller.fetchAll

let getMaxBy' (httpCall:string -> seq<JsonValue>) (property:string) : seq<JsonValue> =
    let maxBy (purchases:seq<JsonValue>) : seq<JsonValue> =
        let getMaxs (maxs:seq<JsonValue>, maxCount:int) (purchase:JsonValue, count:int) =
            match count with
            | c when c = maxCount -> (Seq.append maxs [|purchase|], maxCount)
            | c when c > maxCount -> (seq [purchase], count)
            | _  -> (maxs, maxCount)

        let (maxPurchases, _) =
            purchases
            |> Seq.countBy (fun purchase -> purchase.GetProperty(property))
            |> Seq.fold getMaxs (Seq.empty<JsonValue>, 0)
        maxPurchases

    let purchases = getPurchases' httpCall
    match purchases with
    | p when p = Seq.empty -> Seq.empty
    | _ -> maxBy purchases
