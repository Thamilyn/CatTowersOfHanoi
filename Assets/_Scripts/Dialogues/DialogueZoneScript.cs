using UnityEngine;
using UnityEngine.Scripting;

public class DialogueZoneScript : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dialogueManager;
    [SerializeField]
    private DialogueTrigger dialogueTrigger;
    [SerializeField]
    private bool isTrigeredOnce = false;
    [SerializeField]
    private float timeToLive = 6.5f;
    private float liveTimer = 0;
    private bool isOnArea = false;
    private int openCounter = 0;
    public enum DialogueType
    {
        Zone,LoreItem
    }
    public DialogueType dialogueType= DialogueType.Zone;

    private void Update()
    {
        switch (dialogueType)
        {
            case DialogueType.Zone:
                if (dialogueManager.IsOpen() && Input.GetMouseButtonDown(0) && isOnArea)
                {                    
                    dialogueManager.DisplayNextSentence();
                }
                break;
            case DialogueType.LoreItem:
                if (timeToLive > 0 && dialogueManager.IsOpen() && isOnArea) 
                {
                    liveTimer += Time.unscaledDeltaTime;
                    if (liveTimer >= timeToLive )
                    {
                        EndDialogue();
                        liveTimer = 0;
                    }
                }
                break;
            default:
                break;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueManager.IsOpen() && (!isTrigeredOnce || isTrigeredOnce && openCounter < 1 ))
        {
            isOnArea = true;
            dialogueTrigger.TriggerDialogue();
            openCounter++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isOnArea = false;
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueManager.EndDialogue();
    }


}