using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string characterName; // Ezt kérted, hogy jelenjen meg
    [TextArea(3, 10)]
    public string[] sentences; // A dialógus mondatai
}