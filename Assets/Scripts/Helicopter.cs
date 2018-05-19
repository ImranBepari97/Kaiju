using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

    [SerializeField]
    private Transform waypoints;
    private Transform from, to;
    private float progress;
    private AudioSource flightAS;

    [SerializeField]
    private float speedCoeficent;

	// Use this for initialization
	void Awake () {
        from = GetWaypoint();
        to = GetWaypoint(from);
        progress = 0;
        this.transform.position = from.position;
        this.transform.LookAt(to);
        this.transform.Rotate(-90f, 0f, 0f, Space.Self);
        flightAS = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Breakable>() == null)
        {
            Crash();
            Destroy(this);
            return;
        }

		if (progress >= 1f)
        {
            progress = 0f;
            from = to;
            to = GetWaypoint(from);
            this.transform.LookAt(to);
            this.transform.Rotate(-90f, 0f, 0f, Space.Self);
        } else
        {
            progress += Time.deltaTime / (Vector3.Distance(from.position, to.position) * speedCoeficent);
            this.transform.position = Vector3.Lerp(from.position, to.position, progress);
        }
	}

    private Transform GetWaypoint(Transform except = null)
    {
        Transform chosen = null;

        while (chosen == null || chosen == except)
        {
            chosen = waypoints.GetChild(Random.Range(0, waypoints.childCount - 1));
        }

        return chosen;
    }

    private void Crash()
    {
        Destroy(this.GetComponent<Animation>());
        AudioSource crashAS = this.gameObject.AddComponent<AudioSource>();
        //Setup audio source to mimic flightAS
        crashAS.volume = flightAS.volume * 0.5f;
        crashAS.spatialBlend = flightAS.spatialBlend;
        crashAS.rolloffMode = flightAS.rolloffMode;
        crashAS.minDistance = flightAS.minDistance;
        crashAS.maxDistance = flightAS.maxDistance;
        crashAS.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/chopper_dieing"));
        StartCoroutine(FadeoutFlightAudioSource());
    }

    private IEnumerator FadeoutFlightAudioSource()
    {
        for (float t = 0f; t < 1.0f; t += Time.deltaTime)
        {
            flightAS.volume = Mathf.Lerp(0f, 0.5f, t);
            yield return new WaitForFixedUpdate();
        }
        Destroy(flightAS);
        yield return null;
    }
}
