using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkieTalkie : MonoBehaviour {

    private List<AudioClip> alreadyQueued;
    private Queue<AudioClip> queue;
    [SerializeField]
    private AudioClip music;

    private AudioClip walkieTalkieBeep;

    void Awake()
    {
        alreadyQueued = new List<AudioClip>();
        queue = new Queue<AudioClip>();
        walkieTalkieBeep = Resources.Load<AudioClip>("Audio/SFX/walkie-talkie-beep");
        StartCoroutine(VoiceScheduler(this.gameObject.AddComponent<AudioSource>()));
        StartCoroutine(PlayMusicTwice(this.gameObject.AddComponent<AudioSource>(), music));
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

    IEnumerator VoiceScheduler(AudioSource voiceAudioSource)
    {
        voiceAudioSource.priority = 53;
        while (true)
        {
            if (queue.Count > 0 && !voiceAudioSource.isPlaying)
            {
                voiceAudioSource.PlayOneShot(walkieTalkieBeep);
                yield return new WaitForSeconds(walkieTalkieBeep.length);
                AudioClip x = queue.Dequeue();
                voiceAudioSource.PlayOneShot(x);
                yield return new WaitForSeconds(x.length);
                voiceAudioSource.PlayOneShot(walkieTalkieBeep);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator PlayMusicTwice(AudioSource musicAudioSource, AudioClip musicClip)
    {
        musicAudioSource.volume = 0.1f;
        musicAudioSource.clip = musicClip;
        yield return new WaitForSeconds(3f);
        musicAudioSource.Play();
        yield return new WaitForSeconds(musicClip.length + 3f);
        musicAudioSource.Play();
    }
}
