using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [HideInInspector] public bool tutorialIsPlaying;
    [SerializeField] private GameObject audioManagerObject;

    [Space] //Audio stuff
    [SerializeField] private AudioClip tutorialStarter;
    [SerializeField] private AudioClip tutorialEnd;

    //In de array is 0 de boze knop toelichten, 1 de blije, 2 de verliefde en 3 de verdrietige
    [SerializeField] private AudioClip[] allInputNarratorSentences;
    //In de array is 0 het goede antwoord en 1 het slechte antwoord
    [SerializeField] private AudioClip[] angryClip;
    [SerializeField] private AudioClip[] sadClip;
    [SerializeField] private AudioClip[] loveClip;
    [SerializeField] private AudioClip[] happyClip;

    private int buttonPressed;
    private int tutorialStatus = -1;
    private bool allowToPressButton;
    private bool explanationIsPlaying;
    private bool endTutorialIsPlaying = false;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();

        audioManagerObject.SetActive(false);
        tutorialIsPlaying = true;

        source.clip = tutorialStarter;
        source.Play();
    }

    private void Update()
    {
        if (endTutorialIsPlaying)
        {
            TutorialEnding();
            return;
        }
        else
        {
            CheckForNewSentences();
        }

        if (tutorialStatus == 4 && !source.isPlaying)
        {
            endTutorialIsPlaying = true;
            source.clip = tutorialEnd;
            source.Play();
        }
    }

    public void InputController(int _buttonPressed)
    {
        buttonPressed = _buttonPressed;

        if (allowToPressButton)
        {
            allowToPressButton = false;
            TutorialStatus();
        }
    }

    private void PlayInputExplanation(int _tutorialStatus)
    {
        source.clip = allInputNarratorSentences[_tutorialStatus];
        source.Play();
    }

    private void TutorialStatus()
    {
        if(tutorialStatus == 0)
        {
            if(buttonPressed == 0)
            {
                source.clip = angryClip[0];
                source.Play();
                explanationIsPlaying = false;
                tutorialStatus++;
            }
            else if(buttonPressed != 0)
            {
                source.clip = angryClip[1];
                source.Play();
            }
        }
        else if (tutorialStatus == 1)
        {
            if (buttonPressed == 1)
            {
                source.clip = sadClip[0];
                source.Play();
                explanationIsPlaying = false;
                tutorialStatus++;
            }
            else if (buttonPressed != 1)
            {
                source.clip = sadClip[1];
                source.Play();
            }
        }
        else if (tutorialStatus == 2)
        {
            if (buttonPressed == 3)
            {
                source.clip = loveClip[0];
                source.Play();
                explanationIsPlaying = false;
                tutorialStatus++;
            }
            else if (buttonPressed != 3)
            {
                source.clip = loveClip[1];
                source.Play();
            }
        }
        else if (tutorialStatus == 3)
        {
            if (buttonPressed == 2)
            {
                source.clip = happyClip[0];
                source.Play();
                explanationIsPlaying = false;
                tutorialStatus++;
            }
            else if (buttonPressed != 2)
            {
                source.clip = happyClip[1];
                source.Play();
            }
        }
    }

    private void CheckForNewSentences()
    {
        if (source.clip == tutorialStarter && !source.isPlaying)
        {
            tutorialStatus = 0;
            explanationIsPlaying = false;
        }

        if (tutorialStatus == 0)
        {
            if (!source.isPlaying && !explanationIsPlaying)
            {
                explanationIsPlaying = true;
                PlayInputExplanation(0);
            }

            if (!source.isPlaying)
            {
                allowToPressButton = true;
            }
        }
        else if (tutorialStatus == 1)
        {
            if (!source.isPlaying && !explanationIsPlaying)
            {
                explanationIsPlaying = true;
                PlayInputExplanation(1);
            }

            if (!source.isPlaying)
            {
                allowToPressButton = true;
            }
        }
        else if (tutorialStatus == 2)
        {
            if (!source.isPlaying && !explanationIsPlaying)
            {
                explanationIsPlaying = true;
                PlayInputExplanation(2);
            }

            if (!source.isPlaying)
            {
                allowToPressButton = true;
            }
        }
        else if (tutorialStatus == 3)
        {
            if (!source.isPlaying && !explanationIsPlaying)
            {
                explanationIsPlaying = true;
                PlayInputExplanation(3);
            }

            if (!source.isPlaying)
            {
                allowToPressButton = true;
            }
        }
    }

    private void TutorialEnding()
    {
        Debug.Log("Playing the end part");
        
        if(source.clip == tutorialEnd && !source.isPlaying)
        {
            audioManagerObject.SetActive(true);
            tutorialIsPlaying = false;
            this.enabled = false;
        }
    }
}
