using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WalkieTalkie : MonoBehaviour {

    private List<AudioClip> alreadyQueued;
    private Queue<AudioClip> queue;

    void Awake()
    {
        alreadyQueued = new List<AudioClip>();
        queue = new Queue<AudioClip>();
        StartCoroutine(VoiceScheduler());
    }

    // Update is called once per frame
    void Update () {
        float tr = PointSystem.TimeRemaining;

        //Timing based
        if (tr < 30f)
        {
            QueueVoice("Audio/voice/warning_30_Secs");
        } else if (tr < 60f)
        {
            QueueVoice("Audio/voice/warning_One_Min");
        } else if (tr < 120f)
        {
            QueueVoice("Audio/voice/warning_Two_Mins");
        } else if (tr < 150f)
        {
            QueueVoice("Audio/voice/Countdown");
        } else if (tr < 170f)
        {
            QueueVoice("Audio/voice/We_Got_Permission");
        } else if (tr < 185f)
        {
            QueueVoice("Audio/voice/Can_We_Use_Missiles");
        } else if (tr < 200f)
        {
            QueueVoice("Audio/voice/Our_Weapons_Dont_Work");
        } else if (tr < 215f)
        {
            QueueVoice("Audio/voice/Confirmed_Kaiju");
        } else if (tr < 230f)
        {
            QueueVoice("Audio/voice/first_contact");
        }
	}

    public void QueueVoice(string path)
    {
        QueueVoice(Resources.Load<AudioClip>(path));
    }

    public void QueueVoice(AudioClip voice)
    {
        if (voice == null)
        {
            Debug.LogWarning("Could not locate voice file in resources folder. Skipping...");
        }

        if (!alreadyQueued.Contains(voice))
        {
            alreadyQueued.Add(voice);
            queue.Enqueue(voice);
        }
    }

    IEnumerator VoiceScheduler()
    {
        AudioSource a = GetComponent<AudioSource>();
        while (true)
        {
            if (queue.Count > 0 && !a.isPlaying)
            {
                a.PlayOneShot(queue.Dequeue());
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
