namespace TextAnalyzer.Tests

open Xunit
open TextAnalyzer
open TextAnalyzer.Analyzer
open TextAnalyzer.Types

module AnalyzerTests =

    [<Fact>]
    let ``AnalyzeText returns correct word and sentence counts`` () =
        let text = "Hello world. This is a test."

        let result = analyzeText text

        Assert.Equal(2, result.SentenceCount)
        Assert.Equal(6, result.WordCount)
        Assert.Equal(text, result.Text)

    [<Fact>]
    let ``AnalyzeText returns non-negative readability scores`` () =
        let text = "This is a simple sentence."

        let result = analyzeText text

        Assert.True(result.FRE >= 0)
        Assert.True(result.FKGL >= 0)
        Assert.True(result.GFI >= 0)
        Assert.True(result.ARI >= 0)

    [<Fact>]
    let ``AnalyzeText computes paragraph count correctly`` () =
        let text = "First paragraph.\n\nSecond paragraph."

        let result = analyzeText text

        Assert.Equal(2, result.ParagraphCount)

    [<Fact>]
    let ``AnalyzeText top words are not empty for non-empty text`` () =
        let text = "hello hello world world world test"

        let result = analyzeText text

        Assert.NotEmpty(result.TopWords)
        Assert.True(result.TopWords |> List.exists (fun w -> w.Word = "world"))

    [<Fact>]
    let ``AnalyzeText handles empty string safely`` () =
        let text = ""

        let result = analyzeText text

        Assert.Equal(0, result.WordCount)
        Assert.Equal(0, result.SentenceCount)
        Assert.Equal(0, result.ParagraphCount)
