module Button

open Sutil
open Fable.Core.JsInterop

open Sutil
open Sutil.Styling
open Sutil.Core
open Sutil.CoreElements
open Sutil.DomHelpers

let button = [
    Html.div [ 
        Attr.className "bg-red-500"
        text "Hello world"]
]