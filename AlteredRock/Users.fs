module Users

open FSharp.Data
open FSharp.Data.JsonExtensions

let baseUrl = "https://driftrock-dev-test.herokuapp.com/users?"

let getUsers : seq<JsonValue> = HttpCaller.fetchAll baseUrl

let getById (id:string) : string =
    let user =
        getUsers
        |> Seq.find (fun user -> user.GetProperty("id").AsString() = id)

    [user?first_name.AsString(); user?last_name.AsString()] |> String.concat(" ")

let getByEmail (email:string) : JsonValue =
    getUsers
    |> Seq.find (fun user -> user.GetProperty("email").AsString() = email)
