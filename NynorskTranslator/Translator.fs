module NynorskTranslator

open System

let private replacements = 
    Map [
        "ditt", "dykkar"
        "mens", "mens"
        "å", "at"
        "en", "ein"
        "et", "eit"
        "jeg", "eg"
        "vi", "me"
        "hun", "ho"
        "de", "dei"
        "dere", "dykker"
        "ikke", "ikkje"
        "mye", "mykje"
        "noe", "noko"
        "noen", "nokre"
        "ble", "vart"
        "kommer", "kjem"
    ]
    
let private (|EndsWith|_|) (pattern:string) (word:string) = 
    if word.EndsWith(pattern) then Some word[..(word.Length - pattern.Length)]//word[..^pattern.Length]
    else None
    
let private (|StartsWith|_|) (pattern:string) (word:string) = 
    if word.StartsWith(pattern) then Some word[pattern.Length..]
    else None
    
let private (|HasReplacement|_|) (word:string) = replacements |> Map.tryFind word
    
let private replaceEnding word =
    match word with
    | HasReplacement replacement -> replacement
    | EndsWith "lighet" rest -> rest + "leik"
    | EndsWith "somhet" rest -> rest + "semd"
    | EndsWith "het" rest -> rest + "heit"
    | EndsWith "som" rest -> rest + "sam"
    | EndsWith "løs" rest -> rest + "laus"
    | EndsWith "lig" rest -> rest + "leg"
    | EndsWith "dan" rest -> rest + "leis"
    | EndsWith "else" rest -> rest + "nad"
    | EndsWith "lse" rest -> rest + "sle"
    | EndsWith "lig" rest -> rest + "leg"
    | EndsWith "øst" rest -> rest + "aust"
    | EndsWith "isse" rest -> rest + "esse"
    | EndsWith "ikk" rest -> rest + "ekk"

    | EndsWith "ie" rest -> rest + "ie"
    | EndsWith "rt" rest -> rest + "rti"
    | EndsWith "ke" rest -> rest + "kje"

    | EndsWith "ene" rest -> rest + "a"
    | EndsWith "eke" rest -> rest + "eike"
    | EndsWith "ende" rest -> rest + "ande"
    | EndsWith "ere" rest -> rest + "are"
    | EndsWith "e" rest -> rest + "i"
    | EndsWith "es" rest -> rest + "ast"
    | EndsWith "et" rest -> rest + "i"
    | EndsWith "ete" rest -> rest + "eite"
    | EndsWith "eter" rest -> rest + "eitar"
    | EndsWith "elig" rest -> rest + "eleg"
    | EndsWith "er" rest -> rest + "ar"
    | EndsWith "en" rest -> rest + "an"

    | EndsWith "i" rest -> rest + "iar"
    | EndsWith "a" rest -> rest + "a"
    | _ -> word + "ur"
    
let private replaceStart word =
    match word with
    | HasReplacement replacement -> replacement
    //| StartsWith "an" rest -> "an" + rest
    //| StartsWith "be" rest -> rest
    | StartsWith "hvor" rest -> "kvis" + rest
    | StartsWith "hvert" rest -> "kvart" + rest
    | StartsWith "hva" rest -> "kva" + rest
    | StartsWith "hvem" rest -> "kven" + rest
    | StartsWith "egen" rest -> "eigen" + rest
    | StartsWith "kjed" rest -> "keise" + rest
    | StartsWith "red" rest -> "rei" + rest
    | _ -> word

let private translateToNynorsk (word:string) =
    word
    |> replaceStart
    |> replaceEnding

let rec private translateWord (word:string) =
    let firstLetterCapitalized = Char.IsUpper word[0]
    let translatedWord =
        match word.ToLower() with
        | HasReplacement replacement -> replacement
        | word when word.Length <= 3 -> word
        //| word when Char.IsPunctuation word[^0] -> translateToNynorsk word[..^1] + word[^0..^0]
        | word when Char.IsPunctuation word[word.Length-1] -> $"{translateWord word[..word.Length-2]}{word[word.Length-1]}"
        | _ -> translateToNynorsk word
    if firstLetterCapitalized
    then string (translatedWord[0] |> Char.ToUpper) + translatedWord[1..]
    else translatedWord

let translateLine (line:string) = 
    line.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun str -> str.Trim())
        |> Seq.map translateWord
        |> String.concat " "
let translate (sentence:string) = 
    sentence.Split([| "\r\n"; "\n" |], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.map translateLine
    |> String.concat "\n"