namespace TextAnalyzer.Tests

open Xunit
open TextAnalyzer.Tokenizer

module TokenizerTests =

    [<Fact>]
    let ``splitParagraph splits paragraphs on blank lines`` () =
        let text = "Para one.\n\nPara two.\n\nPara three."

        let paragraphs = splitParagraph text

        Assert.Equal(3, paragraphs.Length)
        Assert.Equal("Para one.", paragraphs.[0])

    [<Fact>]
    let ``splitSentence splits sentences but respects punctuation`` () =
        let text = "Hello world. This is a test! Is it working?"

        let sentences = splitSentence text

        Assert.Equal(3, sentences.Length)

    [<Fact>]
    let ``splitSentence does not split common abbreviations`` () =
        let text = "Dr. Smith is here. He arrived at 5 p.m."

        let sentences = splitSentence text

        Assert.Equal(2, sentences.Length)

    [<Fact>]
    let ``splitWord lowercases words and removes punctuation`` () =
        let text = "Hello, WORLD!!!"

        let words = splitWord text

        Assert.Contains("hello", words)
        Assert.Contains("world", words)

    [<Fact>]
    let ``splitWord keeps emails and urls intact`` () =
        let text = "Contact me at test@example.com or visit https://example.com"

        let words = splitWord text

        Assert.Contains("test@example.com", words)
        Assert.Contains("https://example.com", words)

    [<Fact>]
    let ``tokenizeAll returns consistent counts`` () =
        let text = "Hello world. This is a test.\n\nNew paragraph."

        let paragraphs, sentences, words = tokenizeAll text

        Assert.Equal(2, paragraphs.Length)
        Assert.Equal(3, sentences.Length)
        Assert.True(words.Length > 0)
