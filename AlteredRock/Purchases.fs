module Purchases

open FSharp.Data

let baseUrl = "https://driftrock-dev-test.herokuapp.com/purchases?"

let getPurchases : seq<JsonValue> = HttpCaller.fetchAll baseUrl
