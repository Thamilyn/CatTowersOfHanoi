using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerOfHanoi : MonoBehaviour
{
    public int numberOfDisks = 3;
    public GameObject diskPrefab;
    public GameObject[] towers;
    public GameObject bridge;
    public GameObject consoleControl;
    public GameObject doors;

    private Stack<GameObject>[] disks;
    private bool isGameDone = false;

    //150, 190, 37, 1
    public Color[] diskColors = {};


    void Start()
    {
       //use stack data structure to keep track of the relationship between the disks and the towers
        disks = new Stack<GameObject>[3];
        var tower0Pos = towers[0].transform.position;
        for (int i = 0; i < 3; i++)
        {
            disks[i] = new Stack<GameObject>();
        }
        
        var step = 0.25f;
        var yPos = tower0Pos.y;
        //create the disks and put them on the first tower
        for (int i = 0; i < numberOfDisks; i++)
        {
            var scale = (0.9f - (step * i)) / 2f;
            GameObject disk = Instantiate(diskPrefab);
            disk.GetComponent<DiskScript>().diskIndex = i;
            disk.transform.GetChild(0).GetComponent<Renderer>().material.color = diskColors[i];
            disk.transform.localScale = new Vector3(scale, scale, scale);
            disk.transform.position = new Vector3(tower0Pos.x, yPos + (step * i / 2f), tower0Pos.z);
            disk.transform.parent = transform;
            disks[0].Push(disk);
        }
    }

    void Update()
    {
        //check if the last tower has the number of disks required(3)
        if (disks[towers.Length-1].Count == numberOfDisks && !isGameDone)
        {
            isGameDone= true;
            SoundManager.instance.PlaySoundEffect(0);
            bridge.GetComponent<Animator>().SetBool("IsBridgeUp", true);
            bridge.GetComponent<AudioSource>().Play();
            doors.GetComponent<Animator>().SetBool("AreDoorsOpen", true);
            doors.GetComponent<AudioSource>().Play();
            consoleControl.GetComponent<ConsoleControl>().EnablePlayerCamera();
        }

        //press Escape to close game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //return the index asociated with the tower GameObject
    public int GetTowerIndex(GameObject tower)
    {
        return System.Array.IndexOf(towers, tower);
    }

    //gave the upper disk to the cat
    public GameObject PopDisk(int towerIndex )
    {
        if (disks[towerIndex].Count == 0)
        {
            return null;
        }
        return disks[towerIndex].Pop();
    }

    //the cat put the disk that carries on top of the stack asociated with the tower
    public bool PushDisk(int towerIndex,GameObject disk) 
    {
        var i = disks[towerIndex].Count;

        // if we have at least one disk on the tower check if we can put the disk on top
        if (i > 0)
        {
            var previousDisk = disks[towerIndex].Peek();
            var previousDiskIndex = previousDisk.GetComponent<DiskScript>().diskIndex;
            var newDiskIndex = disk.GetComponent<DiskScript>().diskIndex;

            // check if we are putting a bigger disk on top of the current one
            // this is a comparison based on the index of the disk
            if (newDiskIndex < previousDiskIndex)
            {
                               
                return false;
            }
        } 
        //put the disk on top of the stack

        disks[towerIndex].Push(disk);
        var tower = towers[towerIndex];
        var towerPosition = tower.transform.position;
        // give a small step increment of the y position
        var step = 0.25f;
        // the disk now belongs to the board game GameObject
        disk.transform.parent = transform;
        disk.transform.position = new Vector3(towerPosition.x, towerPosition.y + (step * i / 2f), towerPosition.z);

        return true;



    }
}
