using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLogic : MonoBehaviour
{
    public GameObject[] pillars;
    public GameObject doorToOpen;
    private Doors doorComponent;
    // counter to keep track of the pillars that have the correct orbs on top of it
    private int correctPillars = 0;
    private int pillarsToSolve = 0;
    private bool itsSolved = false;
    // Start is called before the first frame update
    void Start()
    {
        pillars = GameObject.FindGameObjectsWithTag("Pillar");
        doorComponent = doorToOpen.GetComponent<Doors>();
        pillarsToSolve = pillars.Length;
    }

    

    private void Update()
    {
        correctPillars = 0;
        // check if all pillars have the correct orbs
        foreach (var pillar in pillars)
        {
            var p = pillar.GetComponent<Pillar>();
            if (p.HasCorrectOrb())
            {
                correctPillars++;
            }
        }

        // if we solved the puzzle open the doors and set the puzzle as solved
        if (correctPillars == pillarsToSolve && !itsSolved)
        {
            itsSolved = true;
            doorComponent.OpenDoor();
        } 
        // otherwise make sure to mark the puzzle as not solved
        else if (correctPillars < pillarsToSolve)
        {
            itsSolved = false;
        }
    }
}
