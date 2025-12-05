let rec fact n=
    if n=0 then 1
    else n*fact(n-1)

let r = fact 3
printfn "%d" r