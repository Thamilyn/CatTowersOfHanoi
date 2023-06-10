using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorsPuzzleManager : MonoBehaviour
{
    private Reja[] rejas;
    private AudioSource audioSrc;
    private bool isPuzzleDone = false;
    // Start is called before the first frame update
    void Start()
    {
        rejas = GameObject.FindGameObjectsWithTag("Rejas")
            .Select(go => go.GetComponent<Reja>())
            .Where(component => component != null)
            .ToArray();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var isDone = CheckAllRejasOpen();
        if (isDone && !isPuzzleDone) { 
            isPuzzleDone= true;
            audioSrc.Play();
        } else if (!isDone)
        {
            isPuzzleDone = false;
        }
    }

    private bool CheckAllRejasOpen()
    {
        foreach (Reja reja in rejas)
        {
            if (!reja.IsOpen())
            {
                return false;
            }
        }
       
        return true;
    }
}
