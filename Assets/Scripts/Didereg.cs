using UnityEngine;

public class Didereg : MonoBehaviour, IInteractable
{
    public bool IsInteracted { get; private set; }
    public GameObject InteractIcon;
    public Didereg()
    {
        GameManager.OnStateChanged += CheckInteractibility;
    }
    public void CheckInteractibility()
    {
        if (GameManager.Milestones.Contains(Milestones.DideregFristTalk))
        {
            InteractIcon.SetActive(false);
        }
        else
        {
            InteractIcon.SetActive(true);
        }

    }
    public void Interact()
    {
        if (GameManager.Milestones.Contains(Milestones.DideregFristTalk))
        {
            Debug.Log("már a pincében kéne lenned.");
        }
        else
        {
            Debug.Log("helo");
            GameManager.AddMilestone(Milestones.DideregFristTalk);
        }
        // Itt hívd meg a DialogePanel-edet
    }
    public string GetInteractText() => "Beszélgetés (E)";

}