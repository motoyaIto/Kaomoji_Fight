using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Audio;

public class Weapon_Z : WeaponBlocController
{
    private GameObject self_destruct_effect;// 自爆エフェクト

    protected override void Awake()
    {
        base.Awake();

        switch (mozi)
        {
            case "ざ":
            case "ザ":
                break;

            case "じ":
            case "ジ":
                //爆弾テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/bomb");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;
                break;

            case "ず":
            case "ズ":
                break;

            case "ぜ":
            case "ゼ":
                break;

            case "ぞ":
            case "ゾ":
                break;
        }


    }
    protected override void OnEnable()
    {
        base.OnEnable();

        switch (mozi)
        {
            case "ざ":
            case "ザ":
                break;

            case "じ":
            case "ジ":
                DamageValue = 33;

                self_destruct_effect = Resources.Load<GameObject>("prefab/Effect/Explosion");
                break;

            case "ず":
            case "ズ":
                break;

            case "ぜ":
            case "ゼ":
                break;

            case "ぞ":
            case "ゾ":
                break;
        }
    }

    public override void Update()
    {
        base.Update();
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
            case "ざ":
            case "ザ":
                this.Attack_ZA(shot);
                return true;

            case "じ":
            case "ジ":
                this.Attack_ZI(shot);
                return true;

            case "ず":
            case "ズ":
                this.Attack_ZU(shot);
                return true;

            case "ぜ":
            case "ゼ":
                this.Attack_ZE(shot);
                return true;

            case "ぞ":
            case "ゾ":
                this.Attack_ZO(shot);
                return  true;
        }

        return false;
    }

    /// <summary>
    /// 『ざ・ザ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ZA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『じ・ジ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ZI(Vector3 shot)
    {
        var hitEffect = Instantiate(self_destruct_effect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;

        //コライダーを発生させる
        CircleCollider2D EffectCollider = hitEffect.GetComponent<CircleCollider2D>();
        EffectCollider.enabled = true;
        EffectCollider.isTrigger = true;

        //スクリプトを有効にする
        Effect_Explosion Effect_cs = hitEffect.GetComponent<Effect_Explosion>();
        Effect_cs.PSManager_Data = PSManager_cs;
        Effect_cs.OwnerName_Data = owner;
        Effect_cs.DamageValue_Data = DamageValue;
        Effect_cs.enabled = true;

        //PSManager_cs.Effect_myself(this.transform.parent.gameObject, this.gameObject, this.transform.parent.GetComponent<Player>().PlayerNumber_data);

        this.transform.parent.GetComponent<Player>().ChangeWeapon_Data = false;

        Destroy(this.gameObject);
    }

    /// <summary>
    /// 『ず・ズ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ZU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぜ・ゼ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ZE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぞ・ゾ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ZO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
