using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [SerializeField]
    private float ResetTime = 10.0f;
	// Use this for initialization
	void Start () {
        colliderOfPass = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (setPass)
        {
            colliderOfPass.enabled = false;
        }
        if (!setPass)
        {
            colliderOfPass.enabled = true;
        }
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

    bool setPass;
    BoxCollider2D colliderOfPass;



    //プレイヤーのIsTriggerがOnの側のコリジョンが床のIsTriggerがOnの側のコリジョンと接触している時
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            setPass = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            setPass = false;
        }
    }
}
