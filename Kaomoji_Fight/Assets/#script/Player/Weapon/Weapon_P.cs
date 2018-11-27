using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_P : WeaponBlocController {

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
            case "ぱ":
            case "パ":
                this.Attack_PA(shot);
                return true;

            case "ぴ":
            case "ピ":
                this.Attack_PI(shot);
                return true;

            case "ぷ":
            case "プ":
                this.Attack_PU(shot);
                return true;

            case "ぺ":
            case "ペ":
                this.Attack_PE(shot);
                return true;

            case "ぽ":
            case "ポ":
                this.Attack_PO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『ぱ・パ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_PA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぴ・ピ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_PI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぷ・プ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_PU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ペ・ぺ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_PE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ぽ・ポ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_PO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
