# IPK Projekt 1
Cie¾om projektu je vytvori klienta ku vzdialenej kalkulaèke, ktorı bude komunikova pomocou protokolu IPK Calculator Protocol.

## Štruktúra projektu
Projekt je rozdelenı do troch tried a to `Program`, `Udp` a `Tcp`. `Program` je základná trieda, ktorá obsahuje metódu `Main()`, ktorá je vstupnım bodom programu. Triedy `Udp` a `Tcp` poskytujú metódy pre komunikáciu pomocou UDP/TCP protokolu. 

## UML Diagram
```c
			 +-----------+
			 |  Program  |
			 |-----------|
			 |-----------|
			 | + Main()  |
			 +-----------+

+-----------------+ +-----------------+
|       Udp       | |       Tcp       |
|-----------------| |-----------------|
| + Host          | | + Host          |
| + Port          | | + Port          |
|-----------------| |-----------------|
| + Communicate() | | + Communicate() |
+-----------------+ +-----------------+
```

## Teória
V nasledujúcej èasti struène zhrniem teóriu nutnú k pochopeniu funkcionality implementovanej aplikácie. Zameriam sa hlavne na na dva protokoly transportnej vrstvy a to UDP a TCP. Vychádza budem z nasledujúcich zdrojov [1], [2]. a [3]

### UDP


### TCP

## Testovanie

## Bibliografia
[1] KUROSE, James F. a Keith W. ROSS. <em>Computer networking: a top-down approach</em>. Eighth edition.; Global edition. Harlow: Pearson Education Limited, 2022, ISBN 978-1-292-40546-9.<br/>
[2]<br/>
[3]