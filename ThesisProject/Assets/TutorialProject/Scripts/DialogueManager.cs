using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Typing Settings")]
    [SerializeField] private float textDelay = 0.03f;

    private Queue<SO_Dialogue.Info> dialogueQueue;
    private bool isInDialogue;
    private bool isTyping;
    private string completedText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialogueQueue = new Queue<SO_Dialogue.Info>();
    }

    //Typing Coroutine
    private IEnumerator TypeText(SO_Dialogue.Info info)
    {
        isTyping = true;
        dialogueText.text = "";
        completedText = info.dialogue;

        foreach (char letter in info.dialogue)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }

        isTyping = false;
    }

    // Disble player input and start dialogue
    public void QueueDialogue(SO_Dialogue dialogue)
    {
        if (isInDialogue)
            return;

        isInDialogue = true;
        dialogueBox.SetActive(true);

        dialogueQueue.Clear();

        foreach (SO_Dialogue.Info line in dialogue.dialogueInfo)
        {
            dialogueQueue.Enqueue(line);
        }

        GameObject.FindWithTag("Player")
            .GetComponent<PlayerInput>().enabled = false;

        DequeueDialogue();
    }

    // if no more dialogue, end dialogue 
    public void DequeueDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = completedText;
            isTyping = false;
            return;
        }

        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        SO_Dialogue.Info info = dialogueQueue.Dequeue();
        StartCoroutine(TypeText(info));
    }

    private void EndDialogue()
    {
        isInDialogue = false;
        dialogueBox.SetActive(false);

        GameObject.FindWithTag("Player")
            .GetComponent<PlayerInput>().enabled = true;
    }

    private void OnInteract()
    {
        if (!isInDialogue)
            return;

        DequeueDialogue();
    }
}