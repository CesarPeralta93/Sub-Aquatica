using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour, Interactuable
{

    public Animator anim;

    public void Activar()
    {
        anim.SetBool("Hit", true);
    }
    
}
