using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Y : WeaponBlocController {

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
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
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
}
