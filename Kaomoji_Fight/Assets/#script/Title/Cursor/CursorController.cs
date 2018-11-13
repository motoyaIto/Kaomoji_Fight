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
    private float Difference_x = 0.0f;   //X座標の差
    [SerializeField]
    private float Difference_y = 0.0f;   //y座標の差
     
    [SerializeField]
    private int NumberLine = 4;     //行数
    private int NowNumberLine = 0;  //今いる行
    [SerializeField]
    private int NumberColumns = 2;  //列数
    private int NowNumberColumn = 0;//今いる列号

    [SerializeField]
    private GameObject FirstTarget;         //最初にカーソルに入れるオブジェクト
    private Vector2 DifferenceCursor = Vector2.zero;//カーソルとの差

    protected bool LeftStickflag = false;   //スティックが入力されていない(false)された(true)

    protected virtual void Start ()
    {
        Move_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/cursor2");  //移動音
        Click_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/decision2");  //クリック音

        audiosource = this.GetComponent<AudioSource>();

        TManager_cs = TManager.GetComponent<TitleManager>();

        DifferenceCursor = this.transform.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta;
       
        //初期座標を入力
        this.transform.position = FirstTarget.transform.position + new Vector3(Difference_x * NowNumberColumn - DifferenceCursor.x, Difference_y * NowNumberLine);
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
            //行数を一つ戻す
            NowNumberLine--;
            //カーソルが行数を超えようとしたら
            if (NowNumberLine < 0)
            {
                NowNumberLine = NumberLine - 1;
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
            //行数を一つ進める
            NowNumberLine++;

            //カーソルが行数を超えようとしたら
            if (NowNumberLine > NumberLine - 1)
            {
                NowNumberLine = 0;
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
            //target_number += Numberbefore_Linebreak;

            //ターゲットが配列をオーバーしたら
            //if (target_number >= target_pos.Length)
            //{
            //    target_number = target_number - Numberbefore_Linebreak * NumberColumns;
            //}

            //Move();

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
            //target_number -= Numberbefore_Linebreak;

            //ターゲットが配列をオーバーしたら
            //if (target_number < 0)
            //{
            //    target_number = Numberbefore_Linebreak * NumberColumns + target_number;
            //}

            //Move();

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
        this.transform.position = FirstTarget.transform.position + new Vector3(Difference_x * NowNumberColumn - DifferenceCursor.x, (Difference_y - DifferenceCursor.y * NowNumberLine)  * NowNumberLine);
        Debug.Log(FirstTarget.transform.localPosition) ;
        LeftStickflag = true;
    }
}
