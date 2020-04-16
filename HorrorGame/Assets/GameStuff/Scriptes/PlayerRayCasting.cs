using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayCasting : MonoBehaviour
{

    public static float DistanceFromTarget;
    public float ToTarget;


    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit Hit;
        // Fire Ray Cast In front of you
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit))
        {
            ToTarget = Hit.distance;
            DistanceFromTarget = ToTarget;
        }
    }
}
