using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [SerializeField]
    private float ResetTime = 10.0f;

    bool setPass;
    BoxCollider2D colliderOfPass;

    private bool WeaponBlock = false;//武器(true)ではない(false)

    // Use this for initialization

    void Start () {
        colliderOfPass = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnDisable()
    {
        //ResetTime後、床を復帰する
        Invoke("ReStageBlock", ResetTime);
    }

    /// <summary>
    /// 床の復帰処理
    /// </summary>
    public void ReStageBlock()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 床を抜く
    /// </summary>
    public void ChangeWeapon()
    {
        this.gameObject.SetActive(false);
    }


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

    /// <summary>
    /// 武器(true)ではない(false)
    /// </summary>
    public bool Weapon
    {
        get
        {
            return WeaponBlock;
        }

        set
        {
            WeaponBlock = value;
        }
    }
}
