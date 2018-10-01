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
     
    public string GetBlockMozi
    {
        // ｂｏｘの文字を取得する
        get
        {
           // ボックスの下のテキストを取得する
            GameObject textdata = this.transform.Find("Text").gameObject;
            GameObject bloc = this.transform.gameObject;

            bloc.SetActive(false);//ブロックを消す

            //文字を返す
            return textdata.GetComponent<TextMesh>().text;
        }
    }
}
