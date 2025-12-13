namespace TextAnalyzer.Tests

open Xunit
open TextAnalyzer.Frequency
open TextAnalyzer.Types

module FrequencyTests =

    [<Fact>]
    let ``getTopTenWordsFrequencies counts words correctly`` () =
        let words = ["a"; "b"; "a"; "c"; "b"; "a"]

        let result = getTopTenWordsFrequencies words

        let a = result |> List.find (fun x -> x.Word = "a")
        let b = result |> List.find (fun x -> x.Word = "b")

        Assert.Equal(3, a.Count)
        Assert.Equal(2, b.Count)

    [<Fact>]
    let ``getTopTenWordsFrequencies limits output to ten`` () =
        let words = [ for i in 1..20 -> string i ]

        let result = getTopTenWordsFrequencies words

        Assert.Equal(10, result.Length)

    [<Fact>]
    let ``getTopTenWordsFrequencies handles empty list`` () =
        let result = getTopTenWordsFrequencies []

        Assert.Empty(result)
