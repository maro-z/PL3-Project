namespace TextAnalyzer

module Json =
  open Types
  open System.Text.Json
  open System.IO

  let generateJsonReport (report: AnalysisReport) : string =
    let options = JsonSerializerOptions(WriteIndented = true)
    JsonSerializer.Serialize(report, options)

  let saveJsonToFile (filePath: string) (json: string) =
    try
      File.WriteAllText(filePath, json)
      Ok ()
    with
    | ex -> Error ex.Message