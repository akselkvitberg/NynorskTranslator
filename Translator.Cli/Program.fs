open NynorskTranslator
open SpectreCoff

"Totally Akurate Nynorsk Oversettar"
|> figlet
|> toConsole


let rec translateLoop () =
    let input =
        ask<string> ">" 
        
    Translator.translate input
    |> Pumped
    |> toConsole
    
    translateLoop ()
    
translateLoop ()