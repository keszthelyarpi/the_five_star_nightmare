# Sprint 1 Összefoglaló

## Csapat felosztása








## 1. Csapat felosztása és felelősségi körök

A projekt sikeres megvalósítása érdekében a feladatokat az alábbi szakmai felosztás szerint koordináljuk:

### Keszthelyi Árpád (I4KEBG) – Lead Developer & Gameplay Programmer
* **Felelősségi kör:** A Unity játékmotoron belüli technikai megvalósítás, kódolás és rendszerműködés.
* **Feladatok:**
    * A karakter mozgási állapotgépének (State Machine) leprogramozása (futás, ugrás, Coyote Time).
    * Az életerő-rendszer és a sebződési zónák (DamageZone) kezelése.
    * A szintvezérlő (GameManager) és a küldetéslogika (Mission status) fejlesztése.
    * Az ellenséges egységek mozgási MI-jének (Patrol AI) implementálása.

### Molnár Bálint Ottó (VP35W1) – Technical Artist & Visual Designer
* **Felelősségi kör:** A játék kézzel rajzolt vizuális világának megalkotása és a grafikai assetek integrálása.
* **Feladatok:**
    * Egyedi textúrák és karakter sprite-ok készítése (londiner, vendégek, ellenfelek).
    * A Tilemap rendszer vizuális felépítése és a rétegzés (Background, Ground, Foreground).
    * Környezeti elemek és animációs alapok vizuális kidolgozása.

### Zsíros Martin (XO6M7X) – Project Manager & UI/UX Designer
* **Felelősségi kör:** A projekt dokumentálása, a felhasználói felület (UI) és a hangvilág menedzselése.
* **Feladatok:**
    * A fejlesztési dokumentáció (Markdown specifikáció, Sprint jelentések) karbantartása.
    * A Főmenü (Main Menu) és a játékon belüli HUD (Task Notebook) grafikai tervezése.
    * Az AudioManager kezelése és a hangelemek (effektek, zene) beállítása.

## Sprint során elért eredmények
* A The five star nightmare projekt GitHub repozitóriuma sikeresen létrejött.
* Elkészítettük a kötelező dokumentációkat: `README.md`, `CHANGELOG.md`, `Specifikacio.md` és `Sprint1.md`.
* Szoftver architektúra ábra (osztálydiagram) létrehozása Mermaid segítségével.

## Mik akadályoztak a feladatokban? (Blockerek)
* Kezdetben meg kellett ismernünk a Mermaid szintaktikát az ábrakészítéshez, de az online dokumentációk segítségével sikeresen áthidaltuk.
* A csapat tagjainak összehangolása a GitHub hozzáférések és a Markdown fájlok egységes formázása terén igényelt némi extra egyeztetést.

## Mire van szükség a továbbhaladáshoz?
* A következő sprintben a Unity projekt tényleges inicializálására (a megfelelő Unity verzió letöltésével és a sablon létrehozásával).
* A 2D-s rácsháló (Tilemap) és a karakter (Player) alapvető mozgási logikájának (CharacterController2D) megkezdésére a specifikáció (REQ-01) alapján.