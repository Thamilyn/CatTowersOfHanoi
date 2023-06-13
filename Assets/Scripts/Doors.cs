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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Moon") && !animator.GetBool("IsOpen"))
        {
            OpenDoor();
            Destroy(collision.gameObject.transform.parent.gameObject);    
        }
    }
}
