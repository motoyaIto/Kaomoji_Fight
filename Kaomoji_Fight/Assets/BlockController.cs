using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// ｂｏｘの文字を取得する
    /// </summary>
    /// <returns>ｂｏｘの文字を返す</returns>
    public string GetBlockMozi()
    {
        //ボックスの下のテキストを取得する
        GameObject textdata = this.transform.Find("Text").gameObject;
        //文字を返す
        return textdata.GetComponent<TextMesh>().text;
        
    }
}
