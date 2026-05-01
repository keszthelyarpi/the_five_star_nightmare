using UnityEngine;
using TMPro;

public class TaskNotebookManager : MonoBehaviour
{
    [Header("UI Referencia")]
    public TextMeshProUGUI taskText;

    void Start()
    {
        GameManager.OnStateChanged += UpdateTaskUI;
        UpdateTaskUI();
    }

    void OnDestroy()
    {
        GameManager.OnStateChanged -= UpdateTaskUI;
    }

    void UpdateTaskUI()
    {
        string currentObjective = "Nincs aktív feladat.";

        // 1. SZINT: PINCE (Mr. Didereg)
        if (GameManager.Milestones.Contains(MilestoneSet.GameStarted) && !GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
        {
            currentObjective = "- Keress valakit, aki fázik a földszinten!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg) && !GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
        {
            currentObjective = "- Irány a pince! Keresd meg a Szelepkereket és indítsd be a fűtést!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted) && !GameManager.Milestones.Contains(MilestoneSet.Level1Completed))
        {
            currentObjective = "- A kazán üzemel. Mondd meg a jó hírt Mr. Dideregnek!";
        }

        // 2. SZINT: LIFTAKNA (Madame Pompás)
        else if (GameManager.Milestones.Contains(MilestoneSet.Level1Completed) && !GameManager.Milestones.Contains(MilestoneSet.TalkedToMadamePompas))
        {
            currentObjective = "- Madame Pompás a liftnél várakozik. Beszélj vele!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.TalkedToMadamePompas) && !GameManager.Milestones.Contains(MilestoneSet.PowerRestored))
        {
            currentObjective = "- Mássz fel a liftakna tetejéhez és indítsd újra az áramot!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.PowerRestored) && !GameManager.Milestones.Contains(MilestoneSet.Level2Completed))
        {
            currentObjective = "- Van áram! Jelezd Madame Pompásnak, hogy indulhat a lift!";
        }

        // 3. SZINT: PADLÁS (A Tábornok)
        else if (GameManager.Milestones.Contains(MilestoneSet.Level2Completed) && !GameManager.Milestones.Contains(MilestoneSet.TalkedToGeneral))
        {
            currentObjective = "- Keresd meg a Tábornokot a legfelső emeleten!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.TalkedToGeneral) && !GameManager.Milestones.Contains(MilestoneSet.AntennaAdjusted))
        {
            currentObjective = "- Irány a padláson keresztül a tető, állítsd be az antennát!";
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.AntennaAdjusted) && !GameManager.Milestones.Contains(MilestoneSet.AllMissionsDone))
        {
            currentObjective = "- Tiszta a vétel! Térj vissza a Tábornokhoz!";
        }

        // JÁTÉK VÉGE
        else if (GameManager.Milestones.Contains(MilestoneSet.AllMissionsDone))
        {
            currentObjective = "- Mindenki elégedett. Élvezd a hotel nyugalmát!";
        }

        taskText.text = currentObjective;
    }
}