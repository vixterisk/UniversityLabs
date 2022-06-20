let SquaresInRange first last = 
    [
        for i in first..last do
            yield i * i
    ]
let first = System.Int32.Parse(System.Console.ReadLine());
let last = System.Int32.Parse(System.Console.ReadLine());
let squares = SquaresInRange first last
printfn "%A" squares
