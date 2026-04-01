using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    public TMPro.TextMeshProUGUI interactionText; // Ide húzd be a UI szöveget az Inspectorban!

    void Update()
    {
        UpdateInteractionUI();
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Ha van aktív dialógus, azt léptetjük
            if (DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
            // Ha nincs, de van akihez odaszólhatunk, interakcióba lépünk
            else if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }
    private void UpdateInteractionUI()
    {
        // Ha nincs semmi a közelben, vagy épp beszélgetünk, ne látszódjon a felirat
        if (currentInteractable == null || DialogueManager.Instance.isDialogueActive)
        {
            interactionText.gameObject.SetActive(false);
            return;
        }

        // Egyébként aktiváljuk és lekérjük az egyedi szöveget (pl. "Belépés", "Beszélgetés")
        interactionText.gameObject.SetActive(true);
        interactionText.text = currentInteractable.GetInteractText();
        return;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (currentInteractable == interactable) 
            {
                currentInteractable = null;
                DialogueManager.Instance.EndDialogue();
            }
        }
    }
}