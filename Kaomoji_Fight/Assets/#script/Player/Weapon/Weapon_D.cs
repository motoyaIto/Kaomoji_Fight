using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_D : WeaponBlocController {

    GameObject D_needle;
    Vector2 D_needleSpeed = new Vector2(50.0f, 0.0f);

    protected override void OnEnable()
    {
        base.OnEnable();
        switch (mozi)
        {
            case "だ":
            case "ダ":
                return;

            case "ぢ":
            case "ヂ":
                return;

            case "づ":
            case "ヅ":
                return;

            case "で":
            case "デ":
                base.DamageValue = 5;

                D_needle = Resources.Load<GameObject>("prefab/Weapon/Taser_needle");
                D_needle = Instantiate(D_needle, this.transform);
                D_needle.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.2f, 0);
                D_needle.transform.localScale = new Vector3(D_needle.transform.localScale.x, D_needle.transform.localScale.y * 0.5f, D_needle.transform.localScale.z);

                BoxCollider2D NeedleCollider = D_needle.GetComponent<BoxCollider2D>();
                NeedleCollider.isTrigger = true;
                return;

            case "ど":
            case "ド":
                return;
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
            case "だ":
            case "ダ":
                this.Attack_DA(shot);
                return true;

            case "ぢ":
            case "ヂ":
                this.Attack_DI(shot);
                return true;

            case "づ":
            case "ヅ":
                this.Attack_DU(shot);
                return true;

            case "で":
            case "デ":
                this.Attack_DE(shot);
                return true;

            case "ど":
            case "ド":
                this.Attack_DO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『だ・ダ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぢ・ヂ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『づ・ヅ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『で・デ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DE(Vector3 shot)
    {
        //左右に入力なしで排除
        if ((this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0 || this.transform.GetComponent<RectTransform>().anchoredPosition.x < 0) == false)
        {
            return;
        }

        weapon_use = true;

        //針を表示
        D_needle.SetActive(true);

        //各パラメータを与える
        TaserNeedle D_needle_cs = D_needle.GetComponent<TaserNeedle>();
        D_needle_cs.PSManager_Data = PSManager_cs;
        D_needle_cs.Owner_Data = base.Owner_Data;
        D_needle_cs.DamageValue_Data = DamageValue;

        //武器を右か左に寄せる
        if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Right")
                {
                    this.transform.position = child.transform.position;

                    //針を射出する
                    D_needle.transform.GetComponent<Rigidbody2D>().velocity = D_needleSpeed;
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

                    //射出向きを修正
                    SpriteRenderer needleSprite = D_needle.transform.GetComponent<SpriteRenderer>();
                    needleSprite.flipY = true;

                    //針を射出する
                    D_needle.transform.GetComponent<Rigidbody2D>().velocity = -D_needleSpeed;
                }
            }
        }


        owner_cs.ChangeWeapon_Data = false;

        //針だけを飛ばす
        D_needle.transform.parent = null;

        Destroy(this.gameObject);
    }

   
    /// <summary>
    /// 『ど・ド』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
