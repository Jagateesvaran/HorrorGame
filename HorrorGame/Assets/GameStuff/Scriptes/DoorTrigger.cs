using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey("e"))
        {
            animation.Play("DoorOpening");
        }
    }

   
}
