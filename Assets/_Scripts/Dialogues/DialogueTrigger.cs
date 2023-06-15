using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField]
    private DialogueManager dialogueManager;

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
}