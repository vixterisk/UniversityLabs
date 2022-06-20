open System

let CalcPay energy =
    let k1 = 10.0
    let k2 = 20.0
    let k3 = 30.0
    match energy with
    | x when x < 500.0 -> 500.0 * k1
    | x when x < 1000.0 -> 500.0 * k1 + (x - 500.0) * k2
    | x ->  500.0 * k1 + 500.0 * k2 + (x - 1000.0) * k3

let energy = Console.ReadLine() |> Double.Parse
printfn "%.2f" (CalcPay energy)
let c = Console.ReadKey()
