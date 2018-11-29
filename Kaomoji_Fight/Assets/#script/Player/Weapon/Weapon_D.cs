using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_D : WeaponBlocController {

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
            case "だ":
            case "ダ":
                this.Attack_DA(shot);
                return true;

            case "ぢ":
            case "ヂ":
                this.Attack_DI(shot);
                return true;

            case "づ":
            case "ヅ":
                this.Attack_DU(shot);
                return true;

            case "で":
            case "デ":
                this.Attack_DE(shot);
                return true;

            case "ど":
            case "ド":
                this.Attack_DO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『だ・ダ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぢ・ヂ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『づ・ヅ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『で・デ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ど・ド』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_DO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
