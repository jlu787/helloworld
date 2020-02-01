using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class Scr_Rotate : MonoBehaviour
{

    public AudioSource flipSound;
    public enum Faces
    {
        FRONT = 1,
        RIGHT,
        TOP,
        BOTTOM,
        LEFT,
        BACK,
    }

    public Faces currentFace = Faces.FRONT;

    Quaternion[] orientations = new Quaternion[] {
        Quaternion.LookRotation(Vector3.forward, Vector3.right),    // 0 FRONT      Y
        Quaternion.LookRotation(Vector3.forward, -Vector3.right),   // 1 FRONT      Y
        Quaternion.LookRotation(Vector3.forward, Vector3.up),       // 2 FRONT      Y
        Quaternion.LookRotation(Vector3.forward, -Vector3.up),      // 3 FRONT      Y
        Quaternion.LookRotation(-Vector3.forward, Vector3.right),   // 4 BACK       Y
        Quaternion.LookRotation(-Vector3.forward, -Vector3.right),  // 5 BACK       Y
        Quaternion.LookRotation(-Vector3.forward, Vector3.up),      // 6 BACK       Y
        Quaternion.LookRotation(-Vector3.forward, -Vector3.up),     // 7 BACK       Y
        Quaternion.LookRotation(Vector3.right, Vector3.forward),    // 8 LEFT       Y
        Quaternion.LookRotation(Vector3.right, -Vector3.forward),   // 9 RIGHT      Y
        Quaternion.LookRotation(Vector3.right, Vector3.up),         // 10 BOTTOM    Y
        Quaternion.LookRotation(Vector3.right, -Vector3.up),        // 11 TOP       Y
        Quaternion.LookRotation(-Vector3.right, Vector3.forward),   // 12 LEFT      Y
        Quaternion.LookRotation(-Vector3.right, -Vector3.forward),  // 13 RIGHT     Y
        Quaternion.LookRotation(-Vector3.right, Vector3.up),        // 14 TOP       Y
        Quaternion.LookRotation(-Vector3.right, -Vector3.up),       // 15 BOTTOM    Y
        Quaternion.LookRotation(Vector3.up, Vector3.right),         // 16 TOP       Y
        Quaternion.LookRotation(Vector3.up, -Vector3.right),        // 17 BOTTOM    Y
        Quaternion.LookRotation(Vector3.up, Vector3.forward),       // 18 LEFT      Y
        Quaternion.LookRotation(Vector3.up, -Vector3.forward),      // 19 RIGHT     Y
        Quaternion.LookRotation(-Vector3.up, Vector3.right),        // 20 BOTTOM    Y
        Quaternion.LookRotation(-Vector3.up, -Vector3.right),       // 21 TOP       Y
        Quaternion.LookRotation(-Vector3.up, Vector3.forward),      // 22 LEFT      Y
        Quaternion.LookRotation(-Vector3.up, -Vector3.forward)      // 23 RIGHT     Y

        // FRONT 0 - 3
        // BACK 4 - 7
        // LEFT 8,12,18,22
        // RIGHT 9,13,19,23
        // BOTTOM 10,15,17,20
        // TOP 11,14,16,21
    };

    int[,] neighborIndices = new int[24, 4];

    public int orientationIndex;
    Quaternion orientation1;
    Quaternion orientation2;
    bool rotating;
    float rotationTimer;

    void Start()
    {
        Quaternion[] rotations = new Quaternion[] {
            Quaternion.AngleAxis(-90, Vector3.up),
            Quaternion.AngleAxis(90, Vector3.up),
            Quaternion.AngleAxis(-90, Vector3.right),
            Quaternion.AngleAxis(90, Vector3.right)
        };

        for (int i = 0; i < 24; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                var currentOrientation =
                    rotations[j] * orientations[i];
                float maxDot = 0f;
                int neighborIndex = 0;
                for (int k = 0; k < 24; ++k)
                {
                    float dot = Mathf.Abs(Quaternion.Dot(
                        currentOrientation, orientations[k]));
                    if (dot > maxDot)
                    {
                        maxDot = dot;
                        neighborIndex = k;
                    }
                }
                neighborIndices[i, j] = neighborIndex;
            }
        }

        transform.rotation = orientations[orientationIndex];
    }

    void Update()
    {
        const float rotationTime = .1f;
        if (rotating)
        {
            rotationTimer += Time.deltaTime;
            if (rotationTimer >= rotationTime)
            {
                transform.rotation = orientation2;
                rotating = false;
            }
            else
            {
                float t = rotationTimer / rotationTime;
                transform.rotation = Quaternion.Slerp(
                    orientation1, orientation2, t);
            }
        }
        else
        {
            int rotation = -1;
            

            if (Input.GetKeyDown("d"))
            {
                //flipSound.Play();
                rotation = 0;
            }
            else if (Input.GetKeyDown("a"))
            {
                //flipSound.Play();
                rotation = 1;
            }
            else if (Input.GetKeyDown("s"))
            {
                //flipSound.Play();
                rotation = 2;
            }
            else if (Input.GetKeyDown("w"))
            {
                //flipSound.Play();
                rotation = 3;
            }
            if (rotation != -1)
            {
                flipSound.Play();
                orientationIndex =
                    neighborIndices[orientationIndex, rotation];
                orientation1 = transform.rotation;
                orientation2 = orientations[orientationIndex];
                rotationTimer = 0f;
                rotating = true;

                WhichFace(orientationIndex);
            }
        }
    }

    // this function is called whenever the object is rotated to determine which face
    private void WhichFace(int _orientationsIndex)
    {
        // FRONT 0 - 3
        // BACK 4 - 7
        // LEFT 8,12,18,22
        // RIGHT 9,13,19,23
        // BOTTOM 10,15,17,20
        // TOP 11,14,16,21

        if (_orientationsIndex < 4)
        {
            currentFace = Faces.FRONT;
        }
        else if (_orientationsIndex < 8)
        {
            currentFace = Faces.BACK;
        }
        else if (_orientationsIndex == 8 || _orientationsIndex == 12 || _orientationsIndex == 18 || _orientationsIndex == 22)
        {
            currentFace = Faces.LEFT;
        }
        else if (_orientationsIndex == 9 || _orientationsIndex == 13 || _orientationsIndex == 19 || _orientationsIndex == 23)
        {
            currentFace = Faces.RIGHT;
        }
        else if (_orientationsIndex == 10 || _orientationsIndex == 15 || _orientationsIndex == 17 || _orientationsIndex == 20)
        {
            currentFace = Faces.BOTTOM;
        }
        else
        {
            currentFace = Faces.TOP;
        }
    }


}


