using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBlocController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 SetPosition
    { 
        set
        {
           this.transform.position = value;
        }
    }
}
