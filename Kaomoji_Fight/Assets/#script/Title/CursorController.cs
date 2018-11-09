using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


abstract public class CursorController : MonoBehaviour
{

    protected AudioSource audiosource;  //オーディオ
    private AudioClip Move_clip;        //移動音
    protected AudioClip Click_clip;     //クリック音

    [SerializeField]
    protected GameObject TManager;        //タイトルマネージャ
    protected TitleManager TManager_cs;   //タイトルマネージャのCS

    [SerializeField]
    protected GameObject[] Target;        //ターゲット
    [SerializeField]
    protected int Numberbefore_Linebreak = 4;//改行直前のステージ番号
    [SerializeField]
    private int NumberColumns = 2;//列数
    private int NowNumberColumn = 0;//今いる行番号


    protected bool LeftStickflag = false;//スティックが入力されていない(false)された(true)

    protected Vector3[] target_pos;   //ターゲットのy座標
    protected int target_number = 0;  //ターゲットの番号
	protected virtual void Start () {
        Move_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/cursor2");  //移動音
        Click_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/decision2");  //クリック音

        audiosource = this.GetComponent<AudioSource>();
        target_pos = new Vector3[Target.Length];

        TManager_cs = TManager.GetComponent<TitleManager>();
    }


    protected abstract void Update();

    /// <summary>
    /// 上の入力があったとき
    /// </summary>
    /// <param name="input">スティック</param>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_UpButton(Vector2 input)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, XboxController.First) || (input.y > 0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            target_number--;

            //ターゲットが配列をオーバーしたら
            if (target_number < NowNumberColumn * Numberbefore_Linebreak)
            {
                target_number = Numberbefore_Linebreak * (NowNumberColumn + 1) - 1;
            }

            Move();

            return true;
        }

        return false;
    }

    /// <summary>
    /// 下入力があったとき
    /// </summary>
    /// <param name="input">スティック</param>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_DownButton(Vector2 input)
    {
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, XboxController.First) || (input.y < -0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            target_number++;

            //ターゲットが配列をオーバーしたら
            if (target_number >= Numberbefore_Linebreak * (NowNumberColumn + 1))
            {
                target_number = target_number - Numberbefore_Linebreak;
            }

            Move();

            return true;
        }

        return false;
    }

    /// <summary>
    /// 右入力があったとき
    /// </summary>
    /// <param name="input">スティック</param>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_RightBiutton(Vector2 input)
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || XCI.GetDPadDown(XboxDPad.Right, XboxController.First) || (input.x > 0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            target_number += Numberbefore_Linebreak;

            //ターゲットが配列をオーバーしたら
            if (target_number >= target_pos.Length)
            {
                target_number = target_number - Numberbefore_Linebreak * NumberColumns;
            }

            Move();

            return true;
        }

        return false;
    }
    /// <summary>
    /// 左入力があったとき
    /// </summary>
    /// <param name="input">スティック</param>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_LeftBiutton(Vector2 input)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || XCI.GetDPadDown(XboxDPad.Left, XboxController.First) || (input.x < -0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            target_number -= Numberbefore_Linebreak;

            //ターゲットが配列をオーバーしたら
            if (target_number < 0)
            {
                target_number = Numberbefore_Linebreak * NumberColumns + target_number;
            }

            Move();

            return true;
        }

        return false;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        //移動音
        audiosource.PlayOneShot(Move_clip);

        //ターゲットのy座標に変更
        this.transform.position = target_pos[target_number];

        NowNumberColumn = target_number / Numberbefore_Linebreak;

        LeftStickflag = true;
    }
}
