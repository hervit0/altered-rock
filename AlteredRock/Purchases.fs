module Purchases

open FSharp.Data

// TODO: Extract to environment variables
let baseUrl = "https://driftrock-dev-test.herokuapp.com/purchases?"

let getPurchases' (httpCall:string -> seq<JsonValue>) : seq<JsonValue> = httpCall baseUrl

let getPurchases : seq<JsonValue> = getPurchases' HttpCaller.fetchAll
