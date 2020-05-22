using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class WayPointerControllerr : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.2f;
    private float lastWaypointIndex;

    private float movementSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        targetWaypoint = waypoints[targetWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        Debug.Log("Distance : " + distance);
        CheckDistanceToWaypoint(distance);


        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);

    }


    void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            //targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        
        targetWaypoint = waypoints[Random.Range(0, 4)];
    }
}
