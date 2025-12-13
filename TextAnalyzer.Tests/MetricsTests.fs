namespace TextAnalyzer.Tests

open Xunit
open TextAnalyzer.Metrics

module MetricsTests =

    [<Fact>]
    let ``ASL computes average sentence length`` () =
        let words = ["a"; "b"; "c"; "d"]
        let sentences = ["one"; "two"]

        let asl = ASL words sentences

        Assert.Equal(2.0, asl)

    [<Fact>]
    let ``AWL computes average word length`` () =
        let words = ["hi"; "hello"]

        let awl = AWL words

        Assert.Equal(3.5, awl)

    [<Fact>]
    let ``ASPW computes average syllables per word`` () =
        let words = ["hello"; "world"]

        let aspw = ASPW words

        Assert.True(aspw > 0.0)

    [<Fact>]
    let ``safeDiv returns zero when dividing by zero`` () =
        let result = safeDiv 10.0 0.0

        Assert.Equal(0.0, result)

    [<Fact>]
    let ``FRE returns a finite value`` () =
        let words = ["simple"; "test"; "sentence"]
        let sentences = ["one"]

        let fre = FRE words sentences

        Assert.False(System.Double.IsNaN fre)

    [<Fact>]
    let ``ARI returns a finite value`` () =
        let words = ["another"; "test"]
        let sentences = ["one"]

        let ari = ARI words sentences

        Assert.False(System.Double.IsInfinity ari)
