using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scr_Rotate : MonoBehaviour
{
    const float degrees = 90.0f;

    private float speed = 90.0f; // degrees per second
    private Vector3 curEuler;
    private bool rotating = false;

    // Start is called before the first frame update
    void Start()
    {
        curEuler = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // keyboard controls

        if (Input.GetKeyDown("w"))
        {
            // flip it forward
            //Vector3 newAngle = new Vector3(transform.rotation.x + degrees, transform.rotation.y, transform.rotation.z);
            ////transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, newAngle, Time.deltaTime);
            //transform.DOLocalRotate(new Vector3(transform.rotation.x + 90, 0, 0), 0.2f, RotateMode.Fast);
            //Debug.Log("Transform.rotation.x = " + transform.rotation.x.ToString());

            RotateAngle(90.0f);

        }

    }

    void RotateAngle(float _degrees)
    {
        if (rotating) return; // do not allow to rotate if already rotating
        rotating = true; // set rotating to true
        float newAngle = curEuler.y + _degrees; // calculate new angle
        while(curEuler.y < newAngle)
        {
            // move a little step at constant speed to the new angle:
            curEuler.y = Mathf.MoveTowards(curEuler.y, newAngle, speed * Time.deltaTime);
            transform.eulerAngles = curEuler; // Update the objects rotation
            //yield;
        }
        rotating = false;
    }
}


