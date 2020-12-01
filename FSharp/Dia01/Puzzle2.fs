module FSharp.Dia01.Puzzle2
open FSharp.Dia01.Puzzle1

let rec internal find3 value numbers =
    match numbers with
    | x::xs ->
        let resto = value - x
        match find2 resto xs with
        | (-1, -1) ->
            find3 value xs
        | (v2, v3) ->
            (x, v2, v3)
        
    | [] -> (-1, -1, -1)

let puzzle2 filePath =
    System.IO.File.ReadAllLines(filePath)
    |> Seq.map int
    |> List.ofSeq
    |> find3 2020
    |> (fun (v1, v2, v3) ->
        printfn "Dia 1 Puzzle 2: %s" (
            if v1 + v2 + v3 = 2020 then
                sprintf "El resultado es %i" (v1 * v2 * v3)
            else
                "No se hallaron los numeros :c"))

