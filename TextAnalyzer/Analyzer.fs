namespace TextAnalyzer

module Analyzer =
    open Types
    open Tokenizer
    open ScoreTables
    open Metrics
    open Frequency
    open System.Text.RegularExpressions
    open System
    open System.IO
    open System.Text
    open System.Text.Json
    

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
            Text= cleanText
        }