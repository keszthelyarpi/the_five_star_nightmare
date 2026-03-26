using UnityEngine;

public interface IInteractable
{
    public void Interact();
    public string GetInteractText(); // Mi jelenjen meg a kiíráson? (pl. "Beszélgetés")
}
