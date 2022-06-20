//задача 4
let rec SumLists list1 list2 = 
    match list1, list2 with
    | list1, [] -> list1
    | [], list2 -> list2
    | head::tail, head2::tail2 -> (head + head2) :: SumLists tail tail2

//задача 5
let rec FindSuit list suit =
    match list with
    | [] -> 0
    | head::tail when fst head = suit -> 1 + FindSuit tail suit
    | head::tail -> FindSuit tail suit

printfn "%i" (FindSuit [("черви", "валет"); ("черви", "дама"); ("пики", "6"); ("крести", "10"); ("черви", "7")] "пики")
//задача 6
let rec IsVerticalCorrect list vertical figure =
    match list with
    | [] -> false
    | head::tail -> let fig, hor, vert = head
                    if fig = figure then 
                        if vert = vertical then true
                        else false
                    else IsVerticalCorrect tail vertical figure