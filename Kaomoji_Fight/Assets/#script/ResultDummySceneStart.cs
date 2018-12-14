using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultDummySceneStart : MonoBehaviour {

    private ResultData result;

    public RankingData[] ranking = null;

	// Use this for initialization
	void Start () {
        result = new ResultData(105.0f, 10250, "aaa", ranking);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
