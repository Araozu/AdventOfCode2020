open FSharp.Dia01

let getFilePath (day: int) number =
    let day' =
        string day
        |> (fun s -> if s.Length = 1 then "0" + s else s)

    sprintf "/home/araozu/Programacion/AdventOfCode2020/inputs/input_%s_%i.txt" day' number

[<EntryPoint>]
let main _ =
    let s = System.Diagnostics.Stopwatch()
    let s2 = System.Diagnostics.Stopwatch()
    
    s2.Start()
    getFilePath 1 1
    |> Puzzle1.puzzle1
    s2.Stop()
    
    printfn "micro: %f" s2.Elapsed.TotalMilliseconds
    
    s.Start()
    getFilePath 1 1
    |> Puzzle2.puzzle2
    s.Stop()
    
    printfn "micro: %f" s.Elapsed.TotalMilliseconds
    
    0
