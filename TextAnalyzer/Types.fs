namespace TextAnalyzer

module Types =

    type FleschKincaidDetails = {
        ReadingLevel: string
        SchoolLevel: string
        AgeRange: string
    }

    type ScoreLevelDetails = {
        Age: string
        GradeLevel: string
    }

    type WordFrequency = {
        Word: string
        Count: int
    }

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
        Text: string
}