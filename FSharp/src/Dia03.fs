module FSharp.Dia03

let private getTreeAmount data right down =
    // Use a tuple to represent: (currentTreeCount, rightPosition, lineSkips)
    let foldFn (treeCount, rightPosition, lineSkips) (line: string) =
        if lineSkips > 0 then (treeCount, rightPosition, lineSkips - 1)
        else
            let newRightPosition = (rightPosition + right) % line.Length
            let newTreeCount = 
                if line.[rightPosition] = '#' then treeCount + 1
                else treeCount
    
            (newTreeCount, newRightPosition, down - 1)

        
    Seq.fold foldFn (0, 0, down - 1) data


let puzzle filePath =
    let data = System.IO.File.ReadAllLines filePath
    
    let (treeAmount1, _, _) = getTreeAmount data 1 1
    let (treeAmount2, _, _) = getTreeAmount data 3 1
    let (treeAmount3, _, _) = getTreeAmount data 5 1
    let (treeAmount4, _, _) = getTreeAmount data 7 1
    let (treeAmount5, _, _) = getTreeAmount data 1 2
    
    printfn "First tree amount: %i" treeAmount2
    let totalTreeAmount = treeAmount1 * treeAmount2 * treeAmount3 * treeAmount4 * treeAmount5
    printfn "Total tree amount: %i" totalTreeAmount
