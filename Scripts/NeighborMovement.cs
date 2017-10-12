using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeighborMovement : MonoBehaviour {
//Script for NPC to wander to constantly updated targets

	public float wanderRadius;//taking the radius measuring to 1 that surrounds the navmeshagent
	public float wanderTimer;//how long the navmeshagent stays in position until its assigned another destination

	private Transform target;
	private NavMeshAgent agent;
	private float timer; 

	void OnEnable(){
		
		agent = GetComponent<NavMeshAgent> ();
		timer = wanderTimer;
	}

	void Update(){

		timer += Time.deltaTime;

		if(timer >= wanderTimer){

			Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
			agent.SetDestination (newPos);
			timer = 0;
		}
	}

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layerMask){

		Vector3 randDirection = Random.insideUnitSphere * dist; //take a random spot inside unit of sphere and multiple it by distance to be a certain amount away
		randDirection += origin; //....?

		NavMeshHit navHit;
		NavMesh.SamplePosition (randDirection, out navHit, dist, layerMask);
		return navHit.position;
	}
}