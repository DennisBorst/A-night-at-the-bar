using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] private AudioClip[] BackgroundSounds;
    [SerializeField] private float FadeStepTime = .01f;

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
            sources[otherSource()].clip = BackgroundSounds[BGIndex];
            sources[otherSource()].Play();

            CurrentSource = otherSource();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentSource == 0)
        {
            sources[CurrentSource].volume += .01f;
            sources[otherSource()].volume -= .01f;
        }
        else if (CurrentSource == 1)
        {
            sources[CurrentSource].volume += .01f;
            sources[otherSource()].volume -= .01f;
        }
    }

    private int otherSource()
    {
        if (CurrentSource == 0) { return 1; }
        else return 0;
    }

    private void Update() //debug controls
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playBackgroundSound(0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playBackgroundSound(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playBackgroundSound(2);
        }
    }
}