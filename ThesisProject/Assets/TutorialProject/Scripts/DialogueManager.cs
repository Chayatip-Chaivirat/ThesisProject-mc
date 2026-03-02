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
}