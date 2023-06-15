using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI description;
    public Animator animator;
    public bool autoLetters;
    public float letterDelay = 0.05f;
    public bool textReady
    {
        get;
        private set;
    }
    private readonly Queue<string> sentences = new();
    public TextMeshProUGUI nextButtonText;

    public void StartDialogue(Dialogue dialogue)
    {
        textReady = false;  
        if (animator != null ) animator.SetBool("IsOpen", true);
        sentences.Clear();
        foreach (var sentence in dialogue.sentences) sentences.Enqueue(sentence);
        if (nextButtonText != null) nextButtonText.text = sentences.Count == 1 ? "" : "Continue>>";
        
        dialogueName.text = dialogue.name;
        DisplayNextSentence();
      
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            textReady = true;
            EndDialogue();
            return;
        }
        else if (sentences.Count == 1 && nextButtonText != null)
        {
            nextButtonText.text = "";
        }

        var sentence = sentences.Dequeue();
        if (autoLetters)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            description.text = sentence;
            textReady = true;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        description.text = "";
        foreach (var letter in sentence)
        {
            description.text += letter;
            yield return new WaitForSecondsRealtime(letterDelay);
        }
        textReady = true;
    }

    public bool IsOpen()
    {
        return animator.GetBool("IsOpen");
    }
}