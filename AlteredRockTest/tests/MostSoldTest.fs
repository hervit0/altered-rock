module MostSoldTest

open Fuchu
open FSharp.Data

let getPurchase (itemName:string) : JsonValue =
    sprintf "{ \"item\": \"%s\" }" itemName
    |> JsonValue.Parse
let watchPurchase = getPurchase "Watch"
let bagPurchase = getPurchase "Bag"

[<Tests>]
let mostSoldTestNoPurchase =
        testCase "when there is no purchase" <|
            let mockResponse = Seq.empty

            let mockHttpCall = fun _purchases -> mockResponse
            fun _ -> Assert.Equal("returns not found", Analytics.getMostSold' mockHttpCall, "No most sold item found")

[<Tests>]
let mostSoldTestUniqPurchase =
        testCase "when there is a uniq mostSold purchase" <|
            let mockResponse = seq [watchPurchase;watchPurchase;bagPurchase]
            let mockHttpCall = fun _purchases -> mockResponse
            fun _ -> Assert.Equal("returns uniq item", Analytics.getMostSold' mockHttpCall, "Watch")

[<Tests>]
let mostSoldTestMultiplePurchases =
        testCase "when there are multiple mostSold purchases" <|
            let mockResponse = seq [watchPurchase;bagPurchase]

            let mockHttpCall = fun _purchases -> mockResponse
            fun _ -> Assert.Equal("returns all items", Analytics.getMostSold' mockHttpCall, "Watch\nBag")
