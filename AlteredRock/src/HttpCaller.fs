module HttpCaller

open FSharp.Data
open FSharp.Data.JsonExtensions

let fetchAll (baseUrl:string) : seq<JsonValue> =
    let body = baseUrl |> Http.RequestString |> JsonValue.Parse
    seq { for resource in body?data -> resource }

// Code below is for a former pagination (we've changed our tests)

// let pagination = 100

// let url (baseUrl:string) (page:int) : string =
//     sprintf "%s?page=%d&per_page=%d" baseUrl page pagination

// let fetchByPage (baseUrl:string) (page:int) : seq<JsonValue> =
//     let body = url baseUrl page |> Http.RequestString |> JsonValue.Parse
//     seq { for resource in body?data -> resource }

// let rec fetchAllItems (baseUrl:string) (page:int) (allItems:seq<JsonValue>) : seq<JsonValue> =
//     let items = fetchByPage baseUrl page

//     match Seq.length(items) with
//     | noItems when noItems = pagination -> Seq.concat [ items; fetchAllItems baseUrl (page+1) allItems ]
//     | noItems when noItems < pagination -> Seq.concat [ items; allItems ]
//     | _ -> allItems

// let fetchAll (baseUrl:string) : seq<JsonValue> =
//     fetchAllItems baseUrl 1 Seq.empty
