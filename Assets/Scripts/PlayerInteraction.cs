using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
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