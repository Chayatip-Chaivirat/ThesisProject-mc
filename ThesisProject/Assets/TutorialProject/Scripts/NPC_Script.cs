using UnityEngine;

public class NPC_Script : MonoBehaviour, IInteractable
{
    [SerializeField] private SO_Dialogue dialogue;

    public void Interact()
    {
        DialogueManager.Instance.QueueDialogue(dialogue);
    }
}
