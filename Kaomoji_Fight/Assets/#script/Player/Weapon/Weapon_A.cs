using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using System;


public class Weapon_A : WeaponBlocController {

    private GameObject A_effect;            // 爆発エフェクト
    private float A_thrust = 2.5f;          // 爆弾の推進力
    private bool A_firstHit_owner = false;  //オーナーに一度当たったら(true)まだだったら(false)
    private Vector2 A_speed = Vector2.zero; //飛ばしてる弾の速度
    private int A_count = 0;                //跳ね返った回数
    private bool A_rebound = false;         //跳ね返った(treu)返っていない(false)

    private float I_speed = 5.5f;           //突進スピード

    protected override void Awake()
    {
        base.Awake();
        switch (mozi)
        {
            case "あ":
            case "ア":
                //爆弾テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/bomb");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;

                break;

            case "い":
            case "イ":
                //角テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/horn");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;
                break;

            case "う":
            case "ウ":
                break;

            case "え":
            case "エ":
                break;

            case "お":
            case "オ":
                break;
        }
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        switch (mozi)
        {
            case "あ":
            case "ア":
                base.DamageValue = 15;

                A_effect = Resources.Load<GameObject>("prefab/Effect/Explosion");

                return;

            case "い":
            case "イ":
                base.DamageValue = 10;
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
                return;
        }
    }

    public override void Update()
    {
        switch (mozi)
        {
            case "あ":
            case "ア":
                if (weapon_use == true)
                {
                    Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, owner_cs.GetControllerNamber), XCI.GetAxis(XboxAxis.LeftStickY, owner_cs.GetControllerNamber));

                    //Bボタンを離したときに左スティックを投げたい方向に入力していたら
                    if (XCI.GetButtonUp(XboxButton.B, owner_cs.GetControllerNamber) && (input.x != 0 && input.y != 0))
                    {
                        owner_cs.ControllerLock_Data = false;

                        owner_cs.ChangeWeapon_Data = false;

                        // 親から離れる
                        this.transform.parent = null;

                        //ウェポンにボックスコライダーをつける
                        CircleCollider2D CCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
                        CCollider2D.isTrigger = true;
                        
                        //リジッドボディをセット
                        Rigidbody2D rig2d = this.transform.gameObject.AddComponent<Rigidbody2D>();
                        rig2d.gravityScale = .00f;

                        // 投げる
                        A_speed = input * A_thrust;
                        this.transform.GetComponent<Rigidbody2D>().velocity = A_speed;
                    }

                    //Bボタンを離したら
                    if(XCI.GetButtonUp(XboxButton.B, owner_cs.GetControllerNamber))
                    {
                        owner_cs.ControllerLock_Data = false;
                        weapon_use = false;
                    }
                }
                base.Update();

                return;

            case "い":
            case "イ":
                if (weapon_use == true)
                {
                    //方向に加速する
                    if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
                    {
                        owner_cs.Directtion_Data = I_speed;
                    }
                    else
                    {
                        owner_cs.Directtion_Data = -I_speed;
                    }

                    //Bボタンを離したら
                    if (XCI.GetButtonUp(XboxButton.B, owner_cs.GetControllerNamber))
                    {
                        owner_cs.ControllerLock_Data = false;

                        owner_cs.ChangeWeapon_Data = false;

                        Destroy(this.gameObject);
                    }
                }
                base.Update();

                return;

            case "う":
            case "ウ":
                base.Update();
                return;

            case "え":
            case "エ":
                base.Update();
                return;

            case "お":
            case "オ":
                base.Update();
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
        //空中にいる間は投げれない
        if (owner_cs.Jump_data == true)
        {
            return;
        }

        owner_cs.ControllerLock_Data = true;
        owner_cs.Directtion_Data = 0.0f;
        weapon_use = true;

        //武器を真ん中でかまえる
        foreach (Transform child in this.transform.parent.transform)
        {
            if (child.name == "Top")
            {
                this.transform.position = child.transform.position;
            }
        }

       
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
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

        owner_cs.ControllerLock_Data = true;

        weapon_use = true;

        //武器を右か左に寄せる
        if(this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            // 90度右に回転
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));

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
            // 90度左に回転
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Left")
                {
                    this.transform.position = child.transform.position;
                }
            }
        }

        //ウェポンにボックスコライダーをつける
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
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


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "あ":
            case "ア":
                //当たったのがプレイヤーだったら
                if (collision.transform.tag == "Player")
                {
                    //最初に投げるときのあたり判定をスキップ
                    if (A_firstHit_owner == false)
                    {
                        A_firstHit_owner = true;

                        return;
                    }
                    //プレイヤーにダメージを与える
                    PSManager_cs.AllPlayer_Damage(collision.gameObject, this.gameObject, collision.transform.GetComponent<Player>().PlayerNumber_data);

                    //エフェクト
                    var hitEffect = Instantiate(A_effect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;

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

                    Destroy(this.gameObject);

                    return;
                }

                //当たったのがステージだったら
                if (collision.transform.tag == "Stage" && A_rebound == false)
                {
                    A_count++;

                    //10回以上跳ね返ったら爆発する
                    if (A_count >= 10)
                    {
                        //エフェクト
                        var hitEffect = Instantiate(A_effect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;

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

                        Destroy(this.gameObject);
                    }

                    this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.Reflect(A_speed, new Vector2(A_speed.x, A_speed.y * Mathf.Pow(-1, A_count)));
                }
                return;

            case "い":
            case "イ":
                if (base.CheckHit_Rival(collision) == true)
                {
                    if (weapon_use == true)
                    {
                        //プレイヤーにダメージを与える
                        PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.transform.GetComponent<Player>().PlayerNumber_data);
                    }
                }

                return;

            case "う":
            case "ウ":
                base.OnTriggerEnter2D(collision);
                return;

            case "え":
            case "エ":
                base.OnTriggerEnter2D(collision);
                return;

            case "お":
            case "オ":
                base.OnTriggerEnter2D(collision);
                return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "あ":
            case "ア":
               if(A_rebound == true && collision.tag == "Stage")
                {
                    A_rebound = false;
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
}
