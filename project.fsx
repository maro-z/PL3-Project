open System.Text.RegularExpressions
open System
open System.IO
open System.Text

let normalizeText (input: string) : string =
    input.ToLowerInvariant().Trim()

let loadFile (filePath: string) : Result<string, string> =
    try 
        let text = File.ReadAllText(filePath, Encoding.UTF8)
        let cleanedText = normalizeText text
        
        if String.IsNullOrWhiteSpace cleanedText then
            Error (sprintf "ERROR: File (%s) is empty or contains only whitespace." filePath)
        else
            Ok cleanedText 
        
    with 
    | :? FileNotFoundException ->
        Error (sprintf "ERROR: File not found at path: %s" filePath)
    | :? IOException as ex ->
        Error (sprintf "ERROR: An I/O error occurred: %s" ex.Message)
    | :? DecoderFallbackException as ex ->
        Error (sprintf "ERROR: Encoding validation failed (Invalid UTF-8 sequence). Details: %s" ex.Message)
    | ex ->
        Error (sprintf "ERROR: An unexpected error occurred: %s" ex.Message)

let handleManualInput (rawText: string) : Result<string, string> =
    if String.IsNullOrWhiteSpace rawText then
        Error "ERROR: Manual input cannot be empty."
    else
        Ok (normalizeText rawText)

let printResultTest (name: string) (result: Result<string, string>) =
    printfn "\n--- TEST CASE: %s ---" name
    match result with
    | Ok cleanText -> 
        printfn "SUCCESS: Input processed successfully."
        printfn "Cleaned Text Length: %d" cleanText.Length
        printfn "Starts with: '%s...'" (cleanText.Substring(0, Math.Min(cleanText.Length, 30)))
    | Error msg -> 
        printfn "FAILURE: Input processing failed!"
        printfn "Reason: %s" msg

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

//results data types
type FleschKincaidDetails = {
    ReadingLevel: string
    SchoolLevel: string
    AgeRange: string
}

type ScoreLevelDetails = {
    Age: string
    GradeLevel: string
}


//score tabels functions
let getFleschKincaidDetails (score: int) : FleschKincaidDetails =
    match score with
    | 0|1|2 ->
        { ReadingLevel = "Basic";
          SchoolLevel = "Kindergarten / Elementary";
          AgeRange = "5 - 8" }
    | 3|4|5 ->
        { ReadingLevel = "Basic";
          SchoolLevel = "Elementary";
          AgeRange = "8 - 11" }
    |6|7|8 ->
        { ReadingLevel = "Average";
          SchoolLevel = "Middle School";
          AgeRange = "11 - 14" }
    |9|10|11 ->
        { ReadingLevel = "Average";
          SchoolLevel = "High School";
          AgeRange = "14 - 17" }
    |12|13|14 ->
        { ReadingLevel = "Advanced";
          SchoolLevel = "College";
          AgeRange = "17 - 20" }
    |15|16|17|18 ->
        { ReadingLevel = "Advanced";
          SchoolLevel = "Post-grad";
          AgeRange = "20+" }
    | _ ->
        { ReadingLevel = "Undefined";
          SchoolLevel = "Score Out of Range";
          AgeRange = "N/A" }

let ARIScoreLevelDetails (score: int) : ScoreLevelDetails =
    match score with
    | 1 ->
        { Age = "5 - 6";
          GradeLevel = "Kindergarten" }
    | 2 ->
        { Age = "6 - 7";
          GradeLevel = "First/second grade" }
    | 3 ->
        { Age = "7 - 9";
          GradeLevel = "Third grade" }
    | 4 ->
        { Age = "9 - 10";
          GradeLevel = "Fourth grade" }
    | 5 ->
        { Age = "10 - 11";
          GradeLevel = "Fifth grade" }
    | 6 ->
        { Age = "11 - 12";
          GradeLevel = "Sixth grade" }
    | 7 ->
        { Age = "12 - 13";
          GradeLevel = "Seventh grade" }
    | 8 ->
        { Age = "13 - 14";
          GradeLevel = "Eighth grade" }
    | 9 ->
        { Age = "14 - 15";
          GradeLevel = "Ninth grade" }
    | 10 ->
        { Age = "15 - 16";
          GradeLevel = "Tenth grade" }
    | 11 ->
        { Age = "16 - 17";
          GradeLevel = "Eleventh grade" }
    | 12 ->
        { Age = "17 - 18";
          GradeLevel = "Twelfth grade" }
    | 13 ->
        { Age = "18 - 24";
          GradeLevel = "College student" }
    | 14 ->
        { Age = "24+";
          GradeLevel = "Professor" }
    | _ ->
        { Age = "N/A";
          GradeLevel = "Invalid Score" }


let safeDiv a b = if b = 0.0 then 0.0 else a / b

//Average Sentence Length
let ASL (w:string list)(s:string list) = safeDiv (float w.Length) (float s.Length)

//Average Word Length
let AWL (w:string list) = safeDiv (float (w |> List.map(fun x -> x.Length)|>List.sum)) (float w.Length)

//Syllable counter

//first count number oof vowels two or more vowels coming after one another are counted as one 
let countVowelSequencesRegex (input: string) =
    let vowelPattern = "[aeiouy]+"
    Regex.Matches(input, vowelPattern, RegexOptions.IgnoreCase)
    |> fun matches -> matches.Count

//second remove the spicial case of the silent e at the end
let WordSylCount(input:string)=
    let syl = countVowelSequencesRegex(input)
    if(input.EndsWith("e")) then
        if(input.EndsWith("ee")) then syl
        elif(syl>1) then syl-1
        else syl
    else syl        

//finally count the syllabels for each word and sum them to get the final syllabels count
let SyllableCount (w:string list) =float (w|>List.map(WordSylCount)|>List.sum)

//Average syllabel per word
let ASPW (w:string list) = safeDiv (SyllableCount(w)) (float w.Length)

//average number of complex words
let ComplexWordsCount (w:string list) = float (w|>List.map(WordSylCount)|>List.filter(fun x-> x>=3)|>List.length)
let ACW(w: string list) = ComplexWordsCount w / float w.Length


//known metrics for readability

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

type WordFrequency = {
    Word: string
    Count: int
}

let getTopTenWordsFrequencies (words: string list) : WordFrequency list =
    words
    |> List.countBy id
    |> List.sortByDescending snd
    |> List.truncate 10
    |> List.map (fun (word, count) -> { Word = word; Count = count })

type AnalysisReport = {
    FRE: int
    FKGL: int
    GFI: int
    ARI: int
    ParagraphCount : int
    SentenceCount : int
    WordCount : int
    ASL: float
    AWL: float
    ASPW: float
    ACW: float
    FKGLDetails: FleschKincaidDetails
    ARIDetails: ScoreLevelDetails
    TopWords: WordFrequency list
}

let saveJsonToFile (filePath: string) (json: string) =
    try
        File.WriteAllText(filePath, json)
        Ok ()
    with
    | ex -> Error ex.Message

let generateJsonReport (report: AnalysisReport) : string =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(report, options)

let analyzeText (cleanText: string) : AnalysisReport =
    let paragraphs, sentences, words = tokenizeAll cleanText

    let fre = FRE words sentences
    let fkgl = FKGL words sentences
    let gfi = GFI words sentences
    let ari = ARI words sentences

    let roundedFRE = int (Math.Round(float fre, MidpointRounding.AwayFromZero))
    let roundedFKGL = int (Math.Round(float fkgl, MidpointRounding.AwayFromZero))
    let roundedGFI = int (Math.Round(float gfi, MidpointRounding.AwayFromZero))
    let roundedARI = int (Math.Round(float ari, MidpointRounding.AwayFromZero))

    let FKGLresult = getFleschKincaidDetails roundedFKGL
    let ARIresult = ARIScoreLevelDetails roundedARI

    let topWords = getTopTenWordsFrequencies words

    {
        FRE = roundedFRE
        FKGL = roundedFKGL
        GFI = roundedGFI
        ARI = roundedARI

        ParagraphCount = paragraphs.Length
        SentenceCount = sentences.Length
        WordCount = words.Length

        ASL = ASL words sentences
        AWL = AWL words
        ASPW = ASPW words
        ACW = ACW words

        FKGLDetails = FKGLresult
        ARIDetails = ARIresult
        TopWords = topWords
    }

//sample test :(
let filename = "sample.txt"

match loadFile filename with
| Ok cleanText ->
    let report = analyzeText cleanText
    let jsonReport = generateJsonReport report

    match saveJsonToFile "analysis_report.json" jsonReport with
    | Ok () -> printfn "JSON report saved successfully."
    | Error err -> printfn "Failed to save JSON: %s" err

| Error msg -> 
    printfn "\n--- Input Processing Failed ---"
    printfn "Reason: %s" msg
