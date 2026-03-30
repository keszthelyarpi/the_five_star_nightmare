using UnityEngine;

public class QuestIndicatorManager : MonoBehaviour
{
    [Header("UI Referencia")]
    public GameObject indicatorObject; // Ebbe húzd be a gyerekobjektumot (a '!')

    [Header("Mikor jelenjen meg?")]
    // Ez a feltétel azt jelenti, hogy CSAK akkor jelenik meg, ha EZ a milestone megvan...
    public MilestoneSet showAfterMilestone = MilestoneSet.GameStarted;
    
    // ...és EZ a milestone pedig még NINCS meg.
    public MilestoneSet hideAfterMilestone = MilestoneSet.Level1Completed;

    void Start()
    {
        // C# Action feliratkozás += operátorral
        GameManager.OnStateChanged += CheckIndicatorStatus;

        CheckIndicatorStatus();
    }

    void OnDestroy()
    {
        // C# Action leiratkozás -= operátorral
        GameManager.OnStateChanged -= CheckIndicatorStatus;
    }

    // Ez a függvény fut le minden alkalommal, amikor egy mérföldkövet elérünk
    void CheckIndicatorStatus()
    {
        // 1. Megnézzük, hogy megvan-e a kezdő mérföldkő
        bool hasStarted = GameManager.Milestones.Contains(showAfterMilestone);
        
        // 2. Megnézzük, hogy megvan-e a befejező mérföldkő
        bool hasFinished = GameManager.Milestones.Contains(hideAfterMilestone);

        // 3. Aktiváljuk az ikont, ha a feladat elvállalt és még nincs kész
        if (hasStarted && !hasFinished)
        {
            indicatorObject.SetActive(true);
        }
        else
        {
            indicatorObject.SetActive(false);
        }
    }
}