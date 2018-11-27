using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M : WeaponBlocController
{
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
            case "ま":
            case "マ":
                this.Attack_MA(shot);
                return true;

            case "み":
            case "ミ":
                this.Attack_MI(shot);
                return true;

            case "む":
            case "ム":
                this.Attack_MU(shot);
                return true;

            case "め":
            case "メ":
                this.Attack_ME(shot);
                return true;

            case "も":
            case "モ":
                this.Attack_MO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『ま・マ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『み・ミ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『む・ム』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『め・メ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_ME(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『も・モ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_MO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
