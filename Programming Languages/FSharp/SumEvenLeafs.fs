type BinTree = 
    | Node of int*BinTree*BinTree
    | Empty

let myTree = Node(5, 
                  Node(2, 
                       Empty, 
                       Empty), 
                  Node(6,
                       Empty,
                       Node(7,
                           Empty,
                           Empty)))

let rec SumTree tree = 
    match tree with
    | Empty -> 0
    | Node(x, left, right) when (x%2=0) && left = Empty && right = Empty -> x
    | Node(x, left, right) -> SumTree left + SumTree right

printfn "%i" (SumTree myTree)
