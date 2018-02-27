module Users

open FSharp.Data
open FSharp.Data.JsonExtensions

let baseUrl = "https://driftrock-dev-test.herokuapp.com/users?"

let getUsers' (httpCall:string -> seq<JsonValue>) : seq<JsonValue> = httpCall baseUrl

let getUsers : seq<JsonValue> = getUsers' HttpCaller.fetchAll

let findUserBy (property:string) (user:JsonValue) (target:string) =
    (user.GetProperty(property).AsString() = target)

let getById' (httpCall:string -> seq<JsonValue>) (id:string) : string =
    let findUserById = findUserBy "id"
    let user =
        getUsers' httpCall
        |> Seq.find (fun user -> findUserById user id)

    [user?first_name.AsString(); user?last_name.AsString()] |> String.concat(" ")

let getById (id:string) : string = getById' HttpCaller.fetchAll id

let getByEmail' (httpCall:string -> seq<JsonValue>) (email:string) : JsonValue =
    let findUserByEmail = findUserBy "email"
    getUsers' httpCall
    |> Seq.find (fun user -> findUserByEmail user email)

let getByEmail (email:string) : JsonValue = getByEmail' HttpCaller.fetchAll email
