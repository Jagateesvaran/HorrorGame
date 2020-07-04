using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseHover()
    {
        animator.SetBool("selected", true);
    }

    public void OnMouseHoverExit()
    {
        animator.SetBool("selected", false);
    }
}
