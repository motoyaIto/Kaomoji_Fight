using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon_Y : WeaponBlocController {

    float YA_count = 0;             //カウント
    float YA_StiffnessTime = 0.8f;  //硬直時間
    Transform YA_SpriteTransform;   //スプライトのトランスフォーム
    bool YA_setWeapon = false;      //武器を準備しているか
   
    protected override void OnEnable()
    {
        base.OnEnable();

        switch (mozi)
        {
            case "や":
            case "ヤ":
                Initializ_YA();
                
                return;

            case "ゆ":
            case "ユ":
                return;

            case "よ":
            case "ヨ":
                return;
        }
    }

    /// <summary>
    /// イニシャライズ『や』
    /// </summary>
    private void Initializ_YA()
    {
        //槍テキスト
        sprite = Resources.Load<Sprite>("textures/use/Weapon/buki_yari");
        Weapon_spriteFlag = true;
        Weapon_SRenderer.sprite = sprite;

        //画像の角度サイズを調整
        YA_SpriteTransform = this.transform.GetChild(1);
        YA_SpriteTransform.rotation = Quaternion.Euler(YA_SpriteTransform.localRotation.x, YA_SpriteTransform.localRotation.y, -55);
        YA_SpriteTransform.localScale = new Vector3(0.8f, 0.8f, 1);

        YA_setWeapon = true;
        //武器のダメージ量を設定
        DamageValue = 8;

        //文字を非表示にする
        this.transform.GetChild(0).gameObject.SetActive(false);
        //イズトリガーをオンに
        this.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        //あたり判定を調整
        this.transform.GetComponent<BoxCollider2D>().size = new Vector2(3.94f, 0.84f);

        //武器を取得
        Weapon = Resources.Load<GameObject>("prefab/Weapon/Spear");

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
            case "や":
            case "ヤ":
                Update_YA();
                break;

            case "ゆ":
            case "ユ":
                break;

            case "よ":
            case "ヨ":
                break;
        }

        base.Update();

    }

    /// <summary>
    /// アップデート『や』
    /// </summary>
    private void Update_YA()
    {
        if (weapon_use == true)
        {
            YA_count += Time.deltaTime;

            //硬直時間を過ぎたら
            if (YA_count > YA_StiffnessTime)
            {
                owner_cs.ControllerLock_Data = false;
                weapon_use = false;

                this.transform.GetComponent<BoxCollider2D>().enabled = false;
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
            case "や":
            case "ヤ":
                this.Attack_YA(shot);
                return true;

            case "ゆ":
            case "ユ":
                this.Attack_YU(shot);
                return true;

            case "よ":
            case "ヨ":
                this.Attack_YO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『や・ヤ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_YA(Vector3 shot)
    {
        weapon_use = true;
        owner_cs.ControllerLock_Data = true;
        owner_cs.Directtion_Data = 0.0f;

        this.transform.GetComponent<BoxCollider2D>().enabled = true;

        YA_count = 0.0f;

        //武器を右か左に寄せる
        if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
        {
            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Right")
                {
                    this.transform.position = new Vector3(child.transform.position.x + 1.5f, child.transform.position.y, child.transform.position.z);

                    //画像の向きを合わせる
                    Weapon_SRenderer.flipX = false;
                    Weapon_SRenderer.flipY = false;
                }
            }
        }
        else
        {
            foreach (Transform child in this.transform.parent.transform)
            {
                if (child.name == "Left")
                {
                    this.transform.position = new Vector3(child.transform.position.x - 1.5f, child.transform.position.y, child.transform.position.z);

                    //画像の向きを合わせる
                    Weapon_SRenderer.flipX = true;
                    Weapon_SRenderer.flipY = true;
                }
            }
        }
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ゆ・ユ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_YU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『よ・ヨ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_YO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "や":
            case "ヤ":
                if (collision.tag == "Player" && collision.name != owner)
                {
                    //プレイヤーにダメージを与える
                    PSManager_cs.Player_ReceiveDamage(collision.gameObject, this.gameObject, collision.transform.GetComponent<Player>().PlayerNumber_data);
                }
                return;

            case "ゆ":
            case "ユ":
                return;

            case "よ":
            case "ヨ":
                return;
        }
    }

    private void OnDestroy()
    {
        switch (mozi)
        {
            case "や":
            case "ヤ":
                if (YA_setWeapon == true)
                {
                    owner_cs.ControllerLock_Data = false;
                }
                return;

            case "ゆ":
            case "ユ":
                return;

            case "よ":
            case "ヨ":
                return;
        }
    }
}
