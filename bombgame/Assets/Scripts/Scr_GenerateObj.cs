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

    //public AudioSource explodeSound;
    public AudioSource wooshSound;
    

    // decide what goes on each face
    public FaceType[] faces = new FaceType[6]; // f,b,l,r,t,bot

    const int Front = 0;
    const int Back = 1;
    const int Left = 2;
    const int Right = 3;
    const int Top = 4;
    const int Bot = 5;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseFaces();
        transform.DOMoveX(0, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !CubeFinished)
        {
            // check current face
            switch(rotateScript.currentFace)
            {
                case Scr_Rotate.Faces.FRONT:
                    if (faces[Front] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                        GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;

                    }
                    else if (faces[Front] == FaceType.BUTTON)
                    FrontFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BACK:
                    if (faces[Back] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                       GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;


                    }
                    else if (faces[Back] == FaceType.BUTTON)
                        BackFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.RIGHT:
                    if (faces[Right] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                        GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;


                    }
                    else if (faces[Right] == FaceType.BUTTON)
                        RightFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.LEFT:
                    if (faces[Left] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                        GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;


                    }
                    else if (faces[Left] == FaceType.BUTTON)
                        LeftFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.TOP:
                    if (faces[Top] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                        GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;


                    }
                    else if (faces[Top] == FaceType.BUTTON)
                        TopFace.GetComponent<ButtonPress>().Press();
                    break;
                case Scr_Rotate.Faces.BOTTOM:
                    if (faces[Bot] == FaceType.DEATH)
                    {
                        Debug.Log("YOU DEAD");
                        GameObject.FindWithTag("Explosion").GetComponent<ExplodeScript>().Explode();
                        GameObject.FindWithTag("Timer").GetComponent<TimerScript>().paused = true;


                    }
                    else if (faces[Bot] == FaceType.BUTTON)
                        BottomFace.GetComponent<ButtonPress>().Press();
                    break;
            }
        }


        CheckDone();
      
    }

    private void RandomiseFaces()
    {
        // check how many buttons there are
        int buttons = 0;

        for (int i = 0; i < 6; i++)
        {
            faces[i] = (FaceType)Random.Range(0, 3);

            if (faces[i] == FaceType.BUTTON)
            {
                buttons++;
            }
        }

       while(buttons < 2)
        {
            int randomFace = Random.Range(0, 6);
            if (faces[randomFace] != FaceType.BUTTON)
            {
                faces[randomFace] = FaceType.BUTTON;
                buttons++;
            }
        }


        switch (faces[Front])
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
        switch (faces[Back])
        {
            case FaceType.BLANK:
                BackDone = true;

                break;
            case FaceType.BUTTON:
                BackFace = Instantiate(ButtonPrefab, BackFace.transform);
                break;
            case FaceType.DEATH:
                BackFace = Instantiate(DeathButtonPrefab, BackFace.transform);
                BackDone = true;

                break;
        }
        switch (faces[Left])
        {
            case FaceType.BLANK:
                LeftDone = true;

                break;
            case FaceType.BUTTON:
                LeftFace = Instantiate(ButtonPrefab, LeftFace.transform);
                break;

            case FaceType.DEATH:
                LeftFace = Instantiate(DeathButtonPrefab, LeftFace.transform);
                LeftDone = true;

                break;
        }
        switch (faces[Right])
        {
            case FaceType.BLANK:
                RightDone = true;

                break;
            case FaceType.BUTTON:
                RightFace = Instantiate(ButtonPrefab, RightFace.transform);
                break;

            case FaceType.DEATH:
                RightFace = Instantiate(DeathButtonPrefab, RightFace.transform);
                RightDone = true;
                break;
        }
        switch (faces[Top])
        {
            case FaceType.BLANK:
                TopDone = true;
                break;
            case FaceType.BUTTON:
                TopFace = Instantiate(ButtonPrefab, TopFace.transform);
                break;
            case FaceType.DEATH:
                TopFace = Instantiate(DeathButtonPrefab, TopFace.transform);
                TopDone = true;
                break;
        }
        switch (faces[Bot])
        {
            case FaceType.BLANK:
                BottomDone = true;

                break;
            case FaceType.BUTTON:
                BottomFace = Instantiate(ButtonPrefab, BottomFace.transform);
                break;
            case FaceType.DEATH:
                BottomFace = Instantiate(DeathButtonPrefab, BottomFace.transform);
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
                GameObject.FindWithTag("LightController").GetComponent<LightScript>().CubeComplete();
                GameObject.FindWithTag("Timer").GetComponent<TimerScript>().BombSet();
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
