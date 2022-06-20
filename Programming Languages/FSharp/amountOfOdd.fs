let rec amountOfOdd (l : int list) k = 
   match l with
   | [] -> k
   | x :: xr -> if x%2=1 then amountOfOdd (xr) k+1
                else  amountOfOdd (xr) k

let convertFractionToPercent numerator denominator =
    if denominator <> 0.00 then
        (numerator / denominator) * 100.00;
    else
        0.00

let length = System.Int32.Parse(System.Console.ReadLine());
let myList = [for i in 1..length -> System.Int32.Parse(System.Console.ReadLine())]
let amount = amountOfOdd myList 0
let result = convertFractionToPercent (float amount) (float length)
printfn "%.2f%%" result
