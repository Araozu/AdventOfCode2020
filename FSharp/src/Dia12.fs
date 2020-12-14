module FSharp.Dia12

type Instruction =
    | N of int
    | S of int
    | E of int
    | W of int
    | L of int
    | R of int
    | F of int

type Direction =
    | North
    | South
    | East
    | West

let parseInstructions (line: string) =
    let instructionConstructor =
        match line.[0] with
        | 'N' -> N
        | 'S' -> S
        | 'E' -> E
        | 'W' -> W
        | 'L' -> L
        | 'R' -> R
        | 'F' -> F
        | _ -> failwithf "Bad instruction: %c" line.[0]
        
    let value = int (line.Substring(1))
    instructionConstructor value

let turnLeft90 direction =
    match direction with
    | North -> West
    | West -> South
    | South -> East
    | East -> North

let turnRight90 direction =
    match direction with
    | North -> East
    | East -> South
    | South -> West
    | West -> North

let turnOpposite direction =
    match direction with
    | North -> South
    | South -> North
    | East -> West
    | West -> East

let turnLeft direction degrees =
    match degrees % 360 with
    | 0 -> direction
    | 90 -> turnLeft90 direction
    | 180 -> turnOpposite direction
    | 270 -> turnRight90 direction
    | _ -> failwithf "Bad rotation degree: %i" degrees

let turnRight direction degrees =
    match degrees % 360 with
    | 0 -> direction
    | 90 -> turnRight90 direction
    | 180 -> turnOpposite direction
    | 270 -> turnLeft90 direction
    | _ -> failwithf "Bad rotation degree: %i" degrees

let rec execIns inst position =
    let (direction, north, east) = position
    match inst with
    | N i -> 
        (direction, north + i, east)
    | S i -> 
        (direction, north - i, east)
    | E i -> 
        (direction, north, east + i)
    | W i -> 
        (direction, north, east - i)
    | L i -> 
        (turnLeft direction i, north, east)
    | R i ->
        (turnRight direction i, north, east)
    | F i ->
        match direction with
        | North -> execIns (N i) position
        | South -> execIns (S i) position
        | East -> execIns (E i) position
        | West -> execIns (W i) position

let puzzle data =
    let foldf acc ins = execIns ins acc
    
    let instructions = Seq.map parseInstructions data
    // Direction, North, East
    let position = (East, 0, 0)
    
    let (_, north, east) = Seq.fold foldf position instructions
    
    printfn "Result is: %i" ((abs north) + (abs east))

let turnWaypointOppossite (north, east) = (-north, -east)

let turnWaypointRight (north, east) = (-east, north)

let turnWaypointLeft (north, east) = (east, -north)

let rec execIns2 inst pos waypoint =
    let (north, east) = waypoint
    match inst with
    | N i -> 
        let waypoint' = (north + i, east)
        (pos, waypoint')
    | S i -> 
        let waypoint' = (north - i, east)
        (pos, waypoint')
    | E i -> 
        let waypoint' = (north, east + i)
        (pos, waypoint')
    | W i -> 
        let waypoint' = (north, east - i)
        (pos, waypoint')
    | L i ->
        let waypoint' =
            match i % 360 with
            | 0 -> waypoint
            | 90 -> turnWaypointLeft waypoint
            | 180 -> turnWaypointLeft <| turnWaypointLeft waypoint
            | 270 -> turnWaypointRight waypoint
            | _ -> failwithf "Bad rotation degree: %i" i
        (pos, waypoint')
    | R i ->
        let waypoint' =
            match i % 360 with
            | 0 -> waypoint
            | 90 -> turnWaypointRight waypoint
            | 180 -> turnWaypointLeft <| turnWaypointLeft waypoint
            | 270 -> turnWaypointLeft waypoint
            | _ -> failwithf "Bad rotation degree: %i" i
        (pos, waypoint')
    | F i ->
        let (north', east') = pos
        let pos' = (north' + i * north, east' + i * east)
        (pos', waypoint)

let puzzle2 data =
    let foldf (shipPos, waypoint) ins = execIns2 ins shipPos waypoint 
    
    let instructions = Seq.map parseInstructions data
    let shipPos = (0, 0)
    let waypoint = (1, 10)
    
    let ((north, east), _) = Seq.fold foldf (shipPos, waypoint) instructions
    
    printfn "Result is: %i" ((abs north) + (abs east))
