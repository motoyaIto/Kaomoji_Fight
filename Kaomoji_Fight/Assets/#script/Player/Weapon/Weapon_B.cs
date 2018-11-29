using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_B : WeaponBlocController {

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
            case "ば":
            case "バ":
                this.Attack_BA(shot);
                return true;

            case "び":
            case "ビ":
                this.Attack_BI(shot);
                return true;

            case "ぶ":
            case "ブ":
                this.Attack_BU(shot);
                return true;

            case "べ":
            case "ベ":
                this.Attack_BE(shot);
                return true;

            case "ぼ":
            case "ボ":
                this.Attack_BO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『ば・バ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_BA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『び・ビ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_BI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぶ・ブ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_BU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『べ・ベ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_BE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぼ・ボ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_BO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
