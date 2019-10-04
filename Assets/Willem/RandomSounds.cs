using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{

    [SerializeField] private int RollSpeed = 30; // amount of updates it takes to roll for a new sound
    [SerializeField] private int ChanceForSound = 30; //% chance for a sound to happen
    [SerializeField] private int WaitAfterSound = 3; //amount of rolls to wait after a sound has played
    [SerializeField] private RandomSound[] Sounds;

    [System.Serializable]
    public class RandomSound
    {
        [SerializeField] public AudioClip Sound;
        [SerializeField] public int ChanceMultiplier = 1;
    }

    private AudioSource source;
    private bool Delay = true;
    private int DelayCounter = 0;
    private int UpdateCounter = 0;

    private List<AudioClip> clips;

    void Start()
    {
        source = GetComponent<AudioSource>();
        clips = new List<AudioClip>();

        for (int i = 0; i < Sounds.Length; i++)
        {
            for (int snd = 0; snd < Sounds[i].ChanceMultiplier; snd++)
            {
                clips.Add(Sounds[i].Sound);
            }
        }
    }

    void FixedUpdate()
    {
        if (UpdateCounter > RollSpeed)
        {
            UpdateCounter = 0;
            if (!AfterSoundDelay())
            {
                if (Random.Range(0, 100) <= ChanceForSound)
                {
                    source.clip = clips[Random.Range(0, clips.Count)];
                    source.Play();
                    Delay = true;
                }
            }
        }
        else
        {
            UpdateCounter += 1;
        }
    }


    bool AfterSoundDelay() //returns false if there is no delay
    {
        if (Delay)
        {
            if (DelayCounter > WaitAfterSound) { Delay = false; DelayCounter = 0; }
            else { DelayCounter += 1; }

            return true;
        }
        else { return false; }
    }
}
