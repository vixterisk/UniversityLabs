open System
open System.Linq
open System.IO

//2 задача

let mult x = 
    (x % 10) * (x / 10 % 10) * (x / 10 /10 % 10) * (x / 10 / 10 / 10% 10)

Console.ReadLine() |> Int32.Parse |> mult |> printfn "%i"
//3 задача

let inchesTrans len = 
    let mm = len * 25.4
    let cm = (mm - mm % 10.0) / 10.0
    let m = (cm - cm % 100.0) / 100.0
    (mm, cm, m)

let mm, cm, m = inchesTrans (Double.Parse( Console.ReadLine()))
printfn "its %.3f milimeters, %.0f centimeters, %.0f meters" mm cm m 
let z = Console.ReadKey()





//4 задача

let length start finish =
   let lenInSec time (h, m, s) = h * 60 * 60 + m * 60 + s
   let ans = lenInSec start - lenInSec finish
   (ans / 60 / 60, ans / 60 % 60, ans % 60)









let PrintResult = 
    let input = Console.ReadLine().Split()
    let start = (Int32.Parse(input.[0]), Int32.Parse(input.[1]), Int32.Parse( input.[2]))
    let input2 = Console.ReadLine().Split()
    let finish =  (Int32.Parse(input2.[0]), Int32.Parse(input2.[1]), Int32.Parse( input2.[2]))
    printfn "%A" (length start finish)
    Console.ReadKey()

PrintResult

//5 задача

let sum p0 p1 p2 p3 = 
    let first = p0 + p1 / 3.0
    let second = p0 + p1 / 3.0 + (p2 - p1) / 2.0
    let third = p0 + p1 / 3.0 + (p2 - p1) / 2.0 + p3 - p2
    (first, second, third)

printfn "%A" (sum (Double.Parse(Console.ReadLine())) (Double.Parse(Console.ReadLine())) (Double.Parse(Console.ReadLine()) (Double.Parse(Console.ReadLine()) )
Console.ReadKey()