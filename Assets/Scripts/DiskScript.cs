using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskScript : MonoBehaviour
{
    private ParticleSystem particles;

    public int diskIndex = 0;
    private void Start()
    {
        particles= GetComponent<ParticleSystem>();
    }

    public void EmitParticles()
    {
        particles.Play();
    }

}