using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    
    [SerializeField]
    private float ResetTime = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
    }

    public void ChangeWeapon()
    {
        this.gameObject.SetActive(false);
    }
}
