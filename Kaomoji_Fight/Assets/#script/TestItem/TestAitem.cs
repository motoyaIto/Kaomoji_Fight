using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAitem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            pos.y += 0.5f;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            pos.y -= 0.5f;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            pos.x += 0.5f;
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pos.x -= 0.5f;
        }

        this.transform.position = pos;
	}
}
