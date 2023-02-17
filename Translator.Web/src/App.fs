module App

open Sutil
open Fable.Core.JsInterop

open Sutil
open Sutil.Styling
open Sutil.Core
open Sutil.CoreElements
open Sutil.DomHelpers


importAll "./index.css"

type Model = { count: int }

type Msg =
    | Increment
    | Decrement

let init () : Model * Cmd<Msg> =
    let model = { count = 0 }
    let cmd = Cmd.none
    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | Increment -> { model with count = model.count + 1 }, Cmd.none
    | Decrement -> { model with count = model.count - 1 }, Cmd.none

let getCounter m = m.count

let app () =
    let model, dispatch = () |> Store.makeElmish init update ignore

    Html.div [ 
        class' "container mx-auto"
        Html.h1 [
            class' "text-2xl font-bold underline"
            Html.text "Hello world!" ]
        Html.div [
            Html.p [ Bind.fragment (model |> Store.map getCounter) <| fun n -> Html.text $"Counter = {n}" ] ]
        Html.div [ 
            Html.button [
                class' "btn"
                onClick (fun _ -> dispatch Increment) []
                Html.text "(+)" ] ]
        Html.div [ 
            Html.button [
                class' "btn"
                onClick (fun _ -> dispatch Decrement) []
                Html.text "(-)" ] ]
        Html.div Button.button
                
    ]

app () |> Program.mountElement "sutil-app"
