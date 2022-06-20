// задача 1
open System

let rec CalcDigits n =
    match n with
    | n when n = 0 -> 0
    | n -> 1 + CalcDigits (n/10) 

let findDigitQuan n =
    if n = 0 then 1 else (CalcDigits n)

printfn "%i" (findDigitQuan 0)
Console.ReadKey()

// задача 2
open System 

let rec NOD a b =
    match a, b with
    | a, b when a = 0 || b = 0 -> (a + b)
    | a, b when a > b -> NOD (a%b) b
    | a, b -> NOD a (b%a)

printfn "%i" (NOD 18 30)
Console.ReadKey()

// задача 3
open System

let rec multFact n k = 
    match k with
    | k when k = (2*n-1) -> k
    | k -> k * multFact n (k+2)

printfn "%i" (multFact 4 1)
// задача 4
open System

let rec Fact n k = 
    match k with
    | k when k = n -> k
    | k -> k * Fact n (k+1)
    
let rec findSum n k = 
    match k with
    | k when k = n -> Fact k 1
    | k -> Fact k 1 + findSum n (k+1)

printfn "%i" (findSum 4 1)

// задача 5
open System

let rec Fact n k = 
    match k with
    | k when k = n -> k
    | k -> k * Fact n (k+1)
    
let rec findSum n k = 
    match k with
    | k when k = n -> Fact k 1
    | k -> Fact k 1 + findSum n (k+2)

printfn "%i" (findSum 9 1)
//6 задача

let rec Trans n = 
    
    match n with
    
    | n when n < 2 -> n
    
    | n -> n % 2 + 10 * Trans (n / 2)