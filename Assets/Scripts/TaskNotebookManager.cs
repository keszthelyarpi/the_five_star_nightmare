using UnityEngine;
using TMPro; // A specifikáció TextMeshPro-t kér

public class TaskNotebookManager : MonoBehaviour
{
    [Header("UI Referencia")]
    public TextMeshProUGUI taskText; // A jegyzettömbben lévő szövegmező

    void Start()
    {
        // Feliratkozunk a GameManager eseményére
        GameManager.OnStateChanged += UpdateTaskUI;
        UpdateTaskUI(); // Kezdő állapot beállítása
    }

    void OnDestroy()
    {
        GameManager.OnStateChanged -= UpdateTaskUI;
    }

    // Ez a függvény fut le minden Milestone változáskor
    void UpdateTaskUI()
    {
        // Alapértelmezett szöveg, ha nincs aktív küldetés
        string currentObjective = "Nincs aktív feladat.";

        // A specifikáció szerinti lineáris haladás lekezelése

        // 1. SZINT: PINCE
        if (GameManager.Milestones.Contains(MilestoneSet.GameStarted) && !GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
        {
            gameObject.SetActive(true);
            currentObjective = "- Keress valakit, aki fázik a földszinten!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg) && !GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
        {
            currentObjective = "- Irány a pince! Keresd meg a szelepkereket!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted) && !GameManager.Milestones.Contains(MilestoneSet.Level1Completed))
        {
            currentObjective = "- A kazán megy. Mondd meg a jó hírt Mr. Dideregnek!";
        }

        // 2. SZINT: LIFTAKNA
        else if (GameManager.Milestones.Contains(MilestoneSet.Level1Completed) && !GameManager.Milestones.Contains(MilestoneSet.TalkedToMadamePompas))
        {
            currentObjective = "- Segíts Madame Pompásnak a liftnél!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.TalkedToMadamePompas) && !GameManager.Milestones.Contains(MilestoneSet.PowerRestored))
        {
            currentObjective = "- Mássz fel a liftaknába és javítsd meg a biztosítékot!";
        }

        // ... és így tovább a többi szintnél

        // Végül frissítjük a kijelzőt
        taskText.text = currentObjective;
    }
}