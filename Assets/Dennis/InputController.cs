using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private KeyCode happyEmotion;
    private KeyCode angryEmotion;
    private KeyCode sadEmotion;
    private KeyCode loveEmotion;

    void Start()
    {
        ConfigureControlButtons();
    }

    void Update()
    {
        CheckInput();
    }

    private void ConfigureControlButtons()
    {
        //here are the emotions mapped on the controller

        //A button of 1 op toetsenbord
        angryEmotion = KeyCode.Joystick1Button0;


        //B button of 2 op toetsenbord
        sadEmotion = KeyCode.Joystick1Button1;

        //X button of 3 op toetsenbord
        happyEmotion = KeyCode.Joystick1Button2;

        //Y button of 4 op toetsenbord
        loveEmotion = KeyCode.Joystick1Button3;
    }

    public void CheckInput()
    {
        if (audioManager.answerTime)
        {
            if (Input.GetKeyDown(angryEmotion) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("I am Angry");
                audioManager.AnswerButton(0);
            }
            else if (Input.GetKeyDown(sadEmotion) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("I am sad");
                audioManager.AnswerButton(1);
            }
            else if (Input.GetKeyDown(happyEmotion) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                Debug.Log("I am happy");
                audioManager.AnswerButton(2);
            }
            else if (Input.GetKeyDown(loveEmotion) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                Debug.Log("I am in love");
                audioManager.AnswerButton(3);
            }
        }
    }
}
