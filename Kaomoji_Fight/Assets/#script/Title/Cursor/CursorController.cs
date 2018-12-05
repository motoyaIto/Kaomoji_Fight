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
    private GameObject TManager;        //タイトルマネージャ
    protected TitleManager TManager_cs;   //タイトルマネージャのCS

    [SerializeField]
    protected XboxController controllerNumber = XboxController.First;
    [SerializeField]
    protected float Difference_x = 0.0f;   //X座標の差(StageSelect:10)
    [SerializeField]
    protected float Difference_y = 0.0f;   //y座標の差(StageSelect:2.37)

    [SerializeField]
    protected int NumberLine = 4;     //行数
    protected int NowNumberLine = 0;  //今いる行
    [SerializeField]
    protected int NumberColumns = 2;  //列数
    protected int NowNumberColumn = 0;//今いる列号

    [SerializeField]
    protected GameObject FirstTarget;                 //最初にカーソルに入れるオブジェクト
    protected Vector2 DifferenceCursor = Vector2.zero;//カーソルとの差

    protected Vector2 LeftStickInput = Vector2.zero;  //Controllerの左スティックのAxisを取得
    protected bool LeftStickflag = false;           //スティックが入力されていない(false)された(true)

    protected virtual void Start ()
    {
        Move_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/cursor2");      //移動音
        Click_clip = (AudioClip)Resources.Load("Sound/SE/Select/Decision/decision2");   //クリック音

        audiosource = this.GetComponent<AudioSource>();

        TManager_cs = TManager.GetComponent<TitleManager>();
    }


    protected virtual void Update()
    {
        if(this.SelectMyMode() == false) { return; }

        // Controllerの左スティックのAxisを取得            
        LeftStickInput = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, controllerNumber), XCI.GetAxis(XboxAxis.LeftStickY, controllerNumber));

        //LeftStickの入力がない時
        if (LeftStickflag == false)
        {
            this.PushButton();
        }
        else
        {
            //スッティックが中心近くまで戻っていたら
            if ((LeftStickInput.y < 0.1f && LeftStickInput.y > -0.1f) && (LeftStickInput.x < 0.1f && LeftStickInput.x > -0.1f))
            {
                LeftStickflag = false;
            }
        }

        //プレイ人数を決定
        if (Input.GetKeyDown(KeyCode.Space) || XCI.GetButtonDown(XboxButton.B, controllerNumber))
        {
            //クリック音
            audiosource.PlayOneShot(Click_clip);

            this.Decide();
        }
    }

    /// <summary>
    /// 自分のシーンが選択されているかまた、コントローラーを操作していいいか
    /// </summary>
    /// <returns>OK(true)No(false)</returns>
    protected abstract bool SelectMyMode();

    /// <summary>
    /// 押せるボタン
    /// </summary>
    protected abstract void PushButton();

    /// <summary>
    /// 決定したときの動作
    /// </summary>
    protected abstract void Decide();

    /// <summary>
    /// 上の入力があったとき
    /// </summary>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_UpButton()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || XCI.GetDPadDown(XboxDPad.Up, controllerNumber) || (LeftStickInput.y > 0.9f && LeftStickflag == false))
        {
            //行数を一つ戻す
            NowNumberLine--;
            //カーソルが行数を超えようとしたら
            if (NowNumberLine < 0)
            {
                NowNumberLine = NumberLine - 1;
            }

            //移動方法
            if (TManager_cs.Mode_Data == TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.Mode_Data == TitleManager.SELECTMODE.STAGESELECT)
            {
                this.Move_RectTransform();
            }
            else
            {
                this.Move_Transform();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 下入力があったとき
    /// </summary>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_DownButton()
    {
        
        if (Input.GetKeyDown(KeyCode.DownArrow) || XCI.GetDPadDown(XboxDPad.Down, controllerNumber) || (LeftStickInput.y < -0.9f && LeftStickflag == false))
        {
            //行数を一つ進める
            NowNumberLine++;

            //カーソルが行数を超えようとしたら
            if (NowNumberLine > NumberLine - 1)
            {
                NowNumberLine = 0;
            }

            //移動方法
            if (TManager_cs.Mode_Data == TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.Mode_Data == TitleManager.SELECTMODE.STAGESELECT)
            {
                this.Move_RectTransform();
            }
            else
            {
                this.Move_Transform();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 右入力があったとき
    /// </summary>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_RightBiutton()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || XCI.GetDPadDown(XboxDPad.Right, controllerNumber) || (LeftStickInput.x > 0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            NowNumberColumn++;

            //ターゲットが配列をオーバーしたら
            if (NowNumberColumn > NumberColumns - 1)
            {
                NowNumberColumn = 0;
            }

            //移動方法
            if (TManager_cs.Mode_Data == TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.Mode_Data == TitleManager.SELECTMODE.STAGESELECT)
            {
                this.Move_RectTransform();
            }
            else
            {
                this.Move_Transform();
            }

            return true;
        }

        return false;
    }
    /// <summary>
    /// 左入力があったとき
    /// </summary>
    /// <returns>移動した(true)してない(false)</returns>
    protected bool Push_LeftBiutton()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || XCI.GetDPadDown(XboxDPad.Left, controllerNumber) || (LeftStickInput.x < -0.9f && LeftStickflag == false))
        {
            //次のターゲット番号に変更
            NowNumberColumn--;

            //ターゲットが配列をオーバーしたら
            if (NowNumberColumn < 0)
            {
                NowNumberColumn = NumberColumns - 1;
            }

            //移動方法
            if (TManager_cs.Mode_Data == TitleManager.SELECTMODE.PLAYERNUM || TManager_cs.Mode_Data == TitleManager.SELECTMODE.STAGESELECT)
            {
                this.Move_RectTransform();
            }
            else
            {
                this.Move_Transform();
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move_RectTransform()
    {
        //移動音
        audiosource.PlayOneShot(Move_clip);

        //ターゲットのy座標に変更
        this.transform.position = FirstTarget.transform.position + new Vector3(Difference_x * NowNumberColumn - DifferenceCursor.x, -(Difference_y * NowNumberLine));
        
        LeftStickflag = true;
    }

    private void Move_Transform()
    {
        //移動音
        audiosource.PlayOneShot(Move_clip);

        //ターゲットのy座標に変更
        this.transform.position = new Vector3(FirstTarget.transform.position.x - 0.6f + Difference_x * NowNumberColumn, (FirstTarget.transform.position.y + Difference_y * NowNumberLine) + 0.35f);

        LeftStickflag = true;
    }
}
