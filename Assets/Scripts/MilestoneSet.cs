using UnityEngine;

public enum MilestoneSet
{
    // --- Általános ---
    GameStarted,        // A játék elindult

    // --- 1. Szint: A Pince (The Boiler Room) ---
    TalkedToMrDidereg,  // Mr. Didereg elmondta a panaszát, a pince ajtaja kinyílik
    BoilerStarted,      // A szelepkerék megvan, a kazán elindult a pályán
    Level1Completed,    // Visszatértél a Hub-ba, az első küldetés lezárva

    // --- 2. Szint: A Liftakna (The Shaft) ---
    TalkedToMadamePompas, // Madame Pompás segítséget kért, a liftakna elérhető
    PowerRestored,        // A biztosítékdoboz aktiválva, az áram újraindult
    Level2Completed,      // Második küldetés kész

    // --- 3. Szint: A Padlás (The Attic) ---
    TalkedToGeneral,    // A Tábornok kiadta az antenna parancsot
    AntennaAdjusted,    // Az antenna a tetőn beállítva
    AllMissionsDone     // Minden küldetés teljesítve, a játék vége
}