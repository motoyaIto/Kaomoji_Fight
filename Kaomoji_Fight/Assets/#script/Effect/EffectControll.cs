using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControll : MonoBehaviour {

    public GameObject hitEffect;        // ヒットエフェクト
    public GameObject dedEffect;        // 死亡エフェクト

    public Transform[] hitPoints;         // ヒット地点
    public Transform[] dedPoints;         // 死亡地点


    public void HitEffect()
    {        
        foreach (Transform hitPos in hitPoints)
        {
            GameObject hit = Instantiate(hitEffect,               // エフェクトの生成
                hitPos.position, transform.rotation) as GameObject;

            Destroy(hit, 1f);                                             // 1秒後に消す
        }
    }

    public void DedEffect()
    {
        foreach (Transform dedPos in dedPoints)
        {
            GameObject ded = Instantiate(dedEffect,               // エフェクトの生成
                dedPos.position, transform.rotation) as GameObject;

            Destroy(ded, 1f);                                             // 1秒後に消す
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
