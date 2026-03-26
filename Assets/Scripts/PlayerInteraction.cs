using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    void Update()
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.position.ToString());
        Debug.Log(collision.name);

        if (collision.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log(interactable);
            currentInteractable = interactable;
            // Itt kapcsolhatod be a "DoorCanvas"-t vagy a Promptot
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            if (currentInteractable == interactable) currentInteractable = null;
        }
    }
}