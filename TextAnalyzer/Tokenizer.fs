namespace TextAnalyzer

module Tokenizer =
  open System.Text.RegularExpressions
  let splitParagraph (text: string) =
    Regex.Split(text, @"(\r?\n\s*\r?\n)+")
    |> Array.map (fun p -> p.Trim())
    |> Array.filter (fun p -> p <> "")
    |> Array.toList


  let splitSentence (text: string) =
    let abbreviations = @"(?:Mr|Mrs|Ms|Dr|Prof|St|Sr|Jr|vs|etc|p\.m|a\.m|P\.M|A\.M)"
    let pattern = @"(?<!\b" + abbreviations + @")(?<=[\.!\?])\s+"

    Regex.Split(text, pattern)
    |> Array.map (fun s -> s.Trim())
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
