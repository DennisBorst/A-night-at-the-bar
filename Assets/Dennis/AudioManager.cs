using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    //The higher the number the angrier your mate is, the lower the number the happier your mate is
    [Range(-100, 100)]
    [SerializeField] private int currentAngryMeter;

    [SerializeField] private AudioClip conversationStarter;
    [SerializeField] private AudioClip countDownSound;

    [System.Serializable]
    public class Choises
    {
        public AudioClip[] answers;
        public int[] increaseAngryMeter;

        public AudioClip[] reaction;
        public int[] conversationNumber;

        public bool[] conversationEnded;
    }

    //Overall important stuff
    public Choises[] choises;
    private AudioSource source;
    private Choises currentConversationNumber;
    private ConversationState conversationState;

    //Everything about answering
    [SerializeField] private float answerTimer = 5f;
    private float maxAnswerTime;
    [HideInInspector] public bool answerTime = false;
    private bool hasAnswered = false;
    private int answerNumber;

    //Everything about reactions
    private bool hasReacted = false;

    //Everything about the ending
    [Range(0, 0.01f)]
    [SerializeField] private float fadeSpeed;
    private float endFadeTime = 1f;

    private enum ConversationState
    {
        QuestionState,
        AnswerState,
        ReactionState,
        EndState
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        maxAnswerTime = answerTimer;

        currentConversationNumber = choises[0];
        source.clip = conversationStarter;
        source.Play();
    }

    void Update()
    {
        if(source.clip == conversationStarter && !source.isPlaying)
        {
            conversationState = ConversationState.AnswerState;
        }

        SwitchStates();
    }

    private void SwitchStates()
    {
        switch (conversationState)
        {
            case ConversationState.AnswerState:
                AnswerState();
                break;
            case ConversationState.ReactionState:
                ReactionState();
                break;
            case ConversationState.EndState:
                GameHasEnded();
                break;
            default:
                break;
        }
    }

    private void AnswerState()
    {
        if (!hasAnswered)
        {
            answerTime = true;
            answerTimer = Timer(answerTimer);

            if (!source.isPlaying)
            {
                source.clip = countDownSound;
                source.Play();
            }

            if (answerTimer < 0)
            {
                answerTime = false;
                hasAnswered = true;
                answerNumber = Random.Range(0, 4);
            }

            return;
        }

        if (source.clip == currentConversationNumber.answers[answerNumber] && !source.isPlaying)
        {
            conversationState = ConversationState.ReactionState;
        }

        source.clip = currentConversationNumber.answers[answerNumber];

        if (source.clip == currentConversationNumber.answers[answerNumber] && !source.isPlaying)
        {
            source.Play();
            currentAngryMeter += currentConversationNumber.increaseAngryMeter[answerNumber];
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    public void AnswerButton(int _buttonPressed)
    {
        answerNumber = _buttonPressed;
        hasAnswered = true;
        answerTime = false;
    }

    private void ReactionState()
    {
        
        if (!hasReacted)
        {
            hasReacted = true;
            source.clip = currentConversationNumber.reaction[answerNumber];
            source.Play();
            if (currentConversationNumber.conversationEnded[answerNumber] == true)
            {
                conversationState = ConversationState.EndState;
            }
        }

        if (!source.isPlaying)
        {
            answerTimer = maxAnswerTime;
            hasAnswered = false;
            hasReacted = false;
            currentConversationNumber = choises[currentConversationNumber.conversationNumber[answerNumber]];
            conversationState = ConversationState.AnswerState;
        }
    }

    private void GameHasEnded()
    {
        Debug.Log("game has ended");
        endFadeTime -= fadeSpeed; 
        source.volume = endFadeTime;
        if(endFadeTime <= 0)
        {
            endFadeTime = 0;
        }
    }
}
