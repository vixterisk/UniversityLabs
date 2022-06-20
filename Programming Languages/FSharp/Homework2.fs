open System
// задача 2
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
// задача 1
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

// задача 3
open System

let findAllMins k (u, v, w) (s, t, f) = 
    let min1 = (int)(s * 100.0 / (u * (float)k))
    let min2 = (int)(t * 1000.0 / (v * (float)k))
    let min3 = (int)(f * 50.0 / (w * (float)k))
    (min1, min2, min3)

let findMinOfTree (min1, min2, min3) = 
    if min1 < min2 then 
                   if min1 < min3 then min1
                   else min3
    elif min2 < min3 then min2
    else min3

let findMinFood k (u, v, w) (s, t, f) = 
    let min1, min2, min3 = findAllMins k (u, v, w) (s, t, f)
    let min = findMinOfTree(min1, min2, min3)
    if min = min1 then printfn "Сено кончится самым первым"
    elif min = min2 then printfn "Силос кончится самым первым"
    else printfn "Комбикорм кончится самым первым"

let u = 3.0 //кг сена в день
let v = 2.0 //кг силоса в день
let w = 2.0 //кг комбикорма в день
let k = 4 // всего коров
let s = 10.0 //центнеров сена
let t = 30.0 //тонн силоса
let f = 30.0 //мешков комбикорма по 50кг
printfn "Можно кормить стадо по полному рациону %i дней" (findAllMins k (u, v, w) (s, t, f) |> findMinOfTree)
findMinFood k (u, v, w) (s, t, f)
let c = Console.ReadKey()