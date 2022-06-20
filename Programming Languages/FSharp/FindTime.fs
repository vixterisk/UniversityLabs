
let findTime t1 v1 t2 v2 t3 v3 = 
    let w1 = t1 * v1
    let w2 = t2 * v2
    let w3 = t3 * v3
    let halfWay = (w1 + w2 + w3) / 2.0
    match halfWay with
    | x when w1 > halfWay -> halfWay / v1
    | x when w1 + w2 > halfWay -> t1 + (halfWay - w2) / v2
    | x when w1 + w2 + w3 > halfWay -> t1 + t2 + (halfWay - w3) / v3
    | _ -> 0.0

let PrintResult =
    let t1 = Double.Parse(Console.ReadLine())
    let v1 = Double.Parse(Console.ReadLine())
    let t2 = Double.Parse(Console.ReadLine())
    let v2 = Double.Parse(Console.ReadLine())
    let t3 = Double.Parse(Console.ReadLine())
    let v3 = Double.Parse(Console.ReadLine())
    printfn "%f" (findTime t1 v1 t2 v2 t3 v3)

PrintResult
let c = Console.ReadKey()

3 5 4 6 4 4 5.8
15
24
16
27,5