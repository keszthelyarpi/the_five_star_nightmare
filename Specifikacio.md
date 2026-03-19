# The Five Star Nightmare

**Széchenyi István Egyetem**  
Gépészmérnöki, Informatikai és Villamosmérnöki Kar  
Informatika Tanszék  

**Modern szoftverfejlesztési eszközök (GKNB_INTM129)**  
**Féléves feladat**

**Készítők:** Zsíros Martin (XO6M7X), Keszthelyi Árpád (I4KEBG), Molnár Bálint Ottó (VP35W1)  
**Képzés:** Mérnökinformatikus – alapképzés (BSc)

---

## Tartalomjegyzék

- [1. Bevezetés](#1-bevezetés)
- [2. Szinopszis](#2-szinopszis)
- [3. A játék menete](#3-a-játék-menete)
  - [3.1 A Központi Hub és a küldetésfelvétel](#31-a-központi-hub-és-a-küldetésfelvétel)
  - [3.2 A szintek (pályák) teljesítése](#32-a-szintek-pályák-teljesítése)
  - [3.3 Pályatervezési (Level Design) Mátrix](#33-pályatervezési-level-design-mátrix)
- [4. Grafikai megjelenés és Felhasználói felület (UI)](#4-grafikai-megjelenés-és-felhasználói-felület-ui)
  - [4.1 Főmenü (Main Menu)](#41-főmenü-main-menu)
  - [4.2 Töltőképernyő (Loading Screen)](#42-töltőképernyő-loading-screen)
  - [4.3 Játékmenet (Gameplay) és Felhasználói felület (HUD)](#43-játékmenet-gameplay-és-felhasználói-felület-hud)
- [5. A játék működés követelményei](#5-a-játék-működés-követelményei)
- [6. Használati esetek (Use cases)](#6-használati-esetek-use-cases)
  - [6.1 Használati eset (Use Case) diagram leírása](#61-használati-eset-use-case-diagram-leírása)
- [7. Viselkedés](#7-viselkedés)
  - [7.1 Aktivitás diagram leírás](#71-aktivitás-diagram-leírás)
  - [7.2 Állapot diagram leírás](#72-állapot-diagram-leírás)
  - [7.3 Osztálydiagram leírás](#73-osztálydiagram-leírás)
- [8. Technikai megvalósítás és Mechanikák](#8-technikai-megvalósítás-és-mechanikák)
  - [8.1 Mozgási mechanikák és Állapotgép](#81-mozgási-mechanikák-és-állapotgép-movement-logic--state-machine)
  - [8.2 Pályaszerkezet, Grid és mozgó platformok logikája](#82-pályaszerkezet-grid-és-mozgó-platformok-logikája)
  - [8.3 Ellenséges Mesterséges Intelligencia (AI) és Rendszerkapcsolatok](#83-ellenséges-mesterséges-intelligencia-ai-és-rendszerkapcsolatok)
- [9. Rendszerarchitektúra és Unity Objektumok](#9-rendszerarchitektúra-és-unity-objektumok)
  - [9.1 Főbb Játékelemek (GameObjects) és Inspector beállításaik](#91-főbb-játékelemek-gameobjects-és-inspector-beállításaik)
  - [9.2 Rendszervezérlők és Felhasználói Felület (Managers & UI)](#92-rendszervezérlők-és-felhasználói-felület-managers--ui)
  - [9.3 Címkék (Tags) és Rétegek (Layers) Rendszere](#93-címkék-tags-és-rétegek-layers-rendszere)
- [10. Tesztelés](#10-tesztelés)
  - [10.1 Funkcionális és Mechanikai tesztelés](#101-funkcionális-és-mechanikai-tesztelés)
  - [10.2 Pályatervezési és Egyensúlyi tesztelés](#102-pályatervezési-és-egyensúlyi-tesztelés-level-design-testing)
- [11. Fejlesztési eszközök](#11-fejlesztési-eszközök)
  - [11.1 Projektmenedzsment és Feladatütemezés](#111-projektmenedzsment-és-feladatütemezés)

---

## Ábrajegyzék

1. A rendszer használati eset diagramja (Use Case Diagram)
2. A játékmenet logikai folyamata (Activity Diagram)
3. A karakter mozgási állapotgépe (Player State Machine Diagram)
4. A szoftver statikus architektúrája (Class Diagram)
5. Agilis feladatkezelés a GitHub Project Board felületén

## Táblázatjegyzék

1. Az egyes szintek narratív és technikai tervezési mátrixa
2. Funkcionális és nem-funkcionális rendszerkövetelmények listája
3. A játék fő használati eseteinek összefoglalása

## 1. Bevezetés

Ez a specifikáció egy 2D-s platformer és feladatkezelő játékhoz készült. A játékban a hotel területén mozogva a morcos vendégek kiszolgálása, valamint szeszélyes, váratlan csavarokat tartogató kéréseik teljesítése a cél. A feladatok kreatív megoldásához hétköznapi képességek (futás, ugrás, csúszás) használatára, illetve a környezetben található tárgyakkal való interakcióra van szükség, miközben a továbbjutást trükkös pályaelemek, környezeti akadályok és különböző ellenfelek nehezítik. A program a felhasználótól mozgásra és interakcióra vonatkozó parancsokat vár, amelyeket valós idejű eseményként jelenít meg.

## 2. Szinopszis

A cselekmény előrehaladását és a nehézségi szint növekedését a következő főbb küldetések határozzák meg:

- **1. Szint – A Pince (The Boiler Room):** A történet a földszinten indul, ahol a recepció közelében várakozó, fázó Mr. Didereg panasza indítja el az első küldetést. A fűtés megjavításához a sötét, csöpögő csövekkel teli, indusztriális pincébe kell lejutni. A szelepkerék megtalálását és a fő kazán elindítását megnehezíti, hogy a területen patkányok járőröznek, és időzített gőzsugarakat kell kikerülni.
- **2. Szint – A Liftakna (The Shaft):** A következő kihívást Madame Pompás jelenti, aki rengeteg bőröndjével türelmetlenkedik a földszinti liftajtó előtt. Mivel a lift beszorult két emelet közé, a feladat a biztosítékdoboz elérése és az áram újraindítása. Ehhez egy vertikális pályán, az olajos drótkötelekkel teli liftaknában kell felmászni, miközben pókok és szikrázó elektromos kábelek akadályozzák a továbbjutást.
- **3. Szint – A Padlás (The Attic):** A legfelső emeleten a katonai ruhás Tábornok követeli az elment tévéadás azonnali visszaállítását. A probléma megoldásához a poros, korhadt fagerendákkal teli padláson keresztül vezet az út egészen a tetőgerincig, ahol az antennát kell beállítani. Ezen a szakaszon már repülő denevérekkel és pókokkal kell megküzdeni.

A történet során az egyszerű londineri feladatok a trükkös pályaelemek és az egyre veszélyesebb ellenfelek miatt komplex kihívásokká alakulnak át. Ezek leküzdése kizárólag a hétköznapi képességek (futás, ugrás, csúszás) és a környezeti tárgyak kreatív interakciójának segítségével lehetséges.

## 3. A játék menete

A játékmenet egy ismétlődő ciklusra (loop) épül, amely a Központi Hub (lépcsőház) és a belőle nyíló 2D-s platformer szintek (pályák) között zajlik. A tevékenységek ismétlődő, lineárisan nehezedő sorozatának elvégzésével lehet a küldetések céljait elérni és a történet vonalán haladni. A játékos számára elérhető fő tevékenységek a következők: a Központi Hubban várakozó NPC-vel (morcos vendéggel) való interakció, a küldetés elfogadása, az adott feladathoz tartozó pálya bejáratának aktiválása, a 2D-s szinteken történő platformer mozgás (futás, ugrás, mászás), az ellenfelek és akadályok kikerülése, valamint a céltárgy (pl. szelepkerék) megtalálása és aktiválása.

Ezek a tevékenységek logikusan és szigorú sorrendben következnek egymásból. Például, ha a játékos a Hubban interakcióba lép a soron lévő NPC-vel, a párbeszéd után megkapja a feladatot, aminek hatására megnyílik a pályára vezető út. A szintre belépve a játékosnak a környezettel való interakciót és az ügyességi mechanikákat kell alkalmaznia a továbbjutáshoz. Amint a játékos eléri és sikeresen használja a céltárgyat a pálya végén, a rendszer regisztrálja a teljesítést, és a folyamat a Hubban folytatódik a következő lineáris szakasszal.

A küldetések státuszát, az aktuális célokat (`currentObjective`) és a képernyő sarkában lévő felhasználói felületet (UI jegyzettömböt) a játék vezérlője (`GameManager`) automatikusan frissíti a játékos tevékenységeinek és a feladatok felvételének vagy teljesítésének függvényében.

### 3.1 A Központi Hub és a küldetésfelvétel

A két küldetés közötti navigáció és a történeti átvezetések egy több emeletből álló, 2D-s lépcsőházban, a „menü/hub” pályán történnek.

- **Navigáció:** A szintek közötti közlekedés a háttérben vagy a pálya szélén elhelyezett lépcsőkön keresztül lehetséges. A hub közepén egy meghibásodott lift húzódik végig.
- **Küldetés indítása:** A feladatok felvételéhez a megfelelő szinten várakozó vendéggel kell interakcióba lépni az erre szolgáló gomb (pl. `E` gomb) megnyomásával.
- **Dialógus és UI frissítés:** Az interakciót követően egy szövegbuborék jelenik meg a vendég panaszával. A rendszer ekkor frissíti az aktuális célt (`currentObjective`), amely a képernyő sarkában lévő jegyzettömbön (UI) is megjelenik.
- **Pályára lépés:** A párbeszéd lezárultával az adott szinthez tartozó bejárat (például egy „Staff Only” feliratú rozsdás ajtó, egy zárt liftajtó vagy egy padlásfeljáró) interaktívvá válik. Ennek aktiválásakor egy átvezető animáció vagy hangeffekt kíséretében tölt be az aktuális pálya.

### 3.2 A szintek (pályák) teljesítése

A pályákon a fő cél a kapott feladatok (például egy szelepkerék elforgatása a kazánon, vagy az áram újraindítása a biztosítékdoboznál) végrehajtása.

- **Mozgás és képességek:** A karakter kizárólag hétköznapi képességekre (futás, ugrás, csúszás) támaszkodhat. A szintek előrehaladtával olyan specifikus mozgásformák is fókuszba kerülnek, mint a vertikális mászás vagy a precíziós ugrás a törékeny platformokon.
- **Akadályok és ellenfelek:** A továbbjutást a pálya tematikájához illeszkedő környezeti akadályok (időzített gőzsugarak, szikrázó elektromos kábelek) és specifikus ellenfelek (patkányok, pókok, repülő denevérek) nehezítik.
- **Interakció és hibaüzenetek:** A tárgyakkal és a környezettel való interakció elengedhetetlen a feladatok kreatív megoldásához. Amennyiben a felhasználó érvénytelen parancsot ad meg, vagy a bemenet nem megfelelő az adott szituációban, a program hibaüzenettel, illetve megfelelő vizuális visszajelzéssel válaszol.

### 3.3 Pályatervezési (Level Design) Mátrix

A történet előrehaladását és a játékmenet lineárisan növekvő nehézségét a különböző szintek (pályák) vizuális és technikai elemeinek kombinációja határozza meg. Az alábbi mátrix összefoglalja az egyes pályákhoz tartozó narratív célokat, az interaktív küldetéstárgyakat (`QuestItem`), valamint a specifikus környezeti akadályokat (`Hazard`) és a hozzájuk rendelt mesterséges intelligenciával (AI) rendelkező ellenfeleket.

**1. táblázat: Az egyes szintek narratív és technikai tervezési mátrixa**

| Szint (Pálya) | Küldetésadó NPC | Cél (QuestItem) | Fő akadályok és mechanikák (Hazards) |
|---|---|---|---|
| 1. A Pince (The Boiler Room) | Mr. Didereg (fázik, fűtést kér) | Szelepkerék megtalálása és a fő kazán elindítása. | Sötét, indusztriális környezet; időzített gőzsugarak (`DamageZone`). |
| 2. A Liftakna (The Shaft) | Madame Pompás (türelmetlen, beszorult a lift) | Biztosítékdoboz elérése és az áram újraindítása. | Olajos drótkötelek (vertikális mászás); szikrázó elektromos kábelek. |
| 3. A Padlás (The Attic) | Tábornok (tévéadást követel) | A tetőgerinc elérése és az antenna beállítása. | Poros, korhadt fagerendák (időzített, törékeny platformok). |

A mátrixból jól látható, hogy az egyszerű platformer feladatok hogyan alakulnak át komplex kihívásokká a szintek előrehaladtával, megkövetelve a játékostól a különböző mozgási mechanikák (például csúszás a gőzsugarak alatt, vagy precíziós ugrás a törékeny fagerendákon) kombinált alkalmazását.

## 4. Grafikai megjelenés és Felhasználói felület (UI)

A játék vizuális világa 2D-s, kézzel rajzolt (hand-drawn) animációs stílusra épül, amely szatirikus és enyhén sötét, humoros hangulatot közvetít. A grafikai tervezés a hotel lerobbant, „rémálomszerű” állapotát hangsúlyozza: kopott tapéták, tompa tónusú színek, valamint túlzó, karikatúraszerű karakterdizájn (például a morcos vendégek és az életre kelt tárgyak) jellemzik a megjelenést.

### 4.1 Főmenü (Main Menu)

A játék indításakor megjelenő főmenü a hotel egy jellegzetes, lelakott helyiségét ábrázolja, amely már előrevetíti a játékmenet során tapasztalható atmoszférát (például az ott várakozó vendéggel és a fenyegető porszívóval). A felhasználói felület (UI) letisztult, a képernyő alsó sávjában elhelyezett, a környezetbe illeszkedő (díszes keretű, táblaszerű) gombokkal operál.

A navigációs sáv az alábbi interaktív menüpontokat tartalmazza:

- `PLAY GAME`: A játék (vagy az aktuális mentés) elindítása.
- `OPTIONS`: A rendszerbeállítások (például hangerő, grafika, irányítás) elérése.
- `CREDITS`: A projekt fejlesztőinek és készítőinek felsorolása.
- `EXIT`: Az alkalmazás bezárása és kilépés.

### 4.2 Töltőképernyő (Loading Screen)

A pályák betöltése, illetve a központi hub és a szintek közötti átmenet során egy tematikus töltőképernyő jelenik meg. Ez a felület vizuálisan is alátámasztja a játékmenet kaotikus és humoros alaphelyzetét, egyúttal fenntartja a felhasználó figyelmét a várakozási idő alatt. A grafikán a főszereplő (Jack) látható, amint túlterhelten menekül a hotel megelevenedő, ellenséges eszközei (például egy agresszív porszívó) elől, miközben a vendégek folyamatos, gondolatbuborékokban megjelenő követeléseivel (például egy forró kávéval) kell szembenéznie.

A töltőképernyő a következő felhasználói felületi (UI) elemeket tartalmazza:

- **Töltéssáv (Progress Bar):** A képernyő alsó, középső részén elhelyezett zöld sáv, amely valós időben mutatja a pálya betöltési folyamatának aktuális állapotát.
- **Tipográfia és Ikonográfia:** A bal felső sarokban található, a hotel stílusához illeszkedő, díszes `LOADING...` felirat, valamint a jobb felső sarokban elhelyezett, törött recepciós csengőt ábrázoló ikon biztosítja a grafikai dizájn egységességét.

### 4.3 Játékmenet (Gameplay) és Felhasználói felület (HUD)

A pályákon (szinteken) belüli játékmenet során a kamera 2D-s oldalnézetből (orthographic nézetből) követi a főszereplőt. A grafikai rétegzés (Tilemap rendszer) vizuálisan élesen elkülöníti a passzív háttérelemeket (`Background`), az interaktív talajt és akadályokat (`Ground/Environment`), valamint az előtér díszítőelemeit (`Foreground`). A pályákon megjelenő ellenfelek – mint például a padlón járőröző patkányok vagy a csatornákban leselkedő krokodilok – karikatúraszerű megjelenésükkel illeszkednek a szatirikus vizuális stílushoz.

A játékos folyamatos tájékoztatását a képernyőn (`Canvas`) lévő HUD (`Heads-Up Display`) elemek látják el, amelyek nem képezik a fizikai játéktér részét:

- **Feladatlista (Task Notebook):** A képernyő sarkában elhelyezett, jegyzettömböt mintázó UI elem, amelyen dinamikusan megjelenik és frissül a játékos aktuális célja (`currentObjective`) a küldetés felvételekor.
- **Életerő és státuszjelzők:** A képernyő valamelyik felső sarkában elhelyezett ikonok (például szívek vagy kávéscsészék), amelyek vizuális visszajelzést adnak a karakter aktuális életerejéről, illetve a rendelkezésre álló próbálkozások (életek) számáról.

## 5. A játék működés követelményei

**2. táblázat: Funkcionális és nem-funkcionális rendszerkövetelmények listája**

| ID | Megnevezés | Leírás | Prioritás |
|---|---|---|---|
| REQ-01 | Alapmozgás | A karakter képes legyen futni és ugrani (alap platformer mechanikák). | Magas |
| REQ-02 | Vertikális mozgás | A karakter képes legyen létrákon, drótköteleken mászni. | Magas |
| REQ-03 | Interakciós rendszer | Az NPC-khez (vendégekhez) érve egy `E` gomb (`Interact`) megnyomására párbeszéd induljon. | Magas |
| REQ-04 | Dialógus és UI | A párbeszéd szövegbuborékban jelenjen meg, majd a feladat (`Task`) kerüljön fel a képernyő sarkában lévő jegyzettömbre. | Magas |
| REQ-05 | Küldetésfrissítés | A `GameManager` a `currentObjective` változót dinamikusan frissítse a feladat felvételekor. | Magas |
| REQ-06 | Dinamikus akadályok | Gőzsugarak (időzített akadályok) és szikrázó kábelek implementálása a pályákon. | Közepes |
| REQ-07 | Ellenséges AI | Patkányok (padlón járőröző), pókok (falon mászó) és repülő denevérek megvalósítása. | Közepes |
| REQ-08 | Állapotmentés (Save) | A játékos aktuális szintjének és feladatának mentése és betöltése fájlból. | Közepes |

## 6. Használati esetek (Use cases)

**3. táblázat: A játék fő használati eseteinek összefoglalása**

| Megnevezés | Leírás | Prioritás |
|---|---|---|
| UC-01: Új játék / Pálya indítása | A felhasználó a főmenüből elindítja a játékot, betöltődik az első szint. | Magas |
| UC-02: Karakter irányítása | A felhasználó a billentyűzet segítségével mozgatja Jacket (ugrás, futás, mászás). | Magas |
| UC-03: Küldetés felvétele | A felhasználó interakcióba lép a „Morcos Vendég” NPC-vel (pl. Mr. Didereg), megkapja a feladatot. | Magas |
| UC-04: Tárgyhasználat | A felhasználó a pálya végén aktiválja a céltárgyat (pl. szelepkerék, biztosítékdoboz). | Magas |

**1. ábra:** A rendszer használati eset diagramja (Use Case Diagram)

### 6.1 Használati eset (Use Case) diagram leírása

A fenti diagram a *The Five Star Nightmare* rendszer és a Játékos (Actor) közötti magas szintű interakciókat, valamint a játék logikai felépítését és háttérfolyamatait mutatja be. A szoftver funkciói négy fő logikai csomagra (package) vannak bontva a modularitás és az átláthatóság érdekében:

- **Főmenü és Rendszer:** A játékos innen kezdeményezi a játék indítását (UC-01). Ez a cselekvés kötelező jelleggel kiváltja (`<<include>>`) a pálya betöltését, illetve opcionálisan, kiterjesztésként (`<<extend>>`) lehetőséget ad egy korábbi állapot vagy mentés betöltésére (REQ-08).
- **Játékmenet (Karakter irányítása):** Ez a modul felel a platformer mechanikákért. A „Mozgás” egy általános használati eset, amelyből öröklődés (generalizáció) útján származik le az Alapmozgás (futás, ugrás - REQ-01) és a Vertikális mozgás (mászás - REQ-02). A játékos feladata továbbá az akadályok és ellenfelek kikerülése; amennyiben ebben hibázik, egy feltételes `<<extend>>` kapcsolat révén bekövetkezik a sebződés (életerő csökkenés) eseménye.
- **Interakció és Küldetések:** A játékos környezettel való interakciója két fő irányba ágazik szét:
  - **Tárgyhasználat (UC-04):** A céltárgyak aktiválása, amely kötelezően maga után vonja (`<<include>>`) a háttérben futó küldetés teljesítése funkciót.
  - **Párbeszéd indítása (REQ-03):** Az NPC-kkel való kommunikáció.
- **Háttérfolyamatok (Engine & UI):** Ezek a rendszer által automatikusan futtatott folyamatok, amelyeket a játékos akciói váltanak ki. A párbeszéd indítása kötelezően meghívja a szövegbuborékok megjelenítését (REQ-04). A párbeszéd opcionális kiterjesztéseként létrejöhet a küldetés felvétele (UC-03), amelynek sikere esetén a rendszer automatikusan frissíti a felhasználói felületet, azaz a Task UI-t (REQ-05).

## 7. Viselkedés

**2. ábra:** A játékmenet logikai folyamata (Activity Diagram)

### 7.1 Aktivitás diagram leírás

A játékos a Központi Hubból (lépcsőházból) indítja a játékot, ahol alapvetően két fő cselekvési lehetősége van: interakcióba lép egy várakozó NPC-vel (morcos vendéggel), vagy belép egy már elérhető pályára (szintre). Amennyiben az NPC-vel való interakciót választja, a megfelelő gomb (pl. `E`) megnyomása után megjelenik a vendég dialógusa (szövegbuborékban), és a rendszer rögzíti az adott küldetést. Ezt követően a program automatikusan frissíti a felhasználói felületet (a képernyő sarkában lévő jegyzettömböt) az új feladattal, és interaktívvá teszi a hozzá tartozó pálya bejáratát.

Ha a játékos a pályára lépést választja, az adott ajtó vagy átjáró (például a „Staff Only” ajtó vagy a liftajtó) aktiválása után a játék egy átvezető animáció kíséretében betölti a 2D-s platformer szintet. A pályán belül a karakter irányításával (futás, ugrás, csúszás, mászás) és az ellenfelek, illetve akadályok leküzdésével kell eljutni a feladathoz kapcsolódó céltárgyig.

Amikor a játékos sikeresen interakcióba lép a tárggyal (például elforgatja a kazán szelepkerekét vagy újraindítja a biztosítékdobozt), a játék automatikusan teszteli a feltételeket, és regisztrálja a küldetés teljesítését. A rendszer minden feladat végeztével ellenőrzi a játékállapotot, megvizsgálva, hogy az összes küldetés befejeződött-e.

**3. ábra:** A karakter mozgási állapotgépe (Player State Machine Diagram)

### 7.2 Állapot diagram leírás

Amikor a játékos elindítja a játékot, a rendszer egy inicializációs (kezdő) állapotba kerül, ahol a játék világa érintetlen, és még egyetlen küldetés sincs felvéve vagy teljesítve. Ezt követően a játék automatikusan átlép a Központi Hub (Lépcsőház) állapotába. Mivel a történet és a pályák előrehaladása lineáris (kötött sorrendű), a Központi Hub állapotban a rendszer mindig csak az aktuális történeti szakasznak megfelelő, soron lévő opciókat teszi elérhetővé a felhasználó számára.

A folyamat lépéseként a játékos elsőként az éppen aktív NPC-vel léphet interakciós (Párbeszéd) állapotba. A küldetés rögzítése után a rendszer visszatér a Központi Hub állapotba, és interaktívvá teszi a kizárólag ahhoz a feladathoz tartozó pálya bejáratát. Az ajtó aktiválásával a rendszer átlép az aktív pálya (Level) állapotba, ahol a szoftver a 2D-s platformer mechanikákat (mozgás, ellenfelek, akadályok) futtatja. A céltárgy megtalálása és aktiválása révén lehet továbblépni a feladat végrehajtása, majd a küldetés teljesítése állapotba. A sikeres teljesítés után a rendszer visszacsatol a Központi Hubba, ahol elérhetővé válik a következő, szigorúan soron lévő NPC és a hozzá tartozó újabb szint. Ez a lineáris ciklus ismétlődik a játék végéig.

**4. ábra:** A szoftver statikus architektúrája (Class Diagram)

### 7.3 Osztálydiagram leírás

A mellékelt osztálydiagram a szoftver belső, objektumorientált architektúráját (struktúráját) mutatja be. A központi vezérlő a `GameManager` osztály, amely nyilvántartja az aktuális pályát (`Level`), a játékost (`Player`) és az aktív küldetéseket (`Mission`). A pályák (`Level`) magukba foglalják a rajtuk található élőlényeket (`Entity` ősosztályból származó játékos, NPC és ellenfelek), valamint az interaktív elemeket (`Interactable`). A küldetések rendszere szorosan összekapcsolódik a feladatot kiadó NPC-vel és a teljesítéshez szükséges specifikus küldetéstárggyal (`QuestItem`), miközben a `GameManager` folyamatosan ellenőrzi a státuszukat.

## 8. Technikai megvalósítás és Mechanikák

Ebben a fejezetben a játék alapvető mechanikáinak, fizikai interakcióinak és a mesterséges intelligencia (AI) viselkedésének szoftveres megvalósítása kerül bemutatásra, a Unity játékmotor specifikus eszközeire támaszkodva.

### 8.1 Mozgási mechanikák és Állapotgép (Movement Logic & State Machine)

A főszereplő (Jack) mozgása nem korlátozódik az egyszerű, horizontális (balra-jobbra) közlekedésre, hanem több, a játékélményt javító kényelmi és technikai funkciót is magában foglal. Ezek a megoldások a precíziós platformer játékok iparági standardjait követik:

- **Dinamikus ugrásmagasság:** A játékos az ugrás gomb lenyomásának hosszával szabályozhatja a karakter ugrásának magasságát. Ez a megoldás megakadályozza a „lebegő” érzést, és sokkal precízebb kontrollt biztosít a nehezebb platformok közötti navigálásnál.
- **Coyote Time (megbocsátó ugrás):** Miután a karakter lelép egy platform széléről, a rendszer még egy nagyon rövid ideig (0,1 - 0,2 másodpercig) engedélyezi az ugrás parancs kiadását.
- **Input Buffering (bemeneti pufferelés):** Amennyiben a játékos még a levegőben van, de a földet érés előtt (körülbelül 0,1 másodperccel) már kiadja az ugrás parancsot, a szoftver megjegyzi a bemenetet, és a talajt érés pillanatában azonnal végrehajtja azt, ezáltal folyékonyabbá téve a mozgást.
- **Slide és Dash (csúszás és előretörés):** A hirtelen előretörés és a csúszás elengedhetetlen a környezeti akadályok, például az időzített gőzsugarak gyors kikerüléséhez.

**Mozgási állapotgép (Movement State Machine):** A karakter mozgásának leprogramozása nem egyetlen monolitikus scriptben történik, hanem egy állapotgép (State Machine) architektúra alapján. Az egyes állapotok egyértelműen elkülönítik a lehetséges interakciókat:

- **Grounded (Földön):** Ebben az állapotban aktív a futás, a guggolás és a csúszás.
- **Airborne (Levegőben):** Itt kezelődik az ugrás, az esés folyamata, valamint a Coyote Time.
- **Dashing (Előretörés):** Egy speciális állapot, amelynek idejére a gravitációs hatás ideiglenesen kikapcsol, és a karakter egy fix irányba lő ki.

**Felhasznált Unity komponensek a mozgáshoz:**

- `Rigidbody2D`: A fizikai motor, amely a karakter súlyáért, sebességéért és a gravitáció kezeléséért felel.
- `Box / Capsule Collider 2D`: Ütközésdetektorok, amelyek meghatározzák a karakter fizikai kiterjedését. A `Capsule Collider` biztosítja, hogy a játékos ne akadjon el a talaj kisebb egyenetlenségeiben.
- `Physics Material 2D`: A fizikai anyagok felelnek a súrlódás beállításáért (például a falon csúszásnál vagy a csúszós felületen való megállásnál).
- `CharacterController2D`: Egy saját fejlesztésű script, amely összefogja a bemeneti jeleket, és a megadott parancsok alapján mozgatja a `Rigidbody`-t.

### 8.2 Pályaszerkezet, Grid és mozgó platformok logikája

A projekt pályáinak gyors és hatékony felépítése a Unity beépített 2D Tilemap rendszerére támaszkodik, amely lehetővé teszi a szintek rácsháló mentén történő kialakítását. Az alapvető koordináta-rendszert a `Grid` (Rács) határozza meg, amely definiálja az egyes cellák méretét (például 1x1 egység).

**Tilemap rétegek (Layers):** A logikai átláthatóság érdekében a pályaelemek külön rétegekre vannak bontva:

- `Background` (Háttér): Kizárólag vizuális látványelemek, fizikai ütközés (`Collision`) nélkül.
- `Ground / Environment` (Talaj): Az interaktív játéktér, ahol a karakter és az ellenségek mozgása zajlik.
- `Foreground` (Előtér): A játékos karaktere előtt megjelenő esztétikai díszítőelemek.

**Ütközéskezelés a rácson (Optimized Collisions):** A rácson történő ütközések optimalizálása két fő komponens együttes használatával történik:

- `Tilemap Collider 2D`: Automatikusan létrehozza a megfelelő ütközési felületet minden egyes lerakott kockához (`Tile`-hoz).
- `Composite Collider 2D`: Összeállítja az egymás mellett elhelyezkedő `Tile`-okat egyetlen, egybefüggő nagy felületté. Ennek a technikai megoldásnak a legfőbb előnye, hogy a karakter nem akad meg a láthatatlan illesztéseknél a precíziós ugrások vagy a csúszás (`Slide`) közben.

**Mozgó platformok logikája (Moving Platform Logic):** A dinamikus pályaelemek működése a következő mechanikákra épül:

- **Waypoint rendszer:** A mozgó platformok nem fix koordináták között ingáznak, hanem egy `Transform` listát (útvonalpontokat) követnek. Ez a rendszer egyaránt lehetővé teszi a lineáris és a körkörös mozgást is.
- **Kinematic `Rigidbody2D`:** A platformok `Kinematic` módban futnak. Ez biztosítja, hogy a fizikai motor ne lökje el őket a pályáról, miközben az ütközésdetektálás továbbra is aktív marad.
- **Szülő-gyermek viszony (Parenting):** Amikor a játékos a platformra ér (`OnCollisionEnter2D` esemény kiváltása), a karakter az objektum-hierarchiában ideiglenesen a platform „gyermekévé” válik. Ezzel az eljárással sikeresen elkerülhető, hogy Jack fizikai anomáliák miatt lecsússzon a mozgó felületről.
- **Törékeny platformok (Crumbling Platforms):** Ezeknél az elemeknél egy belső időzítő (`Timer`) fut. A játékos érintését követően néhány másodperccel a `Collider` automatikusan kikapcsol, majd egy meghatározott idő elteltével újra megjelenik, arra kényszerítve a felhasználót a gyors továbbhaladásra.

### 8.3 Ellenséges Mesterséges Intelligencia (AI) és Rendszerkapcsolatok

A pályákon megjelenő ellenfelek mozgása és viselkedése különböző logikai rendszerekre épül, amelyek a Unity motor komponenseivel és a pályát alkotó rácshálózattal (`Grid`) szorosan együttműködnek. Az `Enemy` objektumok fizikai és vizuális tulajdonságait a `Rigidbody2D` (`Dynamic` vagy `Kinematic` módban) és az `Animator` komponensek vezérlik. Támadásuk érzékelése egy különálló gyermekobjektum (`AttackZone`) segítségével történik, amelynek `BoxCollider2D` komponense `Is Trigger` módban üzemel.

**Mozgási típusok és logika:** A különböző típusú ellenfelek specifikus viselkedési mintákkal rendelkeznek:

- **Járőrözési logika (Patrol AI - Patkányok):** A földi ellenfelek egy egyszerű, kétállapotú gépet (`State Machine`) használnak, amely a `Patrol` (mozgás) és a `Turn` (irányváltás és várakozás) állapotokból áll. Mozgásuk során nemcsak a falaknál fordulnak meg, hanem a platformok szélénél is. Ezt egy lefelé irányított `Raycast` (sugárkövetés) biztosítja, amely folyamatosan vizsgálja, hogy van-e talaj az entitás előtt.
- **Vertikális mozgás (Pókok):** A falon mászó ellenfelek figyelmen kívül hagyják a gravitációt, és a falak `Collider` felületén mozognak végig.
- **Repülő mozgás (Denevérek):** A levegőben közlekedő ellenfelek egy alapvonal körül, szinuszos mozgási mintát követnek, amely kiszámíthatatlanabbá teszi a viselkedésüket.

**A rács (Grid) és az AI kapcsolata:** A játéktér rácshálózata (`Tilemap` rendszere) és a mesterséges intelligencia közötti interakció három fő elvre épül:

- **Grid Snapping (Rácshoz igazítás):** A mozgó platformok és az ellenségek kezdőpontja minden esetben a rácshoz igazodik, így a fejlesztés során elkerülhető a „lebegő” tárgyak érzete.
- **Edge Detection (Szélérzékelés):** A földi ellenfelek a `Tilemap` széléig közlekednek; ha a rendszer érzékeli, hogy a következő rácscella üres, az entitás automatikusan megfordul.
- **Tile Data (Cella adatok):** A rendszer lehetővé teszi a scriptek számára annak lekérdezését, hogy a játékos karaktere (Jack) éppen milyen típusú cellán áll (például csúszós olajon vagy normál padlón), ami befolyásolhatja a mozgási paramétereket.

## 9. Rendszerarchitektúra és Unity Objektumok

A játék elkészítéséhez többféle `GameObject` kerül felhasználásra, amelyek a játékos karakterét, a pályaelemeket, az ellenfeleket, a gyűjthető tárgyakat, valamint a kezelőfelületet és a háttérrendszereket valósítják meg.

### 9.1 Főbb Játékelemek (GameObjects) és Inspector beállításaik

Az interaktív és fizikai játékelemek az alábbi alapvető komponensekkel és konfigurációkkal rendelkeznek:

- **Player (Játékos karakter):** A játékos által irányított objektum, amely a mozgásért, az ugrásért, az animációkért és az ütközések kezeléséért felel.
  - **Komponensek:** `Sprite Renderer`, `Animator`, `Rigidbody2D` (`Body Type: Dynamic`, `Collision Detection: Continuous`, `Freeze Rotation Z: bekapcsolva`) és `BoxCollider2D / CapsuleCollider2D` (`Is Trigger: kikapcsolva`).
- **Ground / Platform (Talaj):** A pálya járható részei, amelyek fizikai akadályként működnek.
  - **Komponensek:** `Sprite Renderer`, `Rigidbody2D` (`Body Type: Static`) és `BoxCollider2D` vagy `Tilemap Collider 2D` (`Is Trigger: kikapcsolva`).
- **Collectible (Gyűjthető tárgy):** Érmék, pontot adó vagy gyógyító elemek.
  - **Komponensek:** `Sprite Renderer` és `CircleCollider2D` vagy `BoxCollider2D` (`Is Trigger: bekapcsolva`).
- **Checkpoint és Goal (Ellenőrzőpont és Célterület):** Az újraéledési pontokat és a pálya sikeres teljesítését jelző érzékelők.
  - **Komponensek:** `BoxCollider2D` (`Is Trigger: bekapcsolva`), valamint specifikus vezérlő scriptek (`Checkpoint.cs`, `LevelEnd.cs`).
- **Hazard / Trap (Csapdák):** Veszélyforrások, amelyek sebzést okoznak vagy újraindítják a pályát (például tüske vagy láva).
  - **Komponensek:** `BoxCollider2D` vagy `PolygonCollider2D` (`Is Trigger: bekapcsolva`), és a `DamageZone.cs` script.

### 9.2 Rendszervezérlők és Felhasználói Felület (Managers & UI)

A játékmenet folyamatosságát és a felhasználói tájékoztatást háttérfolyamatok biztosítják:

- **GameManager:** A játék fő logikáját kezelő háttérobjektum. Feladata a pontszám, a játékos életek száma, a pályaállapot és a UI referenciák nyilvántartása, valamint a győzelem vagy vereség állapotának vezérlése.
- **AudioManager:** Központilag kezeli a játék hangjait (zene és effektek) egy `Audio Source` komponens és az `AudioManager.cs` script segítségével (a háttérzenénél a `Play On Awake` és `Loop` beállításokkal).
- **Kamera:** `Orthographic` vetítési módban működő kamera, amely opcionálisan a `Cinemachine` rendszer segítségével követi a `Player` objektumot.
- **UI elemek:** A `Canvas` (`Render Mode: Screen Space - Overlay`) rétegen elhelyezkedő `TextMeshPro`, `Image` és `Button` elemek, amelyek a pálya fizikájától függetlenül jelenítik meg a menüket és állapotjelzőket.

### 9.3 Címkék (Tags) és Rétegek (Layers) Rendszere

A projekt átláthatósága, a scriptekben történő gyors objektum-azonosítás és az ütközési szabályok (renderelési sorrend, kameraszűrés) megfelelő működése érdekében az alábbi rendszerezés kerül alkalmazásra:

- **Alkalmazott Tag-ek:** `Player`, `Enemy`, `Ground`, `Collectible`, `Checkpoint`, `Hazard`, `Finish`.
- **Alkalmazott Layer-ek:** `Player`, `Ground`, `Enemy`, `Obstacle`, `Item`, `Hazard`, `UI`.

## 10. Tesztelés

A fejlesztési ciklus során a rendszer stabilitásának és a játékélmény folytonosságának biztosítása érdekében többszintű verifikációs folyamatot alkalmaztunk. A tesztelés elsődleges célja a szoftveres alapmechanikák validálása, a fizikai interakciók hibamentességének garantálása, valamint a nehézségi görbe (balancing) finomhangolása volt.

### 10.1 Funkcionális és Mechanikai tesztelés

A funkcionális tesztelés során a rendszerspecifikációban rögzített alapvető játékmenet-elemek (REQ-01 – REQ-05) megfelelőségét vizsgáltuk.

- **Karakterirányítás validálása:** Kiemelt figyelmet fordítottunk a dinamikus ugrásmagasság, a Coyote Time és az Input Buffering paramétereinek tesztelésére, biztosítva a reszponzív irányítást.
- **Interakciós logika:** Ellenőriztük a `GameManager` és az `Interactable` objektumok közötti jelátvitelt, különös tekintettel a küldetések felvételére és a `Task UI` dinamikus frissítésére.
- **Ütközésdetektálás:** Teszteltük a `Composite Collider 2D` komponensek hatékonyságát, kizárva a rácsháló (`Tilemap`) illesztései mentén fellépő esetleges akadási hibákat.

### 10.2 Pályatervezési és Egyensúlyi tesztelés (Level Design Testing)

Minden egyes szint (Pince, Liftakna, Padlás) egyedi mechanikáit (pl. mozgó és törékeny platformok) izolált és integrált környezetben is vizsgáltuk.

- **Útvonal-validálás:** Ellenőriztük, hogy a platformok és akadályok elhelyezése minden esetben lehetővé teszi-e a pálya teljesítését a rendelkezésre álló mozgáskészlettel (futás, ugrás, csúszás).
- **Hazard-tesztelés:** Vizsgáltuk a csapdák (gőzsugarak, elektromos kábelek) időzítését és a `DamageZone` script életerő-csökkentő hatásmechanizmusát.
- **AI viselkedésellenőrzés:** Validáltuk az ellenfelek (patkányok, pókok, denevérek) járőrözési útvonalait és a szélérzékelési (`Raycast`) logikájuk megbízhatóságát a platformok szélén.

## 11. Fejlesztési eszközök

A projekt tervezése, fejlesztése és a csapattagok közötti koordináció során az alábbi szoftverek, technológiák és platformok kerülnek felhasználásra:

- **Játékmotor:** Unity (a 2D-s platformer mechanikák, a fizika és az UI rendszerek megvalósításához).
- **Programozási nyelv:** C# (a Unity játékmotor elsődleges programozási nyelve).
- **Fejlesztési környezet (IDE):** Visual Studio / Visual Studio Code (a Unity-hez integrált kódolási és hibakeresési feladatokhoz).
- **Verziókezelés:** Git (a forráskód és a projektfájlok változásainak nyomon követésére).
- **Fájlmegosztás és felhőtárhely:** Microsoft OneDrive (a játékhoz tartozó erőforrások, mint például a 2D-s grafikai elemek és dokumentumok biztonságos tárolására és megosztására).
- **Operációs rendszerek:** Elsősorban Windows 11.
- **Kommunikációs platformok:** Messenger (gyors napi egyeztetésekhez) és Google Meet (online megbeszélésekhez és képernyőmegosztáshoz).
- **Szövegszerkesztő és dokumentációs eszközök:** Microsoft Word / Google Docs (a játékspecifikáció és a történetvázlat formális rögzítéséhez).

### 11.1 Projektmenedzsment és Feladatütemezés

**5. ábra:** Agilis feladatkezelés a GitHub Project Board felületén

A fejlesztési folyamat transzparenciája és a hatékony csapatmunka érdekében a GitHub Project Board rendszerét alkalmaztuk. Ez a vizuális kanban tábla lehetővé tette a feladatok (issue-k) állapotának nyomon követését a tervezéstől a megvalósításig.
