using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reja : MonoBehaviour
{
    [SerializeField]
    private GameObject grid;
    private bool isOpen = false;
    private Animation anim;
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        anim.Play("reja_up");
        isOpen = true;
    }

    public void PlayAudio()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            anim.Play("reja_down");
            isOpen = false;
        }
    }

    public bool IsOpen() => isOpen;

}

