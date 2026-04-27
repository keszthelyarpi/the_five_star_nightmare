using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Ez szükséges a jelenetváltáshoz

public class BoilerWheel : MonoBehaviour, IInteractable
{
    [Header("Forgatás beállításai")]
    public float rotationAngle = -180f;
    public float rotationSpeed = 1f;

    [Header("Küldetés lezárása")]
    public DialogueData winDialogue; // Ide húzd be a köszönő/lezáró szöveget
    public string lobbySceneName = "Lobby"; // Ide írd be a Hub pontos jelenetnevét

    private bool isRotating = false;

    // Dinamikus szöveg a specifikáció szerint 
    public string GetInteractText()
    {
        if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
            return "A kazán már üzemel";

        if (!GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
            return "Be van rozsdásodva... (Beszélj a vendéggel)";

        return "Szelep elfordítása (E)";
    }

    public void Interact()
    {
        if (isRotating) return;

        // 1. Ellenőrizzük, hogy felvettük-e a küldetést
        if (!GameManager.Milestones.Contains(MilestoneSet.TalkedToMrDidereg))
        {
            Debug.Log("Még nem tudod, hogyan kell használni.");
            return;
        }
        else if (GameManager.Milestones.Contains(MilestoneSet.BoilerStarted))
        {
            return;
        }
        else
        {
            // 2. Küldetés teljesítése: Milestone hozzáadása és animáció
            GameManager.AddMilestone(MilestoneSet.BoilerStarted);
            StartCoroutine(RotateWheelSmoothly());

            // 3. Dialógus és jelenetváltás indítása
            if (winDialogue != null)
            {
                DialogueManager.Instance.StartDialogue(winDialogue);
                StartCoroutine(WaitAndReturnToLobby());
            }
            else
            {
                Debug.LogWarning("Nincs beállítva a winDialogue! Visszalépés azonnal.");
                StartCoroutine(WaitAndReturnToLobby(true));
            }
        }

        Debug.Log("A fűtés elindult, a küldetés célja teljesítve!");
    }

    private IEnumerator RotateWheelSmoothly()
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationAngle);
        float timeElapsed = 0;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }

    // Ez a függvény figyeli a dialógus végét, majd pályát vált
    private IEnumerator WaitAndReturnToLobby(bool skipWait = false)
    {
        if (!skipWait)
        {
            // Várunk egy nagyon keveset, hogy a DialogueManager biztosan aktívra váltson
            yield return new WaitForSeconds(0.1f);

            // Amíg a dialógus aktív (a játékos olvassa/lépteti), itt várakozik a kód
            while (DialogueManager.Instance.isDialogueActive)
            {
                yield return null;
            }
        }
        else
        {
            // Ha nincs szöveg, csak várunk egy kicsit az animációra
            yield return new WaitForSeconds(1.5f);
        }

        // A specifikáció szerinti 1. szint befejezésének regisztrálása
        GameManager.AddMilestone(MilestoneSet.Level1Completed);

        // Visszatöltjük a Hub jelenetet
        SceneManager.LoadScene(lobbySceneName);
    }
}