module TranslationStore

open Sutil
open Sutil.Styling
open Sutil.Core
open Sutil.CoreElements
open Sutil.DomHelpers

type Model = { input: string; output: string option }

type Msg =
    | SetText of text:string
    | Translate of text:string
    | ClearText

let init () : Model * Cmd<Msg> =
    let model = { input = ""; output = None }
    let cmd = Cmd.none
    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    match msg with
    | SetText(text) -> { model with input = text}, Cmd.ofMsg(Translate(text))
    | Translate(text) -> {model with output = Some (text.ToLower()) }, Cmd.none
    | ClearText -> { model with input = ""; output = None }, Cmd.none