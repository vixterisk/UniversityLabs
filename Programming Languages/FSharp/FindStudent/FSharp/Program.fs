open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv =
    let rec findStudent list studentX = 
        match list with
        | (a, b) :: tail when a = studentX -> b + findStudent tail studentX
        | (a, b) :: tail -> findStudent tail studentX
        | [] -> 0
    printfn "%d" (findStudent [("Ivanov", 5); ("Petrov", 2); ("Petrov", 5); ("Sidorov", 4)] "Petrov")
    Console.ReadLine()
    0 // return an integer exit code
