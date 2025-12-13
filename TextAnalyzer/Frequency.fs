namespace TextAnalyzer
module Frequency =
    open Types
    let getTopTenWordsFrequencies (words: string list) : WordFrequency list =
        words
        |> List.countBy id
        |> List.sortByDescending snd
        |> List.truncate 10
        |> List.map (fun (word, count) -> { Word = word; Count = count })

