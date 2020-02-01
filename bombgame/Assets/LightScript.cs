using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public GameObject leftRedLights;
    public GameObject leftGreenLights;
    public GameObject rightRedLights;
    public GameObject rightGreenLights;
    public GameObject currentCube;

    // Start is called before the first frame update
    void Start()
    {
        currentCube = GameObject.FindGameObjectWithTag("Cube");
        TurnRed();
    }

    // Update is called once per frame
    void Update()
    {
        //if(currentCube.GetComponent<Scr_GenerateObj>().CubeFinished)
        //{
        //    TurnGreen();
        //    StartCoroutine(SpawnLights());
        //}
    }

    public void NewCubeSpawned(GameObject _newCube)
    {
        currentCube = _newCube;
    }

    public void CubeComplete()
    {
        TurnGreen();
        StartCoroutine(SpawnLights());
    }

    IEnumerator SpawnLights()
    {
        yield return new WaitForSeconds(1.0f);
        TurnRed();
    }

    void TurnRed()
    {
        leftRedLights.SetActive(true);
        rightRedLights.SetActive(true);
        leftGreenLights.SetActive(false);
        rightGreenLights.SetActive(false);
    }

    void TurnGreen()
    {
        leftRedLights.SetActive(false);
        rightRedLights.SetActive(false);
        leftGreenLights.SetActive(true);
        rightGreenLights.SetActive(true);
    }
    void TurnOff()
    {
        leftRedLights.SetActive(false);
        rightRedLights.SetActive(false);
        leftGreenLights.SetActive(false);
        rightGreenLights.SetActive(false);
    }
}
