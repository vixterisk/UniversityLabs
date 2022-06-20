open System
open System.Linq
open System.IO

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
