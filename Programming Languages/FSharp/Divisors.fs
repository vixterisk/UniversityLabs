let amountOfDivisors n = 
    let DivisorOfN = 
        [
          for i in 1..n do
              if n % i = 0 then yield i
        ]
    List.length DivisorOfN
let number = System.Int32.Parse(System.Console.ReadLine());
let amount = amountOfDivisors number
printfn "%i has %i divisors" number amount
