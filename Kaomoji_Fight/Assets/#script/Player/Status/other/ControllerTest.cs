using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {

        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            Debug.Log("Button A Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            Debug.Log("Button B Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button2))
        {
            Debug.Log("Button X Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button3))
        {
            Debug.Log("Button Y Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button4))
        {
            Debug.Log("Button LB Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button5))
        {
            Debug.Log("Button RB Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button6))
        {
            Debug.Log("Button Back Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button7))
        {
            Debug.Log("Button START Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button8))
        {
            Debug.Log("L Stick Push Push");
        }

        if (Input.GetKey(KeyCode.Joystick1Button9))
        {
            Debug.Log("R Stick Push");
        }

        float TrigerInput = Input.GetAxis("Triger");
        if (TrigerInput < 0.0f)
        {
            Debug.Log("L Triger");
        }
        else if (TrigerInput > 0.0f)
        {
            Debug.Log("R Triger");
        }

        float HorizontalKeyInput = Input.GetAxis("HorizontalKey");
        if (HorizontalKeyInput < 0.0f)
        {
            Debug.Log("Left Key");
        }
        else if (HorizontalKeyInput > 0.0f)
        {
            Debug.Log("Right Key");
        }

        float VerticalKeyInput = Input.GetAxis("VerticalKey");
        if (VerticalKeyInput < 0.0f)
        {
            Debug.Log("Up Key");
        }
        else if (VerticalKeyInput > 0.0f)
        {
            Debug.Log("Down Key");
        }
    }
}
