using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_G : WeaponBlocController {

    protected override void OnEnable()
    {
        base.OnEnable();
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
            case "が":
            case "ガ":
                this.Attack_GA(shot);
                return true;

            case "ぎ":
            case "ギ":
                this.Attack_GI(shot);
                return true;

            case "ぐ":
            case "グ":
                this.Attack_GU(shot);
                return true;

            case "げ":
            case "ゲ":
                this.Attack_GE(shot);
                return true;

            case "ご":
            case "ゴ":
                this.Attack_GO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『が・ガ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_GA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぎ・ギ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_GI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぐ・グ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_GU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『げ・ゲ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_GE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ご・ゴ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_GO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
