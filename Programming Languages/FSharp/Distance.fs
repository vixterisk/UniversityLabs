open System
let findTime t1 v1 t2 v2 t3 v3 = 
    let w1 = t1 * v1
    let w2 = t2 * v2
    let w3 = t3 * v3
    let halfWay = (w1 + w2 + w3) / 2.0
    printfn "%f" halfWay
    match halfWay with
    | x when w1 > halfWay -> halfWay / v1
    | x when w1 + w2 > halfWay -> t1 + (halfWay - w1) / v2
    | x when w1 + w2 + w3 > halfWay -> t1 + t2 + (halfWay - w2) / v3
    | _ -> 0.0

let PrintResult =
    let t1 = 3.0
    let v1 = 5.0
    let t2 = 4.0
    let v2 = 6.0
    let t3 = 4.0
    let v3 = 4.0
    printfn "%0.3f" (findTime t1 v1 t2 v2 t3 v3)

PrintResult
let c = Console.ReadKey()
