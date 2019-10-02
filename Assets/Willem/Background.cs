using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] private AudioClip[] BackgroundSounds;
    [SerializeField] private float maxVolume;

    private AudioSource[] sources;
    private int CurrentSound = -1;
    private int CurrentSource = 0;

    void Start()
    {
        sources = gameObject.GetComponentsInChildren<AudioSource>();
    }
    
    public void playBackgroundSound(int BGIndex) //chainges to a different bg sound (will add a fadein/out later)
    {
        if (BGIndex != CurrentSound)
        {
            sources[otherSource(CurrentSource)].clip = BackgroundSounds[BGIndex];
            sources[CurrentSource].volume = 0;
            sources[otherSource(CurrentSource)].volume = maxVolume;
            CurrentSource = otherSource(CurrentSource);
        }
    }

    private int otherSource(int i)
    {
        if (i == 0) { return 1; }
        else return 0;
    }
}