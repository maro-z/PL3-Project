namespace TextAnalyzer

#load "Types.fs"
#load "Input.fs"
#load "Tokenizer.fs"
#load "ScoreTables.fs"
#load "Metrics.fs"
#load "Frequency.fs"
#load "Json.fs"
#load "Analyzer.fs"

open Input
open Types
open Analyzer
open Json

module Program =
    open System

    let private processCleanText (cleanText: string) =
        let report = analyzeText cleanText
        Ok report

    let analyzeFile (filePath: string) : Result<AnalysisReport, string> =
        loadFile filePath
        |> Result.bind processCleanText

    let analyzeText (inputText: string) : Result<AnalysisReport, string> =
        handleManualInput inputText
        |> Result.bind processCleanText

    let analyzeTextToJson (analysisReport: AnalysisReport) (filePath: String) : Result<string, string> =
        let json = generateJsonReport analysisReport
        match saveJsonToFile filePath json with
        | Ok () -> Ok json
        | Error e -> Error e

