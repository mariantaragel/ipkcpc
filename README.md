# IPK Projekt 1
Cie�om projektu je vytvori� klienta ku vzdialenej kalkula�ke, ktor� bude komunikova� pomocou protokolu IPK Calculator Protocol.

## �trukt�ra projektu
Projekt je rozdelen� do troch tried a to `Program`, `Udp` a `Tcp`. `Program` je z�kladn� trieda, ktor� obsahuje met�du `Main()`, ktor� je vstupn�m bodom programu. Triedy `Udp` a `Tcp` poskytuj� met�dy pre komunik�ciu pomocou UDP/TCP protokolu. 

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

## Te�ria
V nasleduj�cej �asti stru�ne zhrniem te�riu nutn� k pochopeniu funkcionality implementovanej aplik�cie. Zameriam sa hlavne na na dva protokoly transportnej vrstvy a to UDP a TCP. Vych�dza� budem z nasleduj�cich zdrojov [1], [2]. a [3]

### UDP


### TCP

## Testovanie

## Bibliografia
[1] KUROSE, James F. a Keith W. ROSS. <em>Computer networking: a top-down approach</em>. Eighth edition.; Global edition. Harlow: Pearson Education Limited, 2022, ISBN 978-1-292-40546-9.<br/>
[2]<br/>
[3]