open System.Text.RegularExpressions
open System

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


let paragraphs, sentences, words = tokenizeAll("""First paragraph text here! It has multiple sentences? Yes!!!


Second paragraph: includes numbers (123), symbols like email@test.com,  
and abbreviations such as Dr. John is here at 5 p.m.


Third paragraph has tricky words—
hyphenated-words, snake_case, camelCase, and ellipsis...
Also "quoted sentences." And a sentence without ending
""")
printfn "paragraphs: %A\n" paragraphs
printfn "sentences: %A\n" sentences
printfn "words: %A\n" 
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

//Average Sentence Length
let ASL (w:string list)(s:string list) =float w.Length/float s.Length

//Average Word Length
let AWL (w:string list) = float (w|>List.map(fun x -> x.Length)|>List.sum) / float w.Length

//Syllable counter

//first count number oof vowels two or more vowels coming after one another are counted as one 
let countVowelSequencesRegex (input: string) =
    let vowelPattern = "[aeiouy]+"
    Regex.Matches(input, vowelPattern, RegexOptions.IgnoreCase)
    |> fun matches -> matches.Count

//second remove the spicial case of the silent e at the end
let WordSylCount(input:string)=
    let syl = countVowelSequencesRegex(input)
    if(input.EndsWith('e')) then
        if(input.EndsWith("ee")) then syl
        elif(syl>1) then syl-1
        else syl
    else syl        

//finally count the syllabels for each word and sum them to get the final syllabels count
let SyllableCount (w:string list) =float (w|>List.map(WordSylCount)|>List.sum)

//Average syllabel per word
let ASPW (w:string list) = SyllableCount(w) / float w.Length

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


//Results 
let fre = FRE words sentences
let fkgl = FKGL words sentences
let gfi = GFI words sentences
let ari = ARI words sentences
let roundedFRE= int (Math.Round(float fre, MidpointRounding.AwayFromZero))
let roundedFKGL = int (Math.Round(float fkgl, MidpointRounding.AwayFromZero))
let  roundedGFI = int (Math.Round(float gfi, MidpointRounding.AwayFromZero))
let roundedARI = int (Math.Round(float ari, MidpointRounding.AwayFromZero))
let FKGLresult = getFleschKincaidDetails roundedFKGL
let ARIresult = ARIScoreLevelDetails roundedARI

//Printing Results
printfn "Flesh kincaid results"
printfn "your text score is : %d" roundedFRE
printfn "your text can be read by grade : %d" roundedFKGL
printfn "%A" FKGLresult
printf "\n"
printfn "the Gunning Fog Index results"
printfn "your text score is grade: %d" roundedGFI
printf "\n"
printfn "the Automated Readability Index results"
printfn "your text score is grade: %d" roundedARI
printfn "%A" ARIresult