using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script will be applied to the watchman and the experiment enemies, stores all the raycasts
public class DetectionScript : MonoBehaviour {

	public Ray detectionRay;
	public Ray detectionRayML;
	public Ray detectionRayPL;
	public Ray detectionRayL;
	public Ray detectionRayMR;
	public Ray detectionRayPR;
	public Ray detectionRayR;
	private RaycastHit rayHit;
	private Vector3 anotherPeripheralDirectionRayL;
	private Vector3 anotherPeripheralDirectionRayR;
	private Vector3 otherPeripheralDirectionRayL;
	private Vector3 otherPeripheralDirectionRayR;
	private Vector3 peripheralDirectionRayL;
	private Vector3 peripheralDirectionRayR;
	private Vector3 morePeripheralDirectionL;
	private Vector3 morePeripheralDirectionR;
	private Vector3 localDirectionRay;

	void Start () {
		
	}

	void Update () {

		anotherPeripheralDirectionRayL = new Vector3 (-4, 0, 1);
		otherPeripheralDirectionRayL = new Vector3 (-2f, 0f, 1f);
		peripheralDirectionRayL = new Vector3 (-1, 0, 1);
		morePeripheralDirectionL = new Vector3 (-0.5f, 0, 1);
		anotherPeripheralDirectionRayR = new Vector3 (4, 0, 1);
		otherPeripheralDirectionRayR = new Vector3 (2f, 0f, 1f);
		peripheralDirectionRayR = new Vector3 (1, 0, 1);
		morePeripheralDirectionR = new Vector3 (0.5f, 0, 1);
		
		detectionRay.origin = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		detectionRay = new Ray (detectionRay.origin, gameObject.transform.forward * 10f);
		detectionRayML = new Ray (detectionRay.origin, -transform.right * 7.5f);
		detectionRayPL = new Ray (detectionRay.origin, transform.TransformDirection (morePeripheralDirectionL) * 9f);
		detectionRayL = new Ray (detectionRay.origin, transform.TransformDirection (peripheralDirectionRayL) * 7.5f);//TransformDirection is what makes this shit work
		detectionRayMR = new Ray (detectionRay.origin, transform.right * 7.5f);
		detectionRayPR = new Ray (detectionRay.origin, transform.TransformDirection (morePeripheralDirectionR) * 9f);
		detectionRayR = new Ray (detectionRay.origin, transform.TransformDirection (peripheralDirectionRayR) * 7.5f);

		Debug.DrawRay (detectionRay.origin, -transform.right * 10f, Color.red);//Most left ray
		Debug.DrawRay(detectionRay.origin, transform.TransformDirection(anotherPeripheralDirectionRayL) * 2.5f, Color.red);
		Debug.DrawRay(detectionRay.origin, transform.TransformDirection(otherPeripheralDirectionRayL) * 4.5f, Color.red);//In betweeen most left and peripheral
		Debug.DrawRay (detectionRay.origin, transform.TransformDirection (peripheralDirectionRayL) * 7.5f, Color.red);//Slight left ray
		Debug.DrawRay (detectionRay.origin, transform.TransformDirection (morePeripheralDirectionL) * 9f, Color.red);//In between peripheral and center
		Debug.DrawRay (detectionRay.origin, gameObject.transform.forward * 10, Color.green);//Center ray
		Debug.DrawRay(detectionRay.origin, transform.TransformDirection(anotherPeripheralDirectionRayR) * 2.5f, Color.blue);
		Debug.DrawRay (detectionRay.origin, transform.TransformDirection (morePeripheralDirectionR) * 9f, Color.blue);//In between peripheral and center
		Debug.DrawRay (detectionRay.origin, transform.TransformDirection (peripheralDirectionRayR) * 7.5f, Color.blue);//Slight right ray
		Debug.DrawRay (detectionRay.origin, transform.TransformDirection (otherPeripheralDirectionRayR) * 4.5f, Color.blue);//In between most right and peripheral
		Debug.DrawRay (detectionRay.origin, transform.right * 10f, Color.blue);//Most right ray

		if (Physics.Raycast (detectionRay, out rayHit, 10f)) {
			Debug.Log ("Front ray hit");
		}

		if (Physics.Raycast (detectionRayML, out rayHit, 7.5f)) {
			Debug.Log ("Most left ray hit");
		}

		if (Physics.Raycast (detectionRayL, out rayHit, 7.5f)) {
			Debug.Log ("Slight left ray hit");
		}

		if (Physics.Raycast (detectionRayMR, out rayHit, 7.5f)) {
			Debug.Log ("Most right ray hit");
		}

		if (Physics.Raycast (detectionRayR, out rayHit, 7.5f)) {
			Debug.Log ("Slight right ray hit");
		}
	}
}
