using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject CubePrefab;
    //public GameObject lightController;
    GameObject lightController;

     void Start()
    {
        lightController = GameObject.FindGameObjectWithTag("LightController");
    }

    public void SpawnNewCube()
    {
        lightController.GetComponent<LightScript>().NewCubeSpawned(Instantiate(CubePrefab));
    }
}
