using UnityEngine;
using UnityEngine.SceneManagement; // Ez kell a jelenetváltáshoz

public class LevelDoor : MonoBehaviour, IInteractable
{
    [Header("Beállítások")]
    public string sceneToLoad;           // A betöltendő pálya neve (pl. "BoilerRoom")
    public MilestoneSet requiredMilestone; // Mi kell a belépéshez? (pl. TalkedToMrDidereg)

    public string GetInteractText()
    {
        // Ha megvan a milestone, mehetünk be
        if (GameManager.Milestones.Contains(requiredMilestone))
        {
            return "Belépés (E)";
        }

        // Ha nincs meg, jelezzük, hogy zárva van
        return "Zárva... (Beszélj a megfelelő vendéggel)";
    }

    public void Interact()
    {
        // Ellenőrizzük a feltételt a GameManager-ben
        if (GameManager.Milestones.Contains(requiredMilestone))
        {
            Debug.Log(sceneToLoad + " betöltése...");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Opcionálisan ide jöhet egy hangeffekt vagy UI hibaüzenet (REQ-05)
            Debug.Log("Az ajtó nem nyílik. Még nincs meg a küldetés.");
        }
    }
}