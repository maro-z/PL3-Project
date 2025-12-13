namespace TextAnalyzer

module ScoreTables =
    open Types
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
