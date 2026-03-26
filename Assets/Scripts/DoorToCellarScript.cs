using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IInteractable
{
    public string sceneToLoad = "Pince";
    public bool IsInteracted { get; private set; }
    public string InteractID { get; private set; }
    public Sprite OpenedSprite;
    public string GetInteractText() => "Belépés (E)";
    void Start()
    {
        InteractID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }
    public void Interact()
    {

    }
    public void SetInteracted(bool IsInter)
    {
        if (IsInteracted = IsInter)
        {
            GetComponent<SpriteRenderer>().sprite = OpenedSprite;
        }
    }
}