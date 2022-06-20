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
