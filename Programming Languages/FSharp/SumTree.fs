type BinTree = 
    | Node of int*BinTree*BinTree
    | Empty

let myTree = Node(1, 
                  Node(0, 
                       Empty, 
                       Empty), 
                  Node(2,
                       Empty,
                       Node(3,
                           Empty,
                           Empty)))

let rec SumTree tree = 
    match tree with
    | Empty -> 0
    | Node(x, left, right) -> x + SumTree left + SumTree right

printfn "%i" (SumTree myTree)
