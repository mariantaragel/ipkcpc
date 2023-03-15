# IPK Projekt 1
Cie�om projektu je vytvori� klienta ku vzdialenej kalkula�ke, ktor� bude komunikova� pomocou protokolu IPK Calculator Protocol. Program je nap�san� v objekoto orientovanom jazyku C#.

## �trukt�ra projektu
Z h�adiska �trukt�ry je projekt rozdelen� do troch tried a to `Program`, `Udp` a `Tcp`. `Program` je z�kladn� trieda, ktor� obsahuje met�du `Main()`, ktor� je vstupn�m bodom programu. Triedy `Udp` a `Tcp` poskytuj� met�dy pre komunik�ciu pomocou UDP/TCP protokolu. 

## UML Diagram
```mermaid
classDiagram
class Program
    Program : + Main()
class Udp
	Udp : + Host
	Udp : + Port
    Udp : + Communicate()
class Tcp
	Tcp : + Host
	Tcp : + Port
    Tcp : + Communicate()
Program ..> Udp : <<<a>create>>
Program ..> Tcp : <<<a>create>>
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
Testovanie bolo uskuto�nen� automatizovan�mi skriptami `test_cli.py`, `test_udp.py` a `test_tcp.py`. Aplik�cia bola testovan� v poskytnutom virtu�lnom stroji s opera�n�m syst�mom Linux (distrib�cia NixOS 22.11 Raccoon). Po�as testovania aplik�cia komunikovala s refern�n�m serverom, ktor� bol spusten� nasledovn�m pr�kazom: `ipkpd -h 127.0.0.1 -p 2023 -m (udp|tcp)`. Testy boli sp���an� na zariaden� Dell Latitude 5491 (Intel Core i7, 8GB RAM DDR4, 256GB SSD). Testovan� aplik�cia bola vo verzii 1.0.0.

- `test_cli.py` testuje spr�vne spracovanie argumentov pr�kazov�ho riadku
- `test_udp.py` testuje komunik�ciu pomocou protokolu UDP
- `test_tcp.py` testuje komunik�ciu pomocou protokolu TCP

Vystup z testovania:
```
[nix-shell:~/IPK-Projekt-1/src/bin/Debug/net6.0]$ python test_cli.py 
Testing Command-line interface
 Passed ./ipkcpc -h 127.0.0.1 -p 2023 -m udp -x
 Passed ./ipkcpc -p 2023 -m tcp
 Passed ./ipkcpc -h 127.0.0.1 -p 2023 -m sctp
 Passed ./ipkcpc -h 127.0.0.1 -p eighty -m udp
 Passed ./ipkcpc -h google.com -p 2023 -m tcp
 Passed ./ipkcpc
 Passed ./ipkcpc --help

[nix-shell:~/IPK-Projekt-1/src/bin/Debug/net6.0]$ python test_udp.py 
Testing UDP
 Passed (+ 1 2)
 Passed (a b)
 Passed (/ 0 -1) (- -50 60)
 Passed (* 12 12) (/ 10 0) (- c d) (/ 23 11)
 Passed Empty input

[nix-shell:~/IPK-Projekt-1/src/bin/Debug/net6.0]$ python test_tcp.py 
Testing TCP
 Passed HELLO SOLVE (+ 1 2) BYE
 Passed HELLO SOLVE (- 0 -1) SOLVE (/ 42 2) BYE
 Passed HELLO BYE
 Passed Hi
 Passed Empty input
```

## Bibliografia
[1] KUROSE James F. a Keith W. ROSS. <em>Computer networking: a top-down approach</em>. Eighth edition.; Global edition. Harlow: Pearson Education Limited, 2022, ISBN 978-1-292-40546-9.<br/>
[2] DOLEJ�KA Daniel a KOUTENSK� Michal <em>Programov�n� s�ov�ch aplikac�</em>. VUT FIT: NES@FIT, 2023