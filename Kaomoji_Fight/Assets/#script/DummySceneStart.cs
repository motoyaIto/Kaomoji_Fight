using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySceneStart : MonoBehaviour {

    PlayersData playerdata;
    private void Awake()
    {
        playerdata = new PlayersData(2);
    }

    // Use this for initialization
    void Start () {
        SceneManagerController.LoadScene();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
