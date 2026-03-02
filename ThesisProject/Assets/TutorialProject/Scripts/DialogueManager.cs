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
}