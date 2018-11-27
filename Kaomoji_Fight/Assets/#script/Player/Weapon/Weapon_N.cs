using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_N : WeaponBlocController {

    protected override void Awake()
    {
        base.Awake();
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
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
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
