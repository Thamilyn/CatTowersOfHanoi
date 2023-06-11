using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    private Animator animator;
    private AudioSource victoryAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        victoryAudioSource = GetComponent<AudioSource>();
        animator.SetBool("IsOpen", false);
    }

    public bool IsOpen() { return animator.GetBool("IsOpen");  }

    public void OpenDoor()
    {
        if (victoryAudioSource != null)
        {
            victoryAudioSource.Play();
        }
        animator.SetBool("IsOpen", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("IsOpen", false);
    }
}
