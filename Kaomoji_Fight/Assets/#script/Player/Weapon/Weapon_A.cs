using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Weapon_A : WeaponBlocController {

    private float I_speed = 5.5f;
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        switch (mozi)
        {
            case "あ":
            case "ア":
                return;

            case "い":
            case "イ":
                if (weapon_use == true)
                {
                    //方向に加速する
                    if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
                    {
                        this.transform.parent.GetComponent<Player>().Directtion_Data = I_speed;
                    }
                    else
                    {
                        this.transform.parent.GetComponent<Player>().Directtion_Data = -I_speed;
                    }

                    //Bボタンを離したら
                    if (XCI.GetButtonUp(XboxButton.B, this.transform.parent.GetComponent<Player>().GetControllerNamber))
                    {
                        this.transform.parent.GetComponent<Player>().ControllerLock_Data = false;

                        this.transform.parent.GetComponent<Player>().ChangeWeapon_Data = false;

                        Destroy(this.gameObject);
                    }
                }
                return;

            case "う":
            case "ウ":
                return;

            case "え":
            case "エ":
                return;

            case "お":
            case "オ":
                return;

            default:
                base.Update();
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
            case "あ":
            case "ア":
                this.Attack_A(shot);
                return true;

            case "い":
            case "イ":
                base.DamageValue = 10;
                this.Attack_I(shot);
                return true;

            case "う":
            case "ウ":
                this.Attack_U(shot);
                return true;

            case "え":
            case "エ":
                this.Attack_E(shot);
                return true;

            case "お":
            case "オ":
                this.Attack_O(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『あ・ア』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_A(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『い・イ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_I(Vector3 shot)
    {
        //左右に入力なしで排除
        if ((this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0 || this.transform.GetComponent<RectTransform>().anchoredPosition.x < 0) == false)
        {
            return;
        }

        this.transform.parent.GetComponent<Player>().ControllerLock_Data = true;

        weapon_use = true;

        //武器を右か左に寄せる
        if(this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
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

        //ウェポンにボックスコライダーをつける
        this.gameObject.AddComponent<BoxCollider2D>();
        BoxCollider2D BCollider2D = this.gameObject.GetComponent<BoxCollider2D>();
        BCollider2D.isTrigger = true;

        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『う・ウ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_U(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『え・エ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_E(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『お・オ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_O(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        switch (mozi)
        {
            case "あ":
            case "ア":
                return;

            case "い":
            case "イ":
                if(base.CheckHit_Rival(collision) == true)
                {
                    if (weapon_use == true)
                    {
                        PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.transform.GetComponent<Player>().PlayerNumber_data);
                    }
                }

                return;

            case "う":
            case "ウ":
                return;

            case "え":
            case "エ":
                return;

            case "お":
            case "オ":
                return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
