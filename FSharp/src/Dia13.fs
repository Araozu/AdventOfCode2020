module FSharp.Dia13

let getModulus estimate v =
    let value1 = estimate % v
    if value1 = 0 then 0
    else v - (estimate % v) 

let puzzle (data: string []) =
    
    let estimate = int data.[0]

    let getMin x =
        let x' = int x
        (getModulus estimate x', x')
    
    let (times, id) =
        data.[1].Split(",")
        |> Seq.filter (fun x -> x <> "x")
        |> Seq.map getMin
        |> Seq.min
    
    printfn "The result is %i" (times * id) 

let rec testRules (rules: (uint64 * int) list) (value1: uint64) (offset1: int) =
    match rules with
    | (value2, offset2)::rules' ->
        let offsetDiference = uint64 (offset2 - offset1)
        let newValue = value1 + offsetDiference
        if newValue % value2 = 0UL then testRules rules' value1 offset1
        else false
    | [] -> true    

let rec testValues acc (rules: (uint64 * int) list) =
    let (value, offset)::rules' = rules

    let next = acc + value
    if testRules rules' next offset then next
    else testValues (acc + value) rules

let puzzle2 (data: string[]) =
    let data: (uint64 * int) list =
        data.[1].Split(",")
        |> Seq.mapi (fun i x -> (x, i))
        |> Seq.filter (fun (x, _) -> x <> "x")
        |> Seq.map (fun (x, v) -> (uint64 x, v))
        |> Seq.sortDescending
        |> Seq.toList
       
    let value = testValues 0UL data
    let (_, offset)::_ = data

    printfn "The earliest timestamp is %A" (value - (uint64 offset))
