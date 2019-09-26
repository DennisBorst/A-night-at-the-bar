using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    private KeyCode happyEmotion;
    private KeyCode angryEmotion;
    private KeyCode neutralEmotion;
    private KeyCode nervousEmotion;

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

        //A button
        happyEmotion = KeyCode.Joystick1Button0;

        //B button
        angryEmotion = KeyCode.Joystick1Button1;

        //X button
        neutralEmotion = KeyCode.Joystick1Button2;

        //Y button
        nervousEmotion = KeyCode.Joystick1Button3;
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(happyEmotion))
        {
            Debug.Log("I am happy");
        }
        else if (Input.GetKeyDown(angryEmotion))
        {
            Debug.Log("I am angry");
        }
        else if (Input.GetKeyDown(neutralEmotion))
        {
            Debug.Log("I am neutral");
        }
        else if (Input.GetKeyDown(nervousEmotion))
        {
            Debug.Log("I am nervous");
        }
    }
}
