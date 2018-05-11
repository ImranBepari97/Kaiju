using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VehicleAI : MonoBehaviour {

	DebrisManager manager;
	bool isDriving = true;
	Vector3 destination;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		manager = GetComponent<DebrisManager> ();	
		agent = GetComponent<NavMeshAgent> ();
		FindRandomPoint ();
	}
	
	// Update is called once per frame
	void Update () {
		//If the object is free 
		if (manager.shouldBeManaged) {
			gameObject.tag = "Debris"; 
			Destroy(agent);
			Destroy (this);
		} else {
			//If not a physics object, drive 
			Drive ();
		}


	}

	void Drive() {
		Debug.DrawLine (transform.position, agent.destination);
		// || agent.velocity.magnitude < 0.2f
		if ((transform.position - destination).magnitude < 0.5f || agent.speed < 0.2f || agent.destination == null) {
			FindRandomPoint ();
		} else {
			agent.destination = destination;
		} 

		if (agent.velocity.magnitude != 0)
		{
			transform.rotation = Quaternion.LookRotation(agent.velocity);
		}
	}

	void FindRandomPoint() {
		//Random point within 10m
		Vector3 rand = transform.position + (Random.insideUnitSphere * 5);

		NavMeshHit navHit;
		NavMesh.SamplePosition (rand, out navHit, 5, -1);

		destination = navHit.position;
	
	}
}
