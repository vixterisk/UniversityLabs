--Самодвойственная задача
isSelfdual mylist = checkIfSelfDual mylist True where
    checkIfSelfDual [] flag = flag
    checkIfSelfDual mylist flag =
        if (head mylist) /= (last mylist) then checkIfSelfDual (init (tail mylist)) flag
        else False

--Месяцы с праздниками

import Data.List
        
findMaxMonths ((d,m):t) = 
    nub (map (\(d,m) -> m) (filter (\(d,month) -> maxMonths ((d,m):t) == curMonth month ((d,m):t)) ((d,m):t))) where
    curMonth month ((d,m):t) = 
        length (filter (\(d,m) -> m==month) ((d,m):t))  
    maxMonths ((d,m):t) = maxMonths ((d,m):t) 0 where
        maxMonths [] curMax = curMax
        maxMonths ((d,m):t) curMax =
            if (curMonth m ((d,m):t)) > curMax then maxMonths t (curMonth m ((d,m):t))
            else maxMonths t curMax

--Подстрока
isSubstr str substr = isSubstr str substr substr where
    isSubstr str substr [] = True
    isSubstr [] substr substrLeft = False
    isSubstr str substr substrLeft =
        if (head str) == (head substrLeft) then isSubstr (tail str) substr (tail substrLeft)
        else isSubstr (tail str) substr substr

--Считалочка
countFifth list = countFifth list list 1 (head list) where
    countFifth [] reducingList curNum lastName = lastName
    countFifth list reducingList curNum lastName =
        if curNum == 5 then
            if length reducingList == 0 then countFifth list list curNum lastName
            else countFifth (delete (head reducingList) list) (tail reducingList) 1 (head reducingList)
        else 
            if length reducingList == 0 then countFifth list list curNum lastName
            else countFifth list (tail reducingList) (curNum+1) (head reducingList)