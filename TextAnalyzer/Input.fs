namespace TextAnalyzer

module Input =
    open System
    open System.IO
    open System.Text
    
    let normalizeText (input: string) : string =
        input.Trim()

    let loadFile (filePath: string) : Result<string, string> =
        printfn "File loaded successfully from: %s" filePath
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
