# IPK Projekt 1
Cie¾om projektu je vytvori klienta ku vzdialenej kalkulaèke, ktorı bude komunikova pomocou protokolu IPK Calculator Protocol. Program je napísanı v objekoto orientovanom jazyku C#.

## Štruktúra projektu
Z h¾adiska štruktúry je projekt rozdelenı do troch tried a to `Program`, `Udp` a `Tcp`. `Program` je základná trieda, ktorá obsahuje metódu `Main()`, ktorá je vstupnım bodom programu. Triedy `Udp` a `Tcp` poskytujú metódy pre komunikáciu pomocou UDP/TCP protokolu. 

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
V nasledujúcej èasti struène zhrniem teóriu nutnú k pochopeniu implementovanej funkcionality. Zameriam sa hlavne na dva protokoly transportnej vrstvy a to UDP a TCP. Vychádza budem zo zdrojov [1] a [2].

### UDP
Protokol UDP, definovanı v [RFC 768], poskytuje nespojovú transportnú slubu. Prenos dát pomocou takéhoto spojenia je nespo¾ahlivı a niè negarantuje. Postup pri komunikácii pomocou tohto protokolu zo strany klienta pozostáva z nasledujúcich fáz:
1. Vytvorenie socketu
2. Odoslanie dát na server
3. Èakanie na odpoveï
4. Ukonèenie komunikácie

### TCP
Protokol TCP je spojovo orientovaná transportná sluba. Pred zaèatím komunikácie musí prebehnú medzi serverom a klientom tzv. "handshake" - to znamená, e si musia navzájom posla úvodné správy, ktoré nastavia parametre komunikácie. Protokol TCP je spo¾ahlivı a pri neúspechu doruèenia správy, opakovane posiela dáta znovu. Pri TCP je postup pri komunikácii na strane klienta nasledovnı:
1. Vytvorenie socketu
2. Ustanovenie spojenia
3. Odoslanie dát na server
4. Èakanie na odpoveï
5. Ukonèenie komunikácie

## Testovanie

## Bibliografia
[1] KUROSE James F. a Keith W. ROSS. <em>Computer networking: a top-down approach</em>. Eighth edition.; Global edition. Harlow: Pearson Education Limited, 2022, ISBN 978-1-292-40546-9.<br/>
[2] DOLEJŠKA Daniel a KOUTENSKİ Michal <em>Programování síovıch aplikací</em>. VUT FIT: NES@FIT, 2023