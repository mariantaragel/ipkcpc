# IPK Projekt 1
Cie�om projektu je vytvori� klienta ku vzdialenej kalkula�ke, ktor� bude komunikova� pomocou protokolu IPK Calculator Protocol. Program je nap�san� v objekoto orientovanom jazyku C#.

## �trukt�ra projektu
Z h�adiska �trukt�ry je projekt rozdelen� do troch tried a to `Program`, `Udp` a `Tcp`. `Program` je z�kladn� trieda, ktor� obsahuje met�du `Main()`, ktor� je vstupn�m bodom programu. Triedy `Udp` a `Tcp` poskytuj� met�dy pre komunik�ciu pomocou UDP/TCP protokolu. 

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
V nasleduj�cej �asti stru�ne zhrniem te�riu nutn� k pochopeniu implementovanej funkcionality. Zameriam sa hlavne na dva protokoly transportnej vrstvy a to UDP a TCP. Vych�dza� budem zo zdrojov [1] a [2].

### UDP
Protokol UDP, definovan� v [RFC 768], poskytuje nespojov� transportn� slu�bu. Prenos d�t pomocou tak�hoto spojenia je nespo�ahliv� a ni� negarantuje. Postup pri komunik�cii pomocou tohto protokolu zo strany klienta pozost�va z nasleduj�cich f�z:
1. Vytvorenie socketu
2. Odoslanie d�t na server
3. �akanie na odpove�
4. Ukon�enie komunik�cie

### TCP
Protokol TCP je spojovo orientovan� transportn� slu�ba. Pred za�at�m komunik�cie mus� prebehn�� medzi serverom a klientom tzv. "handshake" - to znamen�, �e si musia navz�jom posla� �vodn� spr�vy, ktor� nastavia parametre komunik�cie. Protokol TCP je spo�ahliv� a pri ne�spechu doru�enia spr�vy, opakovane posiela d�ta znovu. Pri TCP je postup pri komunik�cii na strane klienta nasledovn�:
1. Vytvorenie socketu
2. Ustanovenie spojenia
3. Odoslanie d�t na server
4. �akanie na odpove�
5. Ukon�enie komunik�cie

## Testovanie

## Bibliografia
[1] KUROSE James F. a Keith W. ROSS. <em>Computer networking: a top-down approach</em>. Eighth edition.; Global edition. Harlow: Pearson Education Limited, 2022, ISBN 978-1-292-40546-9.<br/>
[2] DOLEJ�KA Daniel a KOUTENSK� Michal <em>Programov�n� s�ov�ch aplikac�</em>. VUT FIT: NES@FIT, 2023