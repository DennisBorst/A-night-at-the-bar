using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    //The higher the number the angrier your mate is, the lower the number the happier your mate is
    [Range(-100, 100)]
    [SerializeField] private int currentAngryMeter;

    [System.Serializable]
    public class Choises
    {
        public AudioClip conversationStarter;

        public AudioClip[] answers;
        public int[] increaseAngryMeter;

        public AudioClip[] reaction1;
        public AudioClip[] reaction2;
        public AudioClip[] reaction3;
        public AudioClip[] reaction4;
    }

    [SerializeField] private float answerTimer = 5f;
    private float maxAnswerTime;

    public Choises[] choises;

    private AudioSource source;

    private Choises currentChoice;
    private int choiceIndex = 0;

    private bool hasAnswered = false;
    private bool answerTime = false;
    private bool answerIsPlaying = false;
    private int answerNumber;
    private int answerButton;

    private int reactionIndex;
    

    void Start()
    {
        source = GetComponent<AudioSource>();
        maxAnswerTime = answerTimer;

        currentChoice = choises[choiceIndex];
        source.clip = currentChoice.conversationStarter;
        source.Play();
    }

    void Update()
    {
        CheckForNewChoise();

        if(source.clip == currentChoice.conversationStarter && !source.isPlaying)
        {
            answerTime = true;
        }

        if (answerTime)
        {
            answerTimer = Timer(answerTimer);

            if(answerTimer < 0)
            {
                answerTime = false;
            }

            LoadNextSentences();
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    private void LoadNextSentences()
    {
        inputController.CheckInput();

        if (!hasAnswered)
        {
            return;
        }

        if (!answerIsPlaying)
        {
            answerIsPlaying = true;
            source.Play();
        }

        if (source.clip == currentChoice.answers[answerNumber] && !source.isPlaying)
        {
            if(answerNumber == 0)
            {
                Reaction1();
            }
            else if (answerNumber == 1)
            {
                Reaction2();
            }
            else if (answerNumber == 2)
            {
                Reaction3();
            }
            else if (answerNumber == 3)
            {
                Reaction4();
            }
        }
    }
    
    public void AnswerButton(int buttonPressed)
    {
        source.clip = currentChoice.answers[buttonPressed];
        answerNumber = buttonPressed;
        currentAngryMeter += currentChoice.increaseAngryMeter[buttonPressed];
        hasAnswered = true;
    }

    private void CheckForNewChoise()
    {
        if(source.clip == currentChoice.reaction1[reactionIndex] || source.clip == currentChoice.reaction2[reactionIndex] || 
            source.clip == currentChoice.reaction3[reactionIndex] || source.clip == currentChoice.reaction4[reactionIndex] && 
            !source.isPlaying)
        {
            answerTimer = maxAnswerTime;
            hasAnswered = false;
            answerIsPlaying = false;

            choiceIndex++;
            currentChoice = choises[choiceIndex];

            source.clip = currentChoice.conversationStarter;
            source.Play();
        }
    }

    private void Reaction1()
    {
        if (currentAngryMeter < -30)
        {
            //Happy audio clip
            reactionIndex = 0;
            source.clip = currentChoice.reaction1[reactionIndex];
            source.Play();
        }
        else if (currentAngryMeter > -30 && currentAngryMeter < 30)
        {
            //Neutral audio clip
            reactionIndex = 1;
            source.clip = currentChoice.reaction1[reactionIndex];
            source.Play();
        }
        else
        {
            //Angry audio clip
            reactionIndex = 2;
            source.clip = currentChoice.reaction1[reactionIndex];
            source.Play();
        }
    }

    private void Reaction2()
    {
        if (currentAngryMeter < -30)
        {
            //Happy audio clip
            reactionIndex = 0;
            source.clip = currentChoice.reaction2[reactionIndex];
            source.Play();
        }
        else if (currentAngryMeter > -30 && currentAngryMeter < 30)
        {
            //Neutral audio clip
            reactionIndex = 1;
            source.clip = currentChoice.reaction2[reactionIndex];
            source.Play();
        }
        else
        {
            //Angry audio clip
            reactionIndex = 2;
            source.clip = currentChoice.reaction2[reactionIndex];
            source.Play();
        }
    }

    private void Reaction3()
    {
        if (currentAngryMeter < -30)
        {
            //Happy audio clip
            reactionIndex = 0;
            source.clip = currentChoice.reaction3[reactionIndex];
            source.Play();
        }
        else if (currentAngryMeter > -30 && currentAngryMeter < 30)
        {
            //Neutral audio clip
            reactionIndex = 1;
            source.clip = currentChoice.reaction3[reactionIndex];
            source.Play();
        }
        else
        {
            //Angry audio clip
            reactionIndex = 2;
            source.clip = currentChoice.reaction3[reactionIndex];
            source.Play();
        }
    }

    private void Reaction4()
    {
        if (currentAngryMeter < -30)
        {
            //Happy audio clip
            reactionIndex = 0;
            source.clip = currentChoice.reaction4[reactionIndex];
            source.Play();
        }
        else if (currentAngryMeter > -30 && currentAngryMeter < 30)
        {
            //Neutral audio clip
            reactionIndex = 1;
            source.clip = currentChoice.reaction4[reactionIndex];
            source.Play();
        }
        else
        {
            //Angry audio clip
            reactionIndex = 2;
            source.clip = currentChoice.reaction4[reactionIndex];
            source.Play();
        }
    }
}
