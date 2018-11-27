using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Weapon_Z : WeaponBlocController
{
    private GameObject self_destruct_effect;// 自爆エフェクト

    protected override void Awake()
    {
        self_destruct_effect = Resources.Load<GameObject>("prefab/Effect/Explosion");

        base.Awake();
    }

    public override void Update()
    {

    }

    public override void Attack(Vector3 shot, float thrust)
    {
        switch(mozi)
        {
            case "ざ":
            case "ザ":
                break;

            case "じ":
            case "ジ":
                this.Attack_ZI();
                break;

            case "ず":
            case "ズ":
                break;

            case "ぜ":
            case "ゼ":
                break;

            case "ぞ":
            case "ゾ":
                break;

            default:
                base.Attack(shot, thrust);
                break;
        }
       
    }

    private void Attack_ZI()
    {
        var hitobj = Instantiate(self_destruct_effect, this.transform.position + transform.forward, Quaternion.identity) as GameObject;
        DamageValue = 50;
        PSManager_cs.Player_ReceiveDamage(this.transform.parent.gameObject, this.gameObject, this.transform.parent.GetComponent<Player>().PlayerNumber_data);
    }
}
