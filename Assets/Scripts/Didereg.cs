using UnityEngine;

public class Didereg : MonoBehaviour, IInteractable
{
    public DialogueData startQuestDialogue; // Küldetés előtt
    public DialogueData inProgressDialogue; // Küldetés közben
    public DialogueData completeQuestDialogue; // Küldetés után

    public MilestoneSet requiredMilestone; // Mi kell a befejezéshez?
    public MilestoneSet completedMilestone; // Mi legyen a jutalom milestone?

    public void Interact()
    {
        // 1. Ha már kész a küldetés
        if (GameManager.Milestones.Contains(completedMilestone))
        {
            DialogueManager.Instance.StartDialogue(completeQuestDialogue);
        }
        // 2. Ha megvannak a feltételek a befejezéshez
        else if (GameManager.Milestones.Contains(requiredMilestone))
        {
            DialogueManager.Instance.StartDialogue(inProgressDialogue);
        }
        // 3. Ha még el sem kezdte vagy folyamatban van
        else
        {
            DialogueManager.Instance.StartDialogue(startQuestDialogue);
            GameManager.AddMilestone(requiredMilestone);
            // Itt adhatod hozzá az "elkezdve" milestone-t
        }
    }

    public string GetInteractText() => "Beszélgetés";
}