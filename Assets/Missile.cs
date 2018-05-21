using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    private GameObject model;
    private bool missleVisable;
    private Vector3 startPosition;
    private AudioSource missileAudio;

    [SerializeField]
    private Transform player;

    void Awake()
    {
        missileAudio = this.GetComponent<AudioSource>();
        model = this.transform.GetChild(0).gameObject;
        startPosition = this.transform.position;
    }

    // Use this for initialization
    void Start () {
        model.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (missleVisable)
        {
            this.transform.position = Vector3.Lerp(startPosition, player.position, (60.0f - PointSystem.TimeRemaining) / 60.0f);
            this.transform.LookAt(player);
        }

		if (PointSystem.TimeRemaining < 60.0f && !missleVisable)
        {
            missleVisable = true;
            model.SetActive(true);
        }

        if (PointSystem.TimeRemaining < (missileAudio.clip.length) && missileAudio.isPlaying == false)
        {
            missileAudio.Play();
        }
	}
}
