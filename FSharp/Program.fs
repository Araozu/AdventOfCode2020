open FSharp
open FSharp.Benchmark
open FSharp.Dia01

let getFilePath (day: int) =
    let day' =
        string day
        |> (fun s -> if s.Length = 1 then "0" + s else s)

    sprintf "/home/araozu/Programacion/AdventOfCode2020/inputs/input_%s.txt" day'

let getLines day =
    System.IO.File.ReadAllLines(getFilePath day)

[<EntryPoint>]
let main _ =

    let lines = getLines 3
    
    // benchmark (fun () -> getFilePath 1 1 |> Puzzle1.puzzle1)
    // benchmark (fun () -> getFilePath 1 1 |> Puzzle2.puzzle2)

    // benchmark (fun () -> getFilePath 2 1 |> Dia02.puzzle)
    benchmark (fun () -> Dia03.puzzle lines)
    
    0
