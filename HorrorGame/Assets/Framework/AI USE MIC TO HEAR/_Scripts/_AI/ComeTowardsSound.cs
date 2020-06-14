using UnityEngine;
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
	public float TimeToSearch = 5.0f; // ----- How long do we want the enemy to chase us?
	public float Speed; // ------------------- The speed at which the enemy will follow us (Change in Inspector).


	// Spawn Blood when ghost walking around every x seconds
	public GameObject blood;


	public Transform[] points;
	private int destPoint = 0;
	private NavMeshAgent agent;



	private void Start()
	{
		playerHeard = false;
	

		agent = GetComponent<NavMeshAgent>();

		agent.autoBraking = false;

		GotoNextPoint();
	}


	void Update(){
		if (playerHeard) { // --------------------------------------------------- If the player is heard, do the following.
			TimeToSearch -= Time.deltaTime;// ----------------------------------- Deduct time for finding player.
			agent.destination = player.position;
			if (TimeToSearch <= 0){ // ------------------------------------------- If time reaches zero, then we are no longer chasing the player.
				playerHeard = false; // --------------- Chasing player no longer happens.
				TimeToSearch = 5.0f; // Revert back to our 5 seconds for next time.
			}
		}
		else if (!playerHeard)
		{
			if (!agent.pathPending && agent.remainingDistance < 0.5f)
				GotoNextPoint();
		}
	}



	

	void OnTriggerEnter(Collider col){ // ------- col is the variable name I gave for Collider.	
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
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;
	}
}



