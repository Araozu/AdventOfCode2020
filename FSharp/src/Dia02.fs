module FSharp.Dia02

open System.Text.RegularExpressions

let private matchPattern input =
    let m = Regex.Match(input, "(\d+)-(\d+) (\w): (.+)")
    if m.Success then Some (List.tail [for g in m.Groups -> g.Value])
    else None

let private extract =
    function
    | Some (min::max::c::password::_) -> Some (int min, int max, char c, password)
    | _ -> None

let private count (s: string) c =
    s.ToCharArray()
    |> Seq.fold (fun acc c2 -> if c2 = c then acc + 1 else acc) 0

let private validateAll f data : int option =
    let innerValidate =
        function
        | Some x -> f x
        | None -> false        
    
    List.filter innerValidate data
    |> List.length
    |> Some

let private validate1 (min, max, c, password) =
    let charCount = count password c
    charCount >= min && charCount <= max

let private validate2 (min, max, c, password: string) =
    let c1 = password.[min - 1]
    let c2 = password.[max - 1]
    c1 <> c2 && (c1 = c || c2 = c)

let puzzle filePath =
    let printSome s =
        function
        | Some x -> printfn "Hay %i contraseñas validas con la %s heuristica" x s
        | None -> printfn "No hay contraseñas validas"

    let data =
        System.IO.File.ReadLines(filePath)
        |> List.ofSeq
        |> List.map (matchPattern >> extract)
    
    validateAll validate1 data |> printSome "1ra"
    validateAll validate2 data |> printSome "2da"
