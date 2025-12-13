namespace TextAnalyzer

module Metrics =
    open System.Text.RegularExpressions

    let safeDiv a b = if b = 0.0 then 0.0 else a / b

    //Average Sentence Length
    let ASL (w:string list)(s:string list) = safeDiv (float w.Length) (float s.Length)

    //Average Word Length
    let AWL (w:string list) = safeDiv (float (w |> List.map(fun x -> x.Length)|>List.sum)) (float w.Length)


    let countVowelSequencesRegex (input: string) =
        let vowelPattern = "[aeiouy]+"
        Regex.Matches(input, vowelPattern, RegexOptions.IgnoreCase)
        |> fun matches -> matches.Count

    let countVowelSequencesRegex (input: string) =
        let vowelPattern = "[aeiouy-]+"
        Regex.Matches(input, vowelPattern, RegexOptions.IgnoreCase)
        |> fun matches -> matches.Count

    let SpecialWords(input:string)=
        let syl = countVowelSequencesRegex(input)
        if (syl = 0) then 1
        else syl

    let SyllableCount (w:string list) =float (w|>List.map(WordSylCount)|>List.sum)

    //Average syllabel per word
    let ASPW (w:string list) = safeDiv (SyllableCount(w)) (float w.Length)

    //average number of complex words
    let ComplexWordsCount (w:string list) = float (w|>List.map(WordSylCount)|>List.filter(fun x-> x>=3)|>List.length)
    let ACW(w: string list) = ComplexWordsCount w / float w.Length



    //FRE (Flesch Reading Ease Score)
    //Formula: 206.835 - 1.015(ASL)-84.6(ASPW)
    let FRE (w:string list)(s:string list) = 206.85 - (1.015*ASL w s) - (84.6*ASPW w)

    //FKGL (Flesch-Kincaid Grade Level)
    //Formula: 0.39(ASL)+11.8(ASPW)-15.59
    let FKGL (w:string list)(s:string list) = (0.39*ASL w s) + (11.8*ASPW w) - 15.59

    //GFI (the Gunning Fog Index)
    //Formula: 0.4*(ASL + 100 ACW)
    let GFI (w:string list)(s:string list) = 0.4 * (ASL w s + (ACW w * 100.0) )

    //ARI (Automated Readability Index)
    //Formula: 4.71 (AWL) + 0.5 (ASL) - 21.43
    let ARI (w:string list)(s:string list) = (4.71 * AWL w) + (0.5 * ASL w s) - 21.43
