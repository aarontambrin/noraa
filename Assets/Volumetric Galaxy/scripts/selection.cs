using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection : MonoBehaviour {
	GameObject thisgameobject;
	public float thisobjectID;
	public float thisobjectrandomnumber;
	int hitcount;
	public Vector3 thisobjectposition;
	public GameObject starparticles;
	galaxy2 galaxyscript;



	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit = new RaycastHit ();
			if (Physics.Raycast (ray, out hit, 1000f) && hit.transform.gameObject.tag == "star") {
				thisobjectID = hit.collider.gameObject.GetComponent<initiatestar> ().thisstarnumber;
				thisobjectrandomnumber = hit.collider.gameObject.GetComponent<initiatestar> ().thisstarrandomnumber;
				thisobjectposition = hit.collider.gameObject.transform.position;
				Debug.Log ("Star number: " + thisobjectID + " Random ID: " + thisobjectrandomnumber + " Vector3 Position: " + thisobjectposition);
				//hitcount++;

			}

		}

	}

}

