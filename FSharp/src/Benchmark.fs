module FSharp.Benchmark

open System.Diagnostics

let benchmark f =
    let sw = Stopwatch()
    sw.Start()
    f()
    sw.Stop()
    printfn "Millisegundos: %f" sw.Elapsed.TotalMilliseconds
