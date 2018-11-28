using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.Audio;

public class CreateStage : MonoBehaviour
{
    //武器になる文字群
    private string[] WEAPO_MOZI =
    {
        "あ", "い", "う", "え", "お",
        "か", "き", "く", "け", "こ",
        "さ", "し", "す", "せ", "そ",
        "た", "ち", "つ", "て", "と",
        "な", "に", "ぬ", "ね", "の",
        "は", "ひ", "ふ", "へ", "ほ",
        "ま", "み", "む", "め", "も",
        "や",       "ゆ",       "よ",
        "ら", "り", "る", "れ", "ろ",
        "わ",       "を",       "ん",

        "が", "ぎ", "ぐ", "げ", "ご",
        "ざ", "じ", "ず", "ぜ", "ぞ",
        "だ", "ぢ", "づ", "で", "ど",
        "ば", "び", "ぶ", "べ", "ぼ",

        "ぱ", "ぴ", "ぷ", "ぺ", "ぽ",

        "ア", "イ", "ウ", "エ", "オ",
        "カ", "キ", "ク", "ケ", "コ",
        "サ", "シ", "ス", "セ", "ソ",
        "タ", "チ", "ツ", "テ", "ト",
        "ナ", "ニ", "ヌ", "ネ", "ノ",
        "ハ", "ヒ", "フ", "ヘ", "ホ",
        "マ", "ミ", "ム", "メ", "モ",
        "ヤ",       "ユ",       "ヨ",
        "ラ", "リ", "ル", "レ", "ロ",
        "ワ",       "ヲ",       "ン",

        "ガ", "ギ", "グ", "ゲ", "ゴ",
        "ザ", "ジ", "ズ", "ゼ", "ゾ",
        "ダ", "ヂ", "ヅ", "デ", "ド",
        "バ", "ビ", "ブ", "ベ", "ボ",

        "パ", "ピ", "プ", "ペ", "ポ"
    };

    private GameObject StageBlock;  //ステージ
    private Material Weapon_mate;   //武器material

    private Weapon_Z weapon_z_cs;   //ざ行のcs 


    void Start()
    {
        //ブロックの配列
        GameObject[] StageBlocks = null;
        //テキストの文字数
        int textnam = 0;

        //x座標の移動
        int xCount = 0;
        //y座標の移動
        int yCount = 0;


        //テキスト一覧の取得
        string text = PlayData.Instance.StageText;

        textnam = text.Length;

        //文字数分の配列
        StageBlocks = new GameObject[textnam];
        //文字を表示するボックスをResourcesから読み込む
        StageBlock = (GameObject)Resources.Load("prefab/Stage/StageBlock");
        Weapon_mate = Resources.Load<Material>("Material/StageBlock_Weapon");




        //一文字ずつ設定する
        for (int i = 0; i < textnam; i++)
        {
            string mozi = text.Substring(i, 1);
            StageBlocks[i] = StageBlock;
           

            CreateStageBlock(mozi, StageBlocks[i], ref xCount, ref yCount);
        }
    }

    private void CreateStageBlock(string mozi, GameObject StageBlock, ref int x , ref int y)
    {
        //改行が入っていないとき
        if (mozi != "\r" && mozi != "\n")
        {
            //スペースが入っていないとき
            if (mozi != " " && mozi != "　")
            {
                //新しく作るオブジェクトの座標
                Vector3 pos = new Vector3(
                       this.transform.position.x + StageBlock.transform.localScale.x / 2 + StageBlock.transform.localScale.x * x,
                       this.transform.position.y + StageBlock.transform.localScale.y / 2 + StageBlock.transform.localScale.y * y,
                        0.0f);

                //オブジェクトを生成する
                StageBlock = Instantiate(StageBlock, pos, Quaternion.identity, this.transform);
                //ボックスの下のテキストを取得する
                GameObject textdata = StageBlock.transform.Find("Text").gameObject;
                //テキストに文字を書き込む
                textdata.GetComponent<TextMeshPro>().text = mozi;
                StageBlock.name = "StageBlock" + "(" + mozi + ")";
                // RectTransformを追加
                StageBlock.AddComponent<RectTransform>();
                 //weaponだったら
                if (Array.IndexOf(WEAPO_MOZI, mozi) >= 0)
                {
                    //指定の武器用スクリプトをセットする
                    GameObject weapon = StageBlock;
                    SetWeapon_sc(mozi, weapon);

                    //武器の文字用マテリアルに変更
                    Material StageBlock_WeaponMateral = Weapon_mate;
                   StageBlock.GetComponent<Renderer>().material = StageBlock_WeaponMateral;
                    

                    //武器フラグを立てる
                    BlockController Block_cs = StageBlock.transform.GetComponent<BlockController>();
                    Block_cs.Weapon = true;
                }
                
            }
            //右に一文字ずらす
            x++;
        }
        else
        {
            //一行下にずらす
            y--;
            //文字位置をスタートに戻す
            x = 0;
        }
    }

    private void SetWeapon_sc(string mozi, GameObject weapon)
    {
        switch(mozi)
        {
            case "あ": case "い": case "う": case "え": case "お":
            case "ア": case "イ": case "ウ": case "エ": case "オ":
                weapon.AddComponent<Weapon_A>().enabled = false;

                break;

            case "か": case "き": case "く": case "け": case "こ":
            case "カ": case "キ": case "ク": case "ケ": case "コ":
                weapon.AddComponent<Weapon_K>().enabled = false;
                break;

            case "さ": case "し": case "す": case "せ": case "そ":
            case "サ": case "シ": case "ス": case "セ": case "ソ":
                weapon.AddComponent<Weapon_S>().enabled = false;
                break;

            case "た": case "ち": case "つ": case "て": case "と":
            case "タ": case "チ": case "ツ": case "テ": case "ト":
                weapon.AddComponent<Weapon_T>().enabled = false;
                break;

            case "な": case "に": case "ぬ": case "ね": case "の":
            case "ナ": case "ニ": case "ヌ": case "ネ": case "ノ":
                weapon.AddComponent<Weapon_N>().enabled = false;
                break;

            case "は": case "ひ": case "ふ": case "へ": case "ほ":
            case "ハ": case "ヒ": case "フ": case "ヘ": case "ホ":
                weapon.AddComponent<Weapon_H>().enabled = false;
                break;

            case "ま": case "み": case "む": case "め": case "も":
            case "マ": case "ミ": case "ム": case "メ": case "モ":
                weapon.AddComponent<Weapon_M>().enabled = false;
                break;

            case "や": case "ゆ": case "よ":
            case "ヤ": case "ユ": case "ヨ":
                weapon.AddComponent<Weapon_Y>().enabled = false;
                break;

            case "ら": case "り": case "る": case "れ": case "ろ":
            case "ラ": case "リ": case "ル": case "レ": case "ロ":
                weapon.AddComponent<Weapon_R>().enabled = false;
                break;

            case "わ": case "を": case "ん":
            case "ワ": case "ヲ": case "ン":
                weapon.AddComponent<Weapon_W>().enabled = false;
                break;


            case "が": case "ぎ": case "ぐ": case "げ": case "ご":
            case "ガ": case "ギ": case "グ": case "ゲ": case "ゴ":
                weapon.AddComponent<Weapon_G>().enabled = false;
                break;

            case "ざ": case "じ": case "ず": case "ぜ": case "ぞ":
            case "ザ": case "ジ": case "ズ": case "ゼ": case "ゾ":
                weapon.AddComponent<Weapon_Z>().enabled = false;
                break;

            case "だ": case "ぢ": case "づ": case "で": case "ど":
            case "ダ": case "ヂ": case "ヅ": case "デ": case "ド":
                weapon.AddComponent<Weapon_D>().enabled = false;
                break;

            case "ば": case "び": case "ぶ": case "べ": case "ぼ":
            case "バ": case "ビ": case "ブ": case "ベ": case "ボ":
                weapon.AddComponent<Weapon_B>().enabled = false;
                break;

            case "ぱ": case "ぴ": case "ぷ": case "ぺ": case "ぽ":
            case "パ": case "ピ": case "プ": case "ペ": case "ポ":
                weapon.AddComponent<Weapon_P>().enabled = false;

                break;
            default:
                weapon.AddComponent<WeaponBlocController>().enabled = false;
                break;

        }
    }
}
