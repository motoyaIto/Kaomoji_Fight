using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_N : WeaponBlocController {

    GameObject Effect1;     //エフェクト

    protected override void Awake()
    {
        base.Awake();
        switch (mozi)
        {
            case "な":
            case "ナ":
                return;

            case "に":
            case "ニ":
                //爆弾テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/waraningyou");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;
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
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        switch (mozi)
        {
            case "な":
            case "ナ":
                return;

            case "に":
            case "ニ":
                base.Update();
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
                Effect1 = Resources.Load<GameObject>("prefab/Effect/True_Substitution");
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
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『に・ニ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NI(Vector3 shot)
    {
        base.weapon_use = true;

        //武器を非表示にする
        this.transform.GetChild(0).gameObject.SetActive(false);
        owner_cs.ChangeWeapon_Data = false;

        owner_cs.Substitution_Data = true;

        Effect1 = Instantiate(Effect1, this.transform);
        Effect1.transform.position = this.transform.parent.position;
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
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
}
