using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_A : WeaponBlocController {

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
            case "あ":
            case "ア":
                this.Attack_A(shot);
                return true;

            case "い":
            case "イ":
                this.Attack_I(shot);
                return true;

            case "う":
            case "ウ":
                this.Attack_U(shot);
                return true;

            case "え":
            case "エ":
                this.Attack_E(shot);
                return true;

            case "お":
            case "オ":
                this.Attack_O(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『あ・ア』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_A(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『い・イ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_I(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『う・ウ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_U(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『え・エ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_E(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『お・オ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_O(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
