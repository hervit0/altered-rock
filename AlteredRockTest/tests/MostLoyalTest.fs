module MostLoyalTest

open Fuchu
open FSharp.Data

let getPurchase (userId:string) : JsonValue =
    sprintf "{ \"user_id\": \"%s\" }" userId
    |> JsonValue.Parse
let getUser (userId:string) (firstName:string) (lastName:string) : JsonValue =
    sprintf("{ \"id\": \"%s\", \"first_name\": \"%s\", \"last_name\": \"%s\" }") userId firstName lastName
    |> JsonValue.Parse
let johnPurchase = getPurchase "john_id"
let tomPurchase = getPurchase "tom_id"
let john = getUser "john_id" "John" "Wick"
let tom = getUser "tom_id" "Tom" "Cruise"

[<Tests>]
let mostLoyalTestNoPurchase =
        testCase "when there is no user" <|
            let mockResponse = Seq.empty

            let mockHttpCall = fun _users -> mockResponse
            fun _ -> Assert.Equal("returns not found", Analytics.getMostLoyal' mockHttpCall, "No loyal user found")

[<Tests>]
let mostLoyalTestUniqPurchase =
        testCase "when there is a uniq mostLoyal user" <|
            let mockPurchasesResponse = seq [johnPurchase;johnPurchase;tomPurchase]
            let mockUsersResponse = seq [john]

            let mockHttpCall = fun url ->
                match url with
                | url when url = Purchases.baseUrl -> mockPurchasesResponse
                | url when url = Users.baseUrl -> mockUsersResponse
                | _ -> Seq.empty
            fun _ -> Assert.Equal("returns uniq user", Analytics.getMostLoyal' mockHttpCall, "John Wick")

[<Tests>]
let mostLoyalTestMultiplePurchase =
        testCase "when there are multiple mostLoyal users" <|
            let mockPurchasesResponse = seq [johnPurchase;tomPurchase]
            let mockUsersResponse = seq [john;tom]

            let mockHttpCall = fun url ->
                match url with
                | url when url = Purchases.baseUrl -> mockPurchasesResponse
                | url when url = Users.baseUrl -> mockUsersResponse
                | _ -> Seq.empty
            fun _ -> Assert.Equal("returns multiple users", Analytics.getMostLoyal' mockHttpCall, "John Wick\nTom Cruise")
