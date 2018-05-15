using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

    [SerializeField]
    private Transform waypoints;
    private Transform from, to;
    private float progress;

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
    }
	
	// Update is called once per frame
	void Update () {
        if (this.GetComponent<Breakable>() == null)
        {
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
    }
}
