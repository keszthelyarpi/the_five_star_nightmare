using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Referenciák")]
    public GameObject dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Beállítások")]
    public float typingSpeed = 0.02f;

    public bool isDialogueActive = false;
    private int sentenceIndex = 0;
    private DialogueData currentData;
    private Coroutine typingCoroutine; // Eltároljuk a futó animációt

    void Awake() => Instance = this;

    public void StartDialogue(DialogueData data)
    {
        if (data == null) return;

        currentData = data;
        sentenceIndex = 0;
        isDialogueActive = true;
        dialogueBox.SetActive(true);

        // HIBÁZOTT: Itt meg kell hívni az első mondatot!
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // Ha elfogytak a mondatok, bezárjuk
        if (sentenceIndex >= currentData.sentences.Length)
        {
            EndDialogue();
            return;
        }

        string nextSentence = currentData.sentences[sentenceIndex];
        nameText.text = currentData.characterName;

        // Megállítjuk az előző gépelést, ha még futna
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(nextSentence));

        sentenceIndex++;
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null; // Kész a gépelés
    }
}