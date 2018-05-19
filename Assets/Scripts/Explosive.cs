using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {

	Rigidbody rb;

	[SerializeField]
	float explosiveForce;
	[SerializeField]
	float explosiveRadius;
	[SerializeField]
	GameObject particles;

    [SerializeField]
    List<AudioClip> DestructionSounds;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();	
	}


	void OnCollisionEnter(Collision coll) {
		if(rb.velocity.magnitude > 1f) {
			Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
			foreach (Collider hit in colliders)
			{
				Rigidbody rb = hit.GetComponent<Rigidbody>();
				if (rb != null) {
					rb.AddExplosionForce (explosiveForce, transform.position, explosiveRadius, 3.0F);
				}

				Fracture fr = hit.GetComponent<Fracture> ();
				if (fr != null) {
					fr.Break (1);
				}

				Breakable br = hit.GetComponent<Breakable> ();
				if (br != null) {
					br.Break (1);
				}
			}
            //Spawn sound
            if (DestructionSounds.Capacity > 0)
            {
                AudioSource.PlayClipAtPoint(DestructionSounds[(int)Mathf.Round(Random.Range(0, DestructionSounds.Capacity - 1))], transform.position, 0.125f);
            }
            

			//Spawn the particles
			Instantiate (particles, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
