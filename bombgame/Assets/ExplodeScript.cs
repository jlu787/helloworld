using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    public AudioSource explodeSound;
    public ParticleSystem explosionPS;

    public void Explode()
    {
        explodeSound.Play();
        explosionPS.Play();
        Debug.Log("EXPLOSION");
    }
    
}
