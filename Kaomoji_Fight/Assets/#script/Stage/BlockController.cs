using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [SerializeField]
    private GameObject PSManager;

    [SerializeField]
    private float ResetTime = 10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
    }

    private void OnDisable()
    {
        Invoke("ReStageBlock", ResetTime);
    }


    public void ReStageBlock()
    {
        this.gameObject.SetActive(true);
    }


    public void ChangeWeapon()
    {
        this.gameObject.SetActive(false);
    }
}
