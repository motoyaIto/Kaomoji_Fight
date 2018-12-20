using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constant;

public class Weapon_M : WeaponBlocController
{
    float MA_count = 0.0f;          //カウント
    float MA_ouldcount = 0.0f;      //前の時間
    float MA_StiffnessTime = 5.0f;  //硬直時間
    float MA_MAXRecovery = 50;      //最大回復量
    SpriteRenderer MA_Futon;                //ふとん

    float test = 0;

    protected override void Awake()
    {
        base.Awake();

        switch (mozi)
        {
            case "ま":
            case "マ":
                //枕テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/makura");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;
                return;

            case "み":
            case "ミ":
                return;

            case "む":
            case "ム":
                return;

            case "め":
            case "メ":
                return;

            case "も":
            case "モ":
                return;
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        base.Update();

        switch (mozi)
        {
            case "ま":
            case "マ":
                if (weapon_use == true)
                {
                    MA_count += Time.deltaTime;

                    if(Mathf.Floor(MA_count * 10) / 10 > Mathf.Floor(MA_ouldcount * 10) / 10)
                    {
                        //回復
                        PSManager_cs.Effect_myself(this.transform.parent.gameObject, this.gameObject, this.transform.parent.GetComponent<Player>().PlayerNumber_data);

                        MA_ouldcount = MA_count;

                        test += DamageValue;
                    }

                    //制限時間を超えたら止める
                    if (MA_count > MA_StiffnessTime)
                    {
                        //眠り状態を解除する
                        owner_cs.Sleep_Data = false;
                        owner_cs.ChangeWeapon_Data = false;

                        Destroy(this.gameObject);
                    }
                }
                return;

            case "み":
            case "ミ":
                return;

            case "む":
            case "ム":
                return;

            case "め":
            case "メ":
                return;

            case "も":
            case "モ":
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
            case "ま":
            case "マ":
                DamageValue = -((MA_MAXRecovery / MA_StiffnessTime) / 10);

                this.Attack_MA(shot);
                return true;

            case "み":
            case "ミ":
                this.Attack_MI(shot);
                return true;

            case "む":
            case "ム":
                this.Attack_MU(shot);
                return true;

            case "め":
            case "メ":
                this.Attack_ME(shot);
                return true;

            case "も":
            case "モ":
                this.Attack_MO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『ま・マ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MA(Vector3 shot)
    {
        weapon_use = true;

        MA_count = 0.0f;

        this.transform.position = this.transform.parent.position;
        //枕の調整
        Weapon_spriteFlag = false;
        Weapon_SRenderer.color = new Vector4(Weapon_SRenderer.color.r, Weapon_SRenderer.color.g, Weapon_SRenderer.color.b, 1f);
        Weapon_SRenderer.transform.position = new Vector3(Weapon_SRenderer.transform.position.x - 2.0f, Weapon_SRenderer.transform.position.y - 1.2f, Weapon_SRenderer.transform.position.z);
        Weapon_SRenderer.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 90.0f);
        Weapon_SRenderer.flipX = true;

        //移動を止める
        this.transform.parent.GetComponent<Player>().Directtion_Data = 0.0f;
        //眠るステータスを与える
        this.transform.parent.GetComponent<Player>().Sleep_Data = true;

        //布団作成
        Sprite FutonSprite = Resources.Load<Sprite>("textures/use/Weapon/huton");
        MA_Futon = new GameObject("Sprite").AddComponent<SpriteRenderer>();
        MA_Futon.sprite = FutonSprite;
        //布団枕と一緒
        MA_Futon.transform.parent = Weapon_Sprites.transform;
        //布団の調整
        MA_Futon.gameObject.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y - 1.2f, this.transform.position.z);
        MA_Futon.sortingOrder = 1;

       

        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『み・ミ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『む・ム』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『め・メ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ME(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『も・モ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
