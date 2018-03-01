module PurchasesTest

open Fuchu
open FSharp.Data
open Purchases

[<Tests>]
let getPurchasesTest =
    testCase "Test the simple mock pattern" <|
        let mockResponse =
            seq { for x in 1 .. 5 -> sprintf "%i" x }
            |> Seq.map JsonValue.String

        let mockHttpCall = fun _ -> mockResponse

        fun _ -> Assert.Equal("", getPurchases' mockHttpCall, mockResponse)
