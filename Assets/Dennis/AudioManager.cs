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

    [System.Serializable]
    public class Choises
    {
        public AudioClip[] answers;
        public int[] increaseAngryMeter;

        public AudioClip[] reaction;
        public int[] conversationNumber;
    }

    [SerializeField] private float answerTimer = 5f;
    private float maxAnswerTime;

    public Choises[] choises;
    private AudioSource source;
    private Choises currentConversationNumber;

    [HideInInspector] public bool answerTime = false;
    private bool hasAnswered = false;
    private int answerNumber;

    private bool hasReacted = false;

    private ConversationState conversationState;

    private enum ConversationState
    {
        QuestionState,
        AnswerState,
        ReactionState
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

            if (answerTimer < 0)
            {
                answerTime = false;
                hasAnswered = true;
                answerNumber = Random.Range(0, 4);
                currentAngryMeter += currentConversationNumber.increaseAngryMeter[answerNumber];
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
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }

    public void AnswerButton(int buttonPressed)
    {
        answerNumber = buttonPressed;
        currentAngryMeter += currentConversationNumber.increaseAngryMeter[answerNumber];
        hasAnswered = true;
    }

    private void ReactionState()
    {
        if (!hasReacted)
        {
            hasReacted = true;
            source.clip = currentConversationNumber.reaction[answerNumber];
            source.Play();
        }

        if (!source.isPlaying)
        {
            answerTimer = maxAnswerTime;
            hasReacted = false;
            currentConversationNumber = choises[currentConversationNumber.conversationNumber[answerNumber]];
            conversationState = ConversationState.AnswerState;
        }
    }
}
