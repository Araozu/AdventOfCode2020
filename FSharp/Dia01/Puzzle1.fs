module FSharp.Dia01.Puzzle1
open System.Collections.Generic

let puzzle1 filePath =
    let rec find numbers =
        match numbers with
        | x::xs ->
            try (x, List.find (fun v -> x + v = 2020) xs)
            with :? KeyNotFoundException -> find xs
        | [] -> (-1, -1)
    
    System.IO.File.ReadAllLines(filePath)
    |> Seq.map int
    |> List.ofSeq
    |> find
    |> (fun (v1, v2) ->
        if v1 + v2 = 2020 then
            printfn "El resultado es %i" (v1 * v2)
        else
            printfn "No se hallaron los numeros :c")

