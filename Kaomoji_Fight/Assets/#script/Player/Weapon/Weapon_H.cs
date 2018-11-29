using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_H : WeaponBlocController {

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
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
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
}
