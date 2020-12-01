module FSharp.Dia01.Puzzle1
open System.Collections.Generic

let rec find2 value numbers =
    match numbers with
    | [] -> (-1, -1)
    | x::xs ->
        try (x, List.find (fun v -> x + v = value) xs)
        with :? KeyNotFoundException -> find2 value xs

let puzzle1 filePath =
    System.IO.File.ReadAllLines(filePath)
    |> Seq.map int
    |> List.ofSeq
    |> find2 2020
    |> (fun (v1, v2) ->
        printfn "Dia 1 Puzzle 1: %s" (
            if v1 + v2 = 2020 then
                sprintf "El resultado es %i" (v1 * v2)
            else
                "No se hallaron los numeros :c"))

