using System.Collections;
using UnityEngine;
using UnityEngine.Events; // Ez feltétlenül kell az eseményekhez!
using UnityEngine.SceneManagement;

public class LevelEndObjective : MonoBehaviour, IInteractable
{
    [Header("Mérföldkövek (Milestones)")]
    public MilestoneSet requiredMilestone;      // Pl. TalkedToMrDidereg
    public MilestoneSet grantedMilestone;       // Pl. BoilerStarted
    public MilestoneSet levelCompleteMilestone; // Pl. Level1Completed

    [Header("UI Szövegek")]
    public string textWhenDone = "Már működik";
    public string textWhenLocked = "Be van rozsdásodva... (Beszélj a vendéggel)";
    public string textWhenReady = "Használat (E)";

    [Header("Küldetés lezárása")]
    public DialogueData winDialogue;
    public string lobbySceneName = "Lobby";

    [Header("Események (Kattints a + gombra az Inspectorban)")]
    public UnityEvent onObjectiveCompleted; // Ide kötjük be a forgatást a Unity felületén

    private bool isCompleted = false;

    public string GetInteractText()
    {
        if (GameManager.Milestones.Contains(grantedMilestone)) return textWhenDone;
        if (!GameManager.Milestones.Contains(requiredMilestone)) return textWhenLocked;
        return textWhenReady;
    }

    public void Interact()
    {
        // Ha már megcsináltuk, vagy nincs meg a feltétel, kilépünk
        if (isCompleted || GameManager.Milestones.Contains(grantedMilestone) || !GameManager.Milestones.Contains(requiredMilestone))
            return;

        isCompleted = true;
        GameManager.AddMilestone(grantedMilestone);

        // 1. MEGHÍVJUK AZ EGYEDI ANIMÁCIÓKAT/HANGOKAT
        onObjectiveCompleted?.Invoke();

        // 2. Indítjuk a párbeszédet és a jelenetváltást
        if (winDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(winDialogue);
            StartCoroutine(WaitAndReturnToLobby());
        }
        else
        {
            StartCoroutine(WaitAndReturnToLobby(true));
        }
    }

    private IEnumerator WaitAndReturnToLobby(bool skipWait = false)
    {
        if (DialogueManager.Instance == null) skipWait = true;

        if (!skipWait)
        {
            yield return new WaitForSeconds(0.1f);
            while (DialogueManager.Instance.isDialogueActive) yield return null;
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }

        GameManager.AddMilestone(levelCompleteMilestone);
        SceneManager.LoadScene(lobbySceneName);
    }
}