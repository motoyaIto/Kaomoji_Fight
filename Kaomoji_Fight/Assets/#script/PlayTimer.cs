using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayTimer : MonoBehaviour {

    [SerializeField]
    private float countTime = 180;
    private float nowTime = 0f;

	// Use this for initialization
	void Start () {
        //Observalbe.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
        //    .Select(coutTime => (int)(coutTime - coutTime));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
