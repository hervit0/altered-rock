module HttpCaller

open FSharp.Data
open FSharp.Data.JsonExtensions

let pagination = 100

let url (page:int) =
    sprintf "https://driftrock-dev-test.herokuapp.com/purchases?page=%d&per_page=%d" page pagination

let fetchByPage (page:int) =
    let response = page |> url |> Http.RequestString |> JsonValue.Parse
    seq { for purchase in response?data -> purchase?user_id }

let rec getResponse (page:int) (allPurchases:seq<JsonValue>) =
    let purchases = fetchByPage page

    match Seq.length(purchases) with
    | quantity when quantity = pagination -> Seq.concat [ purchases; getResponse (page+1) allPurchases ]
    | quantity when quantity < pagination -> Seq.concat [ purchases; allPurchases ]
    | _ -> allPurchases
