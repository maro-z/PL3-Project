namespace TextAnalyzer

module Tokenizer =
    open System.Text.RegularExpressions
    let splitParagraph (text: string) =
        Regex.Split(text, @"(\r?\n\s*\r?\n)+")
        |> Array.map (fun p -> p.Trim())
        |> Array.filter (fun p -> p <> "")
        |> Array.toList

    let protectAbbreviations (text: string) =
        let absolutes = [ "Mr"; "Mrs"; "Ms"; "Dr"; "Prof"; "St"; "Sr"; "Jr"; "vs"; "etc" ]
        let times = [ "p.m"; "a.m"; "P.M"; "A.M"; "P.m"; "A.m"; "p.M"; "a.M" ]

        let t1 =
            absolutes
            |> List.fold (fun acc abbr ->
                Regex.Replace(
                    acc,
                    $@"\b{abbr}\.(?=\s+\p{{L}})",
                    $"{abbr}<DOT>",
                    RegexOptions.IgnoreCase
                )
            ) text

        times
        |> List.fold (fun acc abbr ->
            Regex.Replace(
                acc,
                $@"\b{abbr}\.(?=\s+\p{{Ll}})",
                $"{abbr}<DOT>"
            )
        ) t1


    let splitSentence (text: string) =
        let protectedText = protectAbbreviations text

        Regex.Split(protectedText, @"(?<=[.!?])\s+")
        |> Array.map (fun s -> s.Replace("<DOT>", ".").Trim())
        |> Array.filter (fun s -> s <> "")
        |> Array.toList

    let splitWord (text: string) =
        let urlEmailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}|https?://\S+"

        let urlEmailMatches = Regex.Matches(text, urlEmailPattern)
        let urlEmailTokens = 
            urlEmailMatches 
            |> Seq.cast<Match>
            |> Seq.map (fun m -> m.Value)

        let cleaned = Regex.Replace(text, urlEmailPattern, " ")

        let words =
            Regex.Split(cleaned.ToLower(), @"[^a-z0-9_'\-]+")
            |> Array.filter (fun w -> w <> "")

        (urlEmailTokens |> Seq.append words)
        |> Seq.toList


    let tokenizeAll text =
        let paragraphs = splitParagraph text
        let sentences = splitSentence text
        let words = splitWord text
        (paragraphs, sentences, words)
