using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_W : WeaponBlocController {

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
            case "わ":
            case "ワ":
                this.Attack_WA(shot);
                return true;

            case "を":
            case "ヲ":
                this.Attack_WO(shot);
                return true;

            case "ん":
            case "ン":
                this.Attack_NN(shot);
                return true;
        }

        return false;
    }

    /// <summary>
    /// 『わ・ワ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_WA(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『を・ヲ』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_WO(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }

    /// <summary>
    /// 『ん・ン』で攻撃
    /// </summary>
    /// <param name="shot">使用した座標</param>
    private void Attack_NN(Vector3 shot)
    {
        //仮//////////////////////////////////////////////////////////////////
        base.SpecifiedOperation_NoneWeapon(shot);
        //仮//////////////////////////////////////////////////////////////////
    }
}
