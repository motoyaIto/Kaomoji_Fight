using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWithWeapons : MonoBehaviour {

    private WeaponBlocController WBC;

	// Use this for initialization
	void Start () {
        WBC = gameObject.GetComponent<WeaponBlocController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
