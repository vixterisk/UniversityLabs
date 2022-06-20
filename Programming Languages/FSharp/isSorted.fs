let rec isSorted (l: int list) = 
    match l with
    | [] -> true
    | [x] -> true
    | x::y::xr -> if x > y then false else isSorted(y :: xr);

let n = System.Int32.Parse(System.Console.ReadLine());
let myList = [for i in 1..n -> System.Int32.Parse(System.Console.ReadLine())]
let result =  isSorted myList
printfn "%b" result
