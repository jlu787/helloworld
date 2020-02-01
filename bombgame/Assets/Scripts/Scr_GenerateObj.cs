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
        DEATH,
    }
    public GameObject newCube;
    public Scr_Rotate rotateScript;
    public GameObject FrontFace = null, BackFace = null, RightFace = null, LeftFace = null, TopFace = null, BottomFace = null;
    public bool FrontDone, BackDone, RightDone, LeftDone, TopDone, BottomDone;
    public GameObject ButtonPrefab;
    public GameObject DeathButtonPrefab;
    public bool CubeFinished = false;
    public bool Spawned = false;

    // decide what goes on each face
    FaceType front, back, left, right, top, bottom;

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
                    if (front == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (front == FaceType.BUTTON)
                    FrontFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BACK:
                    if (back == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (back == FaceType.BUTTON)
                        BackFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.RIGHT:
                    if (right == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (right == FaceType.BUTTON)
                        RightFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.LEFT:
                    if (left == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (left == FaceType.BUTTON)
                        LeftFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.TOP:
                    if (top == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (top == FaceType.BUTTON)
                        TopFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BOTTOM:
                    if (bottom == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                    }
                    else if (bottom == FaceType.BUTTON)
                        BottomFace.GetComponent<ButtonPress>().Press();
                    break;
            }
        }


        CheckDone();
      
    }

    private void RandomiseFaces()
    {
        front = (FaceType)Random.Range(0, 3);
        back = (FaceType)Random.Range(0, 3);
        left = (FaceType)Random.Range(0, 3);
        right = (FaceType)Random.Range(0, 3);
        top = (FaceType)Random.Range(0, 3);
        bottom = (FaceType)Random.Range(0, 3);

        switch (front)
        {
            case FaceType.BLANK:
                FrontDone = true;
                break;
            case FaceType.BUTTON:
                FrontFace = Instantiate(ButtonPrefab, FrontFace.transform);
                break;
            case FaceType.DEATH:
                FrontFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                FrontDone = true;
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
            case FaceType.DEATH:
                BackFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                BackDone = true;

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

            case FaceType.DEATH:
                LeftFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                LeftDone = true;

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

            case FaceType.DEATH:
                RightFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                RightDone = true;
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
            case FaceType.DEATH:
                TopFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                TopDone = true;
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
            case FaceType.DEATH:
                BottomFace = Instantiate(DeathButtonPrefab, FrontFace.transform);
                BottomDone = true;
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
            if (!Spawned)
            {
                CubeFinished = true;
                transform.DOMoveX(-1, 1.25f);
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
