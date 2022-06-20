open System
open System.Linq
open System.IO

let inchesTrans len = 
    let mm = len * 25.4
    let cm = (mm - mm % 10.0) / 10.0
    let m = (cm - cm % 100.0) / 100.0
    (mm, cm, m)

let mm, cm, m = inchesTrans (Double.Parse( Console.ReadLine()))
printfn "its %.3f milimeters, %.0f centimeters, %.0f meters" mm cm m 
let z = Console.ReadKey()
