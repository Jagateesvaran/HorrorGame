﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEditor;
using UnityEngine.AI;
// In this basic AI Script, I will have the enemy chase you for 5 seconds.

// Pretty much you are free to do ANY kind of AI you would like to use, just make sure
// you have the same setup in the OnTriggerEnter at the bottom of this code, and it should
// work like a charm with no issues, if you have any issues, it's your code.
// Becuase this is the easiest way I can really explain how to do this.
// I hope this helps you learn basic AI for your enemies.
// This is in NO WAY SHAPE OR FORM something you would really want to use for a game
// unless this is the kind of AI you want, but it is not advanced.


public class ComeTowardsSound : MonoBehaviour {
	// This is a very basic AI that gets alerted to the noise.
	// and chases the player around for 5 seconds.


	public Transform player; // -------------- Be sure to drop your player into the Inspector Slot.
	public bool playerHeard; // -------------- This is a bool that gets triggered if the trigger hits the enemy.
	public float TimeToSearch = 2.0f; // ----- How long do we want the enemy to chase us?
	public float Speed; // ------------------- The speed at which the enemy will follow us (Change in Inspector).

	private GameObject[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;


	public Animator animator;

	// Audio 
	public AudioSource seeYouAudio;
	public AudioClip seeYouClip;
	public bool alreadyPlayed = false;

	private void Start()
	{

		animator.SetBool("isWalking", true);

		playerHeard = false;
		//targetWaypoint = waypoints[targetWaypointIndex];

		agent = GetComponent<NavMeshAgent>();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = false;

		points = GameObject.FindGameObjectsWithTag("walkPoints");


		GotoNextPoint();
	}


	void Update(){

		// Play Audio Of I See u
        if (!alreadyPlayed && playerHeard)
        {
			seeYouAudio.PlayOneShot(seeYouClip);
			alreadyPlayed = true;
		}

		if (playerHeard) { // --------------------------------------------------- If the player is heard, do the following.

			TimeToSearch -= Time.deltaTime;// ----------------------------------- Deduct time for finding player.
			//transform.LookAt(player); // ---------------------------------------- Always face the player (This is just for basic AI Guys).
			//transform.Translate(Vector3.forward * Speed * Time.deltaTime); // --- Chase our player at our Speed float.
			// go to the place the player is ones is found

			agent.destination = player.position;
			if (TimeToSearch <= 0){ // ------------------------------------------- If time reaches zero, then we are no longer chasing the player.
				playerHeard = false; // --------------- Chasing player no longer happens.
				TimeToSearch = 1.0f; // Revert back to our 5 seconds for next time.
			}
		}
		else if (!playerHeard)
		{


			//float movementStep = Speed * Time.deltaTime;
			//float distance = Vector3.Distance(transform.position, targetWaypoint.position);
			//CheckDistanceToWaypoint(distance);
			//transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
			// Choose the next destination point when the agent gets
			// close to the current one.

			if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
				GotoNextPoint();
				alreadyPlayed = false;
			}
		}




	}



	//void CheckDistanceToWaypoint(float currentDistance)
	//{
	//	if (currentDistance <= minDistance)
	//	{
	//		Instantiate(blood, this.gameObject.transform.position, Quaternion.identity);
	//		//targetWaypointIndex++;
	//		UpdateTargetWaypoint();
	//	}
	//}

	//void UpdateTargetWaypoint()
	//{

	//	targetWaypoint = waypoints[Random.Range(0, 4)];
	//}

	// The below code I will explain as it seems very simple, and it is! But I want to explain why I did it
	// this way and not another way. To keep it simple.

	// Okay, if the Sound Zone (the trigger for our microphone) hits this enemy, then we will enable
	// the playerHeard bool to true, by being true, the code above this is then activated to happen until
	// the 5 seconds has reached 0, then at the end of the code up top, when it says "TimeToSearch = 5.0f"
	// It's resetting the time back to 5 seconds again, so if this code BELOW gets triggered again
	// The AI Can chase us with 5 seconds again.


	void OnTriggerEnter(Collider col){ // ------- col is the variable name I gave for Collider.
		// AI Game Logic Here, go to the exact position the noise was heard at.
	
		if(col.gameObject.tag == "SoundZone"){ // --- If the Sound Zone (Our microphone hits the player then activate.
			playerHeard = true; // ------------------- Player is heard.
			}
		}



	void GotoNextPoint()
	{
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].gameObject.transform.position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}
}



