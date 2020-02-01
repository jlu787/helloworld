using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{

    public Animator anim;
    public bool pressed = false;
    
    public void Press()
    {
        if (!pressed)
        {
            anim.SetBool("push", true);
            pressed = true;
        }
    }
}
