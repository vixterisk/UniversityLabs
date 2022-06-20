let rec SumLists list1 list2 = 
    match list1, list2 with
    | list1, [] ->  list1
    | [], list2 -> list2
    | head::tail, head2::tail2 -> (head + head2) :: SumLists tail tail2
    
printfn "%A" (SumLists [1; 2; 3] [2])