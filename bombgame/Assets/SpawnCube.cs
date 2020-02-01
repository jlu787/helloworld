using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject CubePrefab;

    public void SpawnNewCube()
    {
        Instantiate(CubePrefab);
    }
}
