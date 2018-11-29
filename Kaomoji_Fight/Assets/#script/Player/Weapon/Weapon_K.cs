using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_K : WeaponBlocController {

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
            case "か":
            case "カ":
                this.Attack_KA(shot);
                return true;

            case "き":
            case "キ":
                this.Attack_KI(shot);
                return true;

            case "く":
            case "ク":
                this.Attack_KU(shot);
                return true;

            case "け":
            case "ケ":
                this.Attack_KE(shot);
                return true;

            case "こ":
            case "コ":
                this.Attack_KO(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『か・カ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_KA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『き・キ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_KI(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『く・ク』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_KU(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『け・ケ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_KE(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『こ・コ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_KO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
