module App

open Sutil
open Fable.Core.JsInterop

open Sutil.CoreElements
open NynorskTranslator

importAll "./index.css"

let input = Store.make "Man må alltid gjøre det man er overbevist om, selv om det kan være vanskelig"
let output = Store.map translate input    

let header = 
    Html.divc "py-5 bg-slate-600 shadow-lg shadow-gray-700/50" [
        Html.divc "container mx-auto px-3 text-white drop-shadow-lg shadow-green-900" [
            Html.h1 [ text "Totally Akkurate Nynorsk Translator"; class' "text-2xl font-bold leading-7 sm:truncate sm:text-3xl sm:tracking-tight" ]
            Html.h3 [ text "Translates a good Norwegian setning into somewhat tvilsom Nynorsk"; class' "text-lg" ]
            ]
        ]

let inputs = 
    Html.divc "container mx-auto px-3 flex flex-col lg:flex-row gap-3" [
        Html.divc "flex-1" [ 
            text "Norsk"
            Html.textarea [
            Attr.rows 5
            Bind.attr("value", input)
            class' "w-full border rounded-md align-text-top px-1"
            ]
            ]
        
        Html.divc "flex-1" [ 
            text "Nynorsk"
            Html.textarea [
                Attr.rows 5
                Attr.readOnly true
                Bind.attr("value", output)
                class' "w-full border rounded-md align-text-top px-1"
            ]
        ]
    ]

let app () =
    Html.divc "flex flex-col gap-5" [
        header
        inputs
    ]

app () |> Program.mountElement "sutil-app"
