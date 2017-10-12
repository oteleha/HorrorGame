using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour {
	
	private Vector3[] patrolDestinations;
	private Vector3 randomDestination;
	private NavMeshAgent agent;
	private DetectionScript detectionScript;
	private bool destroyLocation;
	private bool beingChased;
	private float timer;
	private float remainTimer = 5f;

	public Transform lastKnownLocation;

	void Start () {

		agent = GetComponent<NavMeshAgent> ();
		detectionScript = GetComponent<DetectionScript> ();
		patrolDestinations = new Vector3[3];
		patrolDestinations [0] = new Vector3 (-3.71f, 0.5f, -11.89f);
		patrolDestinations [1] = new Vector3 (20.64f, 0.5f, 12.56f);
		patrolDestinations [2] = new Vector3 (-3.71f, 0.5f, -36.16f);//populating the array
		randomDestination = patrolDestinations [Random.Range (0, patrolDestinations.Length)];//picking a vector from the array at random
	}

	void Update () {

		agent.speed = 1.5f;//setting the average patrol speed of the agent
		UpdateDestination ();//selecting a random destination from an array and when the destination has been reached, select a new random destination
		Chase ();//if the player hits the raycast, the agent will follow until they are no longer hitting the raycast, a transform will then be instantiated representing the players last known location
	}

	void UpdateDestination(){//sets the enemy's destination, then updates the destination after a certain period of time once its a certain distance away from its current target destination

		agent.speed = 1.5f;
		agent.destination = randomDestination;

		if(Vector3.Distance(transform.position, randomDestination) < 2.0f){//if the enemy is less than 2.0f distance from its destination, change destination to new random destination
			timer += Time.deltaTime;

			if (timer >= remainTimer) {
				randomDestination = patrolDestinations [Random.Range (0, patrolDestinations.Length)];//using this twice so that destination is updated 
				agent.SetDestination (randomDestination);
				timer = 0;
			}
		}
	}

	void Chase(){
		
		Ray detectionRay = detectionScript.detectionRay;//pulling all the raycasts from the detection script
		Ray detectionRayML = detectionScript.detectionRayML;
		Ray detectionRayL = detectionScript.detectionRayL;
		Ray detectionRayMR = detectionScript.detectionRayMR;
		Ray detectionRayR = detectionScript.detectionRayR;
		RaycastHit rayHit;

		//I need to set it up so that the last set known location is destroyed so the agent follows the newest
		if (Physics.Raycast (detectionRay, out rayHit, 10f)) {//if player hits a ray, set destination to the player transform.position, however if the player is no longer detected it will resume original path

			beingChased = true;
			if (beingChased == true) {
				agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
				agent.speed = 3.5f;
			}
//			if(GameObject.FindGameObjectWithTag("Player").transform.position.z > transform.position.z){//if the player is too far away, instantiate the players last known location transform
//				
//				lastKnownLocation = Instantiate (lastKnownLocation, new Vector3 (transform.position.x, 0.5f, transform.position.z + 10f), new Quaternion(0,0,0,0));
//				agent.destination = lastKnownLocation.position;
//				Debug.Log ("lastKnownLocation was instantiated ");
//				 
//				Destroy (lastKnownLocation.gameObject, 5f);
//				Debug.Log ("lastKnownLocation destroyed");
//				
//			}
		}

//		if(Physics.Raycast(detectionRayML, out rayHit, 7.5f)){
//			
//			agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
//			agent.speed = 3.5f;
////			if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x){
////
////				lastKnownLocation = Instantiate (lastKnownLocation, new Vector3 (transform.position.x, 0.5f, transform.position.z + 10f), new Quaternion(0,0,0,0));
////				agent.destination = lastKnownLocation.position;
////				Debug.Log ("lastKnownLocation was instantiated ");
////
////				Destroy (lastKnownLocation.gameObject, 5f);
////				Debug.Log ("lastKnownLocation destroyed");
////			}
//		}
//
//		if(Physics.Raycast(detectionRayL, out rayHit, 7.5f)){
//
//
//			agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
//			agent.speed = 3.5f;
////			if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x){
////
////				lastKnownLocation = Instantiate (lastKnownLocation, new Vector3 (transform.position.x, 0.5f, transform.position.z + 10f), new Quaternion(0,0,0,0));
////				agent.destination = lastKnownLocation.position;
////				Debug.Log ("lastKnownLocation was instantiated ");
////				Destroy (lastKnownLocation.gameObject, 5f);
////			}
//		}
//		 
//		if(Physics.Raycast(detectionRayMR, out rayHit, 7.5f)){
//
//
//			agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
//			agent.speed = 3.5f;
////			if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x){
////
////				lastKnownLocation = Instantiate (lastKnownLocation, new Vector3 (transform.position.x, 0.5f, transform.position.z + 10f), new Quaternion(0,0,0,0));
////				agent.destination = lastKnownLocation.position;
////				Debug.Log ("lastKnownLocation was instantiated ");
////				Destroy (lastKnownLocation.gameObject, 5f);
////			}
//		}
//
//		if(Physics.Raycast(detectionRayR, out rayHit, 7.5f)){
//
//			agent.destination = GameObject.FindGameObjectWithTag ("Player").transform.position;
//			agent.speed = 3.5f;
////			if(GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x){
////
////				lastKnownLocation = Instantiate (lastKnownLocation, new Vector3 (transform.position.x, 0.5f, transform.position.z + 10f), new Quaternion(0,0,0,0));
////				agent.destination = lastKnownLocation.position;
////				Debug.Log ("lastKnownLocation was instantiated ");
////				Destroy (lastKnownLocation.gameObject, 5f);
////			}
//		}
	}
}
