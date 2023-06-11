using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public AudioSource bombSound;
    public float delay=2.5f;
    private float countdown;
    public GameObject explosionEffect;
    private bool hasExploded = false;
    public float explotionRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !hasExploded)
        {

            bombSound.Play();
            Explode();
        }
    }
    void Explode()
    {
        hasExploded=true;
        // instantiate explosion effect
        Instantiate(explosionEffect,transform.position,transform.rotation);
        var colliders = Physics.OverlapSphere(transform.position, explotionRadius);
        foreach (var collider in colliders)
        {
        
            DoorExplosive door = collider.GetComponent<DoorExplosive>();
            if (door != null)
            {
                door.Explode();
            }
        }
        // destroy the bomb delaying it until the bombsound has finished playing
        Destroy(gameObject, bombSound.clip.length);
    }
}
