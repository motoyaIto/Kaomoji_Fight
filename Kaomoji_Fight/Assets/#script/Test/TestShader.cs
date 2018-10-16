using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour {
    Shader shader;
	// Use this for initialization
	void Start () {
        shader = this.transform.GetComponent<Shader>();

        //Debug.Log(shader.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
