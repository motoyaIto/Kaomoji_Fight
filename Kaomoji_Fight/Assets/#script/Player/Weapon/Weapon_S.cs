using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_S : WeaponBlocController {

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
            case "さ":
            case "サ":
                this.Attack_SA(shot);
                return true;

            case "し":
            case "シ":
                this.Attack_SI(shot);
                return true;

            case "す":
            case "ス":
                this.Attack_SU(shot);
                return true;

            case "せ":
            case "セ":
                this.Attack_SE(shot);
                return true;

            case "そ":
            case "ソ":
                this.Attack_SO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『さ・サ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_SA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『し・シ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_SI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『す・ス』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_SU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『せ・セ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_SE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『そ・ソ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_SO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
