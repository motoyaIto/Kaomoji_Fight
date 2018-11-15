using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControll : MonoBehaviour {

    [SerializeField]
    public GameObject hitEffect;        // ヒットエフェクト
    [SerializeField]
    public GameObject dedEffect;        // 死亡エフェクト

    public Transform[] hitPoints;         // ヒット地点
    public Transform[] dedPoints;         // 死亡地点


    public void HitEffect()
    {
        var hitobj = Instantiate(hitEffect, transform.position + transform.forward, Quaternion.identity) as GameObject;
    }

    //public void DedEffect()
    //{
    //    foreach (Transform dedPos in dedPoints)
    //    {
    //        GameObject ded = Instantiate(dedEffect,               // エフェクトの生成
    //            dedPos.position, transform.rotation) as GameObject;

    //        Destroy(ded, 1f);                                             // 1秒後に消す
    //    }
    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var hitobj = Instantiate(hitEffect, transform.position + transform.forward, Quaternion.identity) as GameObject;
        var dedobj = Instantiate(dedEffect, transform.position + transform.forward, Quaternion.identity) as GameObject;
    }
}
