using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon_H : WeaponBlocController {

    float HA_RotationalDistanc = 0.0f;  //回転距離
    float HA_Acceleration = 1.12f;       //加速度
    string HA_LeftRight = "";                //右か左か
    Transform HA_SpriteTransform;   //スプライトのトランスフォーム

    protected override void OnEnable()
    {
        
        base.OnEnable();
        switch (mozi)
        {
            case "は":
            case "ハ":
                Initializ_HA();
                return;

            case "ひ":
            case "ヒ":
                return;

            case "ふ":
            case "フ":
                return;

            case "へ":
            case "ヘ":
                return;

            case "ほ":
            case "ホ":
                return;
        }
    }

    private void Initializ_HA()
    {
        //ハンマーテキスト
        sprite = Resources.Load<Sprite>("textures/use/Weapon/hammer");
        Weapon_spriteFlag = true;
        Weapon_SRenderer.sprite = sprite;

        //画像のサイズを調整
        HA_SpriteTransform = this.transform.GetChild(1);
        HA_SpriteTransform.localPosition = new Vector3(0f, 0.77f, 1);
        HA_SpriteTransform.localScale = new Vector3(0.7f, 2.0f, 1);

        DamageValue = 18;

        //あたり判定を調整
        this.transform.GetComponent<BoxCollider2D>().offset = new Vector2(0, 1.2f);
        this.transform.GetComponent<BoxCollider2D>().size = new Vector2(2.3f, 1.5f);

        //文字を非表示にする
        this.transform.GetChild(0).gameObject.SetActive(false);
        //イズトリガーをオンに
        this.transform.GetComponent<BoxCollider2D>().isTrigger = true;

        //武器を取得
        Weapon = Resources.Load<GameObject>("prefab/Weapon/Hammer");

        //武器の生成
        Weapon = Instantiate(Weapon);
        Weapon.transform.parent = this.transform;
        Weapon.transform.localPosition = new Vector3(0, 0, 0);

        //武器文字を調整
        foreach (Transform Child in Weapon.transform)
        {
            Child.GetChild(0).GetComponent<TextMeshPro>().text = mozi;
        }
    }

    public override void Update()
    {
        switch (mozi)
        {
            case "は":
            case "ハ":
                this.Update_HA();
                break;

            case "ひ":
            case "ヒ":
                break;

            case "ふ":
            case "フ":
                break;

            case "へ":
            case "ヘ":
                break;

            case "ほ":
            case "ホ":
                break;
        }

        base.Update();
    }

    private void Update_HA()
    {
        if(weapon_use == true)
        {
            //左に振るか右に振るか
            if (HA_RotationalDistanc < 90.0f　&& HA_RotationalDistanc > -90.0f)
            {
                if (HA_LeftRight == "Left")
                {
                    //時間毎に加速する
                    HA_RotationalDistanc += Time.deltaTime;
                    HA_RotationalDistanc *= HA_Acceleration;
                }
                else
                {
                    //時間毎に加速する
                    HA_RotationalDistanc -= Time.deltaTime;
                    HA_RotationalDistanc *= HA_Acceleration;
                }

                //ハンマーを回す
                this.transform.rotation = Quaternion.AngleAxis(HA_RotationalDistanc, new Vector3(0, 0, 1));
            }
            else
            {
                StartCoroutine(base.DelayMethod(0.5f, () =>
                {
                    owner_cs.ControllerLock_Data = false;
                    weapon_use = false;

                    this.transform.GetComponent<BoxCollider2D>().enabled = false;

                    //元の角度に戻す
                    HA_RotationalDistanc = 0.0f;
                    this.transform.rotation = Quaternion.AngleAxis(HA_RotationalDistanc, new Vector3(0, 0, 1));

                    HA_LeftRight = "";
                }));
              
            }
        }
        else
        {
            //武器を使用してないときは常に上でかまえる
            foreach(Transform child in this.transform.parent)
            {
                if(child.name == "Top")
                {
                    this.transform.position = child.position;
                }
            }
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
            case "は":
            case "ハ":
                this.Attack_HA(shot);
                return true;

            case "ひ":
            case "ヒ":
                this.Attack_HI(shot);
                return true;

            case "ふ":
            case "フ":
                this.Attack_HU(shot);
                return true;

            case "へ":
            case "ヘ":
                this.Attack_HE(shot);
                return true;

            case "ほ":
            case "ホ":
                this.Attack_HO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『は・ハ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_HA(Vector3 shot)
    {
        owner_cs.ControllerLock_Data = true;
        owner_cs.Directtion_Data = 0.0f;
        weapon_use = true;

        this.transform.GetComponent<BoxCollider2D>().enabled = true;

        //右上かまえる
        if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            foreach (Transform child in this.transform.parent)
            {
                if (child.name == "TopRight")
                {
                    this.transform.position = child.position;
                    HA_LeftRight = "Right";

                    return;
                }
            }
        }

        //左上かまえる
        if (this.transform.GetComponent<RectTransform>().anchoredPosition.x < 0)
        {
            foreach (Transform child in this.transform.parent)
            {
                if (child.name == "TopLeft")
                {
                    this.transform.position = child.position;
                    HA_LeftRight = "Left";

                    return;
                }
            }
        }
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ひ・ヒ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_HI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ふ・フ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_HU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『へ・ヘ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_HE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ほ・ホ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_HO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "は":
            case "ハ":
                if (collision.tag == "Player" && collision.name != owner)
                {
                    //プレイヤーにダメージを与える
                    PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.transform.GetComponent<Player>().PlayerNumber_data);
                }
                break;

            case "ひ":
            case "ヒ":
                break;

            case "ふ":
            case "フ":
                break;

            case "へ":
            case "ヘ":
                break;

            case "ほ":
            case "ホ":
                break;
        }
    }
}
