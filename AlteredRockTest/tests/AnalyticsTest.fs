module AnalyticsTest

open Fuchu

[<Tests>]
let mostSoldTestNoPurchase =
        testCase "when no purchase" <|
            let mockResponse = Seq.empty
            let mockHttpCall = fun _purchases -> mockResponse
            fun _ -> Assert.Equal("returns not found", Analytics.getMostSold' mockHttpCall, "No most sold item found")

[<Tests>]
let mostSoldTestUniqPurchase =
        testCase "when uniq mostSold purchase" <|
            let mockResponse = Seq.empty
            let mockHttpCall = fun _purchases -> mockResponse
            fun _ -> Assert.Equal("returns not found", Analytics.getMostSold' mockHttpCall, "No most sold item found")
