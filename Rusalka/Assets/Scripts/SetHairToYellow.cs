using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHairToYellow : MonoBehaviour
{
    public Animator animator;
    private void Start()
    {
        animator.SetBool("GrayHair", true);
    }
    
}
