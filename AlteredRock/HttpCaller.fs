module HttpCaller

open FSharp.Data
open FSharp.Data.JsonExtensions

let pagination = 100

let url (baseUrl:string) (page:int) =
    sprintf "%spage=%d&per_page=%d" baseUrl page pagination

let fetchByPage (baseUrl:string) (page:int) =
    let body = url baseUrl page |> Http.RequestString |> JsonValue.Parse
    seq { for resource in body?data -> resource }

let rec fetchAllItems (baseUrl:string) (page:int) (allItems:seq<JsonValue>) =
    let items = fetchByPage baseUrl page

    match Seq.length(items) with
    | noItems when noItems = pagination -> Seq.concat [ items; fetchAllItems baseUrl (page+1) allItems ]
    | noItems when noItems < pagination -> Seq.concat [ items; allItems ]
    | _ -> allItems
