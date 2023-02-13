module Translator.Api.App

open Giraffe
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open NynorskTranslator

let translate (next:HttpFunc) (context:HttpContext) =
    task {
        let! body = context.ReadBodyFromRequestAsync()
        let translatedString = Translator.translate body
        return! text translatedString next context
    }

let webApp =
    choose [
        route "/" >=> text "Nynorsk translator"
        route "/" >=> POST >=> translate
    ]

[<EntryPoint>]
let Main args = 
    let builder = WebApplication.CreateBuilder(args)
    builder.Services.AddGiraffe() |> ignore
    let app = builder.Build()
    app.UseGiraffe(webApp);
    app.Run();
    0
