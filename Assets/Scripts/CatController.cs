using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public Camera cam;
    private NavMeshAgent agent;
    private Animator animator;
    public float catRadius = 2f;
    public TowerOfHanoi towersController;
    private bool canMove = false;
    private GameObject currentDisk;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canMove)
        {
            //move the cat where the mouse have been clicked
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                animator.Play("walk");
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            //if we are near some tower and the E key is pressed 
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, catRadius);
            foreach (Collider collider in hitColliders)
            {
               
                if (collider.CompareTag("Towers"))
                {
                    //Get the tower closest to the cat
                    GameObject tower = collider.gameObject;
                    int towerIndex = towersController.GetTowerIndex(tower);
                    //check if the cat is not carrying a disk
                    if (towerIndex >= 0 && currentDisk == null)
                    {
                       currentDisk = towersController.PopDisk(towerIndex);
                       if (currentDisk!= null)
                        {   
                            //the cat takes the upper disk of the tower
                            SoundManager.instance.PlaySoundEffect(2);
                            currentDisk.transform.parent = transform;
                            currentDisk.transform.position = transform.position + Vector3.up;
                        }
                    } 
                    // if the cat is already carrying a disk
                    else
                    {
                        //try to put the disk on top of the tower
                        var isDiskReleased = towersController.PushDisk(towerIndex, currentDisk);
                        // if the cat could put the disk succesfully on the tower
                        if (isDiskReleased)
                        {
                            SoundManager.instance.PlaySoundEffect(3);
                            currentDisk = null;
                        }
                        // if the disk can't be put on the tower
                        else
                        {
                            SoundManager.instance.PlaySoundEffect(1);
                        }
                    }
                    break;
                }
            }
        }
    }
    public void EnableMove()
    {
        canMove= true;
    }
    public void DisableMove()
    {
        canMove= false;
    }
}
