using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Weapon_N : WeaponBlocController {

    private GameObject Effect1;             //エフェクト
    private float NA_StiffnessTime = 0.5f;  //硬直時間
    private float NA_InstantDeathProbability = 0.01f;//即死の確率
    private int[] NA_InstantDeathNamber;      //ランダムで即死を与える値
    bool NA_setWeapon = false;      //武器を準備しているか

    /// <summary>
    /// 武器を目的の位置にずらす
    /// </summary>
    private void Weapon_bring(string position)
    {
        foreach (Transform child in this.transform.parent.transform)
        {
            if (child.name == position)
            {
                this.gameObject.transform.position = child.transform.position;
            }
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        switch (mozi)
        {
            case "な":
            case "ナ":
                Initializ_NA();
                return;

            case "に":
            case "ニ":
                Initialize_NI();
                return;

            case "ぬ":
            case "ヌ":
                return;

            case "ね":
            case "ネ":
                return;

            case "の":
            case "ノ":
                return;
        }
    }

    /// <summary>
    /// イニシャライズ『な』
    /// </summary>
    private void Initializ_NA()
    {
        NA_setWeapon = true;
        DamageValue = 4;
        this.transform.GetComponent<BoxCollider2D>().isTrigger = true;

        //ナイフテキスト
        sprite = Resources.Load<Sprite>("textures/use/Weapon/knife");
        Weapon_spriteFlag = true;
        Weapon_SRenderer.sprite = sprite;

        //即死を与える値を取得
        NA_InstantDeathNamber = new int[Mathf.FloorToInt(100 * NA_InstantDeathProbability)];
        for(int i = 0; i < NA_InstantDeathNamber.Length; i++)
        {
            NA_InstantDeathNamber[i] = Random.Range(1, 100);

            if (i == 0)
            {
                break;
            }

            //数字のかぶりがないかを検出する
            for (int j = 0; j < NA_InstantDeathNamber.Length; j++)
            {
                if(NA_InstantDeathNamber[i] == NA_InstantDeathNamber[j])
                {
                    i -= 1;
                    break;
                }
            }
           
        }
    }

    /// <summary>
    /// イニシャライズ『に』
    /// </summary>
    private void Initialize_NI()
    {
        Effect1 = Resources.Load<GameObject>("prefab/Effect/True_Substitution");

        //藁人形テキスト
        sprite = Resources.Load<Sprite>("textures/use/Weapon/waraningyou");
        Weapon_spriteFlag = true;
        Weapon_SRenderer.sprite = sprite;
    }


    public override void Update()
    {
        switch (mozi)
        {
            case "な":
            case "ナ":
                Update_NA();
                
                break;

            case "に":
            case "ニ":
                break;

            case "ぬ":
            case "ヌ":
                break;

            case "ね":
            case "ネ":
                break;

            case "の":
            case "ノ":
                break;
        }

        base.Update();
    }

    /// <summary>
    /// アップデート『な』
    /// </summary>
    private void Update_NA()
    {
        if (weapon_use == false)
        {
            //武器を左に寄せる
            if (this.transform.GetComponent<RectTransform>().anchoredPosition.x < 0)
            {
                Weapon_bring("DownLeft");
                return;
            }

            //武器を右に寄せる
            if (this.transform.GetComponent<RectTransform>().anchoredPosition.x > 0)
            {
                Weapon_bring("DownRight");
                return;
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
            case "な":
            case "ナ":
                this.Attack_NA(shot);
                return true;

            case "に":
            case "ニ":
                this.Attack_NI(shot);
                return true;

            case "ぬ":
            case "ヌ":
                this.Attack_NU(shot);
                return true;

            case "ね":
            case "ネ":
                this.Attack_NE(shot);
                return true;

            case "の":
            case "ノ":
                this.Attack_NO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『な・ナ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NA(Vector3 shot)
    {
        weapon_use = true;

        //コントロールをロック
        this.transform.parent.transform.GetComponent<Player>().ControllerLock_Data = true;
        this.transform.parent.transform.GetComponent<Player>().Directtion_Data = 0.0f;
        this.transform.GetComponent<BoxCollider2D>().enabled = true;

        //動作ロックを解除する
        StartCoroutine(base.DelayMethod(NA_StiffnessTime, () => { weapon_use = false; this.transform.parent.transform.GetComponent<Player>().ControllerLock_Data = false; this.transform.GetComponent<BoxCollider2D>().enabled = false; }));


        if (weapon_use)
        {
            // Controllerの左スティックのAxisを取得            
            Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, this.transform.parent.GetComponent<Player>().GetControllerNamber), XCI.GetAxis(XboxAxis.LeftStickY, this.transform.parent.GetComponent<Player>().GetControllerNamber));

            //武器を左に寄せる
            if (input.x < 0.0f && input.y <= 0.7f && input.y >= -0.7f)
            {
                Weapon_bring("Left");
                return;
            }

            //武器を右に寄せる
            if (input.x > 0 && input.y <= 0.7f && input.y >= -0.7f)
            {
                Weapon_bring("Right");
                return;
            }

            //武器を上に寄せる
            if (input.y > 0 && input.x <= 0.7f && input.x >= -0.7f)
            {
                Weapon_bring("Top");
                return;
            }

            //武器を下に寄せる
            if (input.y < 0 && input.x <= 0.7f && input.x >= -0.7f)
            {
                Weapon_bring("Down");
                return;
            }
        }
    }

    /// <summary>
    /// 『に・ニ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NI(Vector3 shot)
    {
        if(this.transform.parent.GetComponent<Player>().Substitution_Data == true)
        {
            Destroy(this.gameObject);
        }
        base.weapon_use = true;

        //武器を中心に寄せる
        this.transform.position = this.transform.parent.position;
        //文字を非表示にする
        this.transform.GetChild(0).gameObject.SetActive(false);

        //他の武器を持てるようにする
        owner_cs.ChangeWeapon_Data = false;

        //身代わりをオンにする
        owner_cs.Substitution_Data = true;

        Effect1 = Instantiate(Effect1, this.transform);
        Effect1.transform.position = this.transform.parent.position;
    }

    /// <summary>
    /// 『ぬ・ヌ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ね・ネ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『の・ノ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (mozi)
        {
            case "な":
            case "ナ":
                Hit_NA(collision.gameObject);
                return;

            case "に":
            case "ニ":
                
                return;

            case "ぬ":
            case "ヌ":
                return;

            case "ね":
            case "ネ":
                return;

            case "の":
            case "ノ":
                return;
        }
    }

    /// <summary>
    /// あたり判定『な』
    /// </summary>
    private void Hit_NA(GameObject player)
    {
        if (player.tag == "Player" && player.name != owner)
        {
            bool InstantDeath = false;

            //ランダムで同じ数字が出たら即死
            for (int i = 0; i < NA_InstantDeathNamber.Length; i++)
            {
                int test = Random.Range(1, 100);
                if (NA_InstantDeathNamber[i] == test)
                {
                    InstantDeath = true;

                    break;
                }
            }

            PSManager_cs.Player_ReceiveDamage(player, this.gameObject, player.GetComponent<Player>().PlayerNumber_data, InstantDeath);
        }
    }

    private void OnDestroy()
    {
        switch (mozi)
        {
            case "な":
            case "ナ":
                if (NA_setWeapon == true)
                {
                    owner_cs.ControllerLock_Data = false;
                }
                return;

            case "に":
            case "ニ":

                return;

            case "ぬ":
            case "ヌ":
                return;

            case "ね":
            case "ネ":
                return;

            case "の":
            case "ノ":
                return;
        }
    }
}
