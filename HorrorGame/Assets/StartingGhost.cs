using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGhost : MonoBehaviour
{
    // Adjust the speed for the application.
    private float speed = 10.0f;

    public GameObject endPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, endPoint.gameObject.transform.position, step);
    }
}
