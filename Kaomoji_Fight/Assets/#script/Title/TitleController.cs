using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(XCI.GetButtonDown(XboxButton.B, XboxController.First))
        {
            
        }
	}
}
