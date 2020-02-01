using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GenerateObj : MonoBehaviour
{
    public enum FaceType
    {
        BLANK = 0,
        BUTTON,       
    }

    public GameObject FrontFace, BackFace, RightFace, LeftFace, TopFace, BottomFace;
    public GameObject ButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RandomiseFaces();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // check current face

        }
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
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, FrontFace.transform);
                break;
        }
        switch (back)
        {
            case FaceType.BLANK:
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, BackFace.transform);
                break;
        }
        switch (left)
        {
            case FaceType.BLANK:
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, LeftFace.transform);
                break;
        }
        switch (right)
        {
            case FaceType.BLANK:
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, RightFace.transform);
                break;
        }
        switch (top)
        {
            case FaceType.BLANK:
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, TopFace.transform);
                break;
        }
        switch (bottom)
        {
            case FaceType.BLANK:
                break;
            case FaceType.BUTTON:
                Instantiate(ButtonPrefab, BottomFace.transform);
                break;
        }
    }
}
