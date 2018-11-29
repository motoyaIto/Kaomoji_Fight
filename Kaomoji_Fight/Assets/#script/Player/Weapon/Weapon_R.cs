using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_R : WeaponBlocController {

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
            case "ら":
            case "ラ":
                this.Attack_RA(shot);
                return true;

            case "り":
            case "リ":
                this.Attack_RI(shot);
                return true;

            case "る":
            case "ル":
                this.Attack_RU(shot);
                return true;

            case "れ":
            case "レ":
                this.Attack_RE(shot);
                return true;

            case "ろ":
            case "ロ":
                this.Attack_RO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『ら・ラ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_RA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『り・リ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_RI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『る・ル』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_RU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『れ・レ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_RE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ろ・ロ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_RO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
