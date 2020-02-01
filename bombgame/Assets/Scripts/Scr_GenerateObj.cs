using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_GenerateObj : MonoBehaviour
{
    public enum FaceType
    {
        BLANK = 0,
        BUTTON,       
    }
    public GameObject newCube;
    public Scr_Rotate rotateScript;
    public GameObject FrontFace = null, BackFace = null, RightFace = null, LeftFace = null, TopFace = null, BottomFace = null;
    public bool FrontDone, BackDone, RightDone, LeftDone, TopDone, BottomDone;
    public GameObject ButtonPrefab;
    public bool CubeFinished = false;
    public bool Spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseFaces();
        transform.DOMoveX(0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // check current face
            switch(rotateScript.currentFace)
            {
                case Scr_Rotate.Faces.FRONT:
                    if (FrontFace != null)
                    FrontFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BACK:
                    BackFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.RIGHT:
                    RightFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.LEFT:
                    LeftFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.TOP:
                    TopFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BOTTOM:
                    BottomFace.GetComponent<ButtonPress>().Press();
                    break;
            }
        }


        CheckDone();
      
    }

    private void RandomiseFaces()
    {
        // decide what goes on each face
        FaceType front, back, left, right, top, bottom;

        front = (FaceType)Random.Range(0, 2);
        back = (FaceType)Random.Range(0, 2);
        left = (FaceType)Random.Range(0, 2);
        right = (FaceType)Random.Range(0, 2);
        top = (FaceType)Random.Range(0, 2);
        bottom = (FaceType)Random.Range(0, 2);

        switch (front)
        {
            case FaceType.BLANK:
                FrontDone = true;
                break;
            case FaceType.BUTTON:
                FrontFace = Instantiate(ButtonPrefab, FrontFace.transform);
                break;
        }
        switch (back)
        {
            case FaceType.BLANK:
                BackDone = true;

                break;
            case FaceType.BUTTON:
                BackFace = Instantiate(ButtonPrefab, BackFace.transform);
                break;
        }
        switch (left)
        {
            case FaceType.BLANK:
                LeftDone = true;

                break;
            case FaceType.BUTTON:
                LeftFace = Instantiate(ButtonPrefab, LeftFace.transform);
                break;
        }
        switch (right)
        {
            case FaceType.BLANK:
                RightDone = true;

                break;
            case FaceType.BUTTON:
                RightFace = Instantiate(ButtonPrefab, RightFace.transform);
                break;
        }
        switch (top)
        {
            case FaceType.BLANK:
                TopDone = true;
                break;
            case FaceType.BUTTON:
                TopFace = Instantiate(ButtonPrefab, TopFace.transform);
                break;
        }
        switch (bottom)
        {
            case FaceType.BLANK:
                BottomDone = true;

                break;
            case FaceType.BUTTON:
                BottomFace = Instantiate(ButtonPrefab, BottomFace.transform);
                break;
        }
    }

    // function checks which sides are done
    private void CheckDone()
    {
        if (!TopDone)
        {
            TopDone = TopFace.GetComponent<ButtonPress>().pressed;
        }
        if (!BottomDone)
        {
            BottomDone = BottomFace.GetComponent<ButtonPress>().pressed;
        }
        if (!RightDone)
        {
            RightDone = RightFace.GetComponent<ButtonPress>().pressed;
        }
        if (!LeftDone)
        {
            LeftDone = LeftFace.GetComponent<ButtonPress>().pressed;
        }
        if (!FrontDone)
        {
            FrontDone = FrontFace.GetComponent<ButtonPress>().pressed;
        }
        if (!BackDone)
        {
            BackDone = BackFace.GetComponent<ButtonPress>().pressed;
        }

        // if all sides are complete then cube is finished
        if (TopDone && BottomDone && RightDone && LeftDone && FrontDone && BackDone)
        {
            CubeFinished = true;
            transform.DOMoveX(-1, 1.25f);
            if (!Spawned)
            {
                GameObject.FindWithTag("Spawner").GetComponent<SpawnCube>().SpawnNewCube();
                StartCoroutine(DestroySelf());
                Spawned = true;
            }
        }
    }

    IEnumerator DestroySelf()
    {
        Destroy(gameObject, 1.0f);
        yield return null;
    }
}
