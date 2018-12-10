using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon_K : WeaponBlocController {

    private GameObject KA_Effect;       //回復エフェクト
    private float KA_EffectWait = 0.5f; //エフェクトの処理を待つ

    protected override void Awake()
    {
        base.Awake();

        switch (mozi)
        {
            case "か":
            case "カ":
                //回復テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/portion");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;
                return;

            case "き":
            case "キ":
                return ;

            case "く":
            case "ク":
                return;

            case "け":
            case "ケ":
                return;

            case "こ":
            case "コ":
                return;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Update()
    {
        switch (mozi)
        {
            case "か":
            case "カ":
                //回復テキスト
                sprite = Resources.Load<Sprite>("textures/use/Weapon/portion");
                Weapon_spriteFlag = true;
                Weapon_SRenderer.sprite = sprite;

                base.Update();
                return;

            case "き":
            case "キ":
                return;

            case "く":
            case "ク":
                return;

            case "け":
            case "ケ":
                return;

            case "こ":
            case "コ":
                return;
        }
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
                KA_Effect = Resources.Load<GameObject>("prefab/Effect/Recovery");

                DamageValue = -20;
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
        //回復
        PSManager_cs.Effect_myself(this.transform.parent.gameObject, this.gameObject, this.transform.parent.GetComponent<Player>().PlayerNumber_data);

        //回復エフェクト
        KA_Effect = Instantiate(KA_Effect, this.transform) as GameObject;
        KA_Effect.transform.position = new Vector3(this.transform.parent.transform.position.x, this.transform.position.y, 0);

        //エフェクト発生を待って破棄する
        StartCoroutine(base.DelayMethod(KA_EffectWait, () => { Destroy(this.gameObject); }));
        this.transform.parent.GetComponent<Player>().ChangeWeapon_Data = false;
        //仮//////////////////////////////////////////////////////////////////
        //base.SpecifiedOperation_NoneWeapon(shot);
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
