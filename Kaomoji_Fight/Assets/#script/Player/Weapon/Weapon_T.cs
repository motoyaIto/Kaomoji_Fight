using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon_T : WeaponBlocController {

    GameObject TO_SmokeEffect;  //とエフェクトの煙
    GameObject TO_FireEffect;   //と炎が舞い上がるエフェクト

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();

        switch (mozi)
        {
            case "た":
            case "タ":
                return;

            case "ち":
            case "チ":
                return;

            case "つ":
            case "ツ":
                return;

            case "て":
            case "テ":
                return;

            case "と":
            case "ト":
                TO_SmokeEffect = Resources.Load<GameObject>("prefab/Effect/Smoke");
                TO_FireEffect = Resources.Load<GameObject>("prefab/Effect/FireMoment");

                DamageValue = 5;
                return;
        }
    }

    /// <summary>
    /// 攻撃文字
    /// </summary>
    /// <param name="shot">使用した座標</param>
    /// <returns>文字攻撃をしたら(true)していなかったら(fasle)</returns>
    protected override bool AttackMozi(Vector3 shot)
    {
        switch (mozi)
        {
            case "た":
            case "タ":
                this.Attack_TA(shot);
                return true;

            case "ち":
            case "チ":
                this.Attack_TI(shot);
                return true;

            case "つ":
            case "ツ":
                this.Attack_TU(shot);
                return true;

            case "て":
            case "テ":
                this.Attack_TE(shot);
                return true;

            case "と":
            case "ト":
                this.Attack_TO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『た・タ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_TA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ち・チ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_TI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『つ・ツ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_TU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『て・テ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_TE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『と・ト』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_TO(Vector3 shot)
    {
        //地上でない場合は使用できない
        if (owner_cs.Jump_data == true)
        {
            return;
        }

        weapon_use = true;

        //武器を右か左に寄せる
        if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Right")
                {
                    this.transform.position = child.transform.position;
                }
            }
        }
        else
        {
            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Left")
                {
                    this.transform.position = child.transform.position;
                }
            }
        }

        //親から離れる
        owner_cs.ChangeWeapon_Data = false;
        this.transform.parent = null;

        //トラップを設置
        TO_SmokeEffect = Instantiate(TO_SmokeEffect, this.transform) as GameObject;
        TO_SmokeEffect.transform.position = this.transform.position;

        //あたり判定を設置
        BoxCollider2D TO_collider = this.gameObject.AddComponent<BoxCollider2D>();
        TO_collider.offset = new Vector2(0, -0.35f);
        TO_collider.size = new Vector2(1, 0.3f);
        TO_collider.isTrigger = true;

        this.transform.GetChild(0).gameObject.SetActive(false);
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "た":
            case "タ":
                base.OnTriggerEnter2D(collision);
                return;

            case "ち":
            case "チ":
                base.OnTriggerEnter2D(collision);
                return;

            case "つ":
            case "ツ":
                base.OnTriggerEnter2D(collision);
                return;

            case "て":
            case "テ":
                base.OnTriggerEnter2D(collision);
                return;

            case "と":
            case "ト":
                if(collision.tag == "Player" && collision.name != owner)
                {
                    PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.GetComponent<Player>().PlayerNumber_data);

                    //爆発
                    TO_FireEffect = Instantiate(TO_FireEffect, this.transform) as GameObject;
                    TO_FireEffect.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 2f);

                    StartCoroutine(base.DelayMethod(1.5f, () => { Destroy(this.gameObject); }));
                }
                return;
        }
    }
}
