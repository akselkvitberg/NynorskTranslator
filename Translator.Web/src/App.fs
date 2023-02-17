module App

open Sutil
open Fable.Core.JsInterop

open Sutil.CoreElements

open TranslationStore

importAll "./index.css"

let getTranslatedText m = m.output |> Option.defaultValue "Text not set"

let app () =
    let model, dispatch = () |> Store.makeElmish TranslationStore.init TranslationStore.update ignore

    Html.div [ 
        class' "container mx-auto"
        Html.h1 [
            class' "text-3xl font-bold"
            text "Hello world!" ]
        Html.div [
            Html.p [ Bind.fragment (model |> Store.map getTranslatedText) <| fun n -> Html.text $"Text = {n}" ] ]
        Html.div [ 
            Html.button [
                class' "btn"
                onClick (fun _ -> dispatch (TranslationStore.SetText ("Hello World"))) []
                Html.text "(+)" ] ]
        Html.div [ 
            Html.button [
                class' "btn"
                onClick (fun _ -> dispatch TranslationStore.ClearText) []
                Html.text "(-)" ] ]                
    ]

app () |> Program.mountElement "sutil-app"
