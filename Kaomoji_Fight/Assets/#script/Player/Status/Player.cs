using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using TMPro;
using Constant;

//[RequireComponent(typeof(Contoroller2d))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : RaycastController {

    //状態異常
    public struct StatesAbnormality
    {
        public bool Stan;   //麻痺
        public bool Sleep;  //眠る
    }

    public struct UpStates
    {
        public bool Substitution;   //身代わり
        public bool Invincible;     //無敵
    }

    #region 変数群
    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    private Vector3 velocity;

    private StatesAbnormality ButState;     //バットステータス
    private UpStates statesUp;              //上昇ステータス

    private float moveSpeed = 10f;          // 移動速度
    float Avoidance_time = .0f;             // 回避時間
    private float Invincible_time = 8.0f;   // クールタイム

    private float scroll = 10f;             // 幅
    private float maxflap = 800f;           // ジャンプの高さ（最大）
    private float minflap = 400f;           // ジャンプの高さ（最小）

    private float direction = 0;            // 方向


    private GameObject weapon;

    private bool HaveWeapon = false;        //武器を持っている(true)いない(false)
    private bool Avoidance = false;         // 回避フラグ
    private bool jump = false;              // ジャンプ中か？
    private bool EnteFlag = false;          //あったたオブジェクトがあるか

    private bool isQuitting = false;        // エディタ実行終了時か？

    Contoroller2d controller;               // コントローラー
    bool controller_lock = false;           //コントローラーをロックする
    Rigidbody2D rig = null;
    [HideInInspector]
    public CollisionInfo collisions;
    private PlaySceneManager PSM;

    private new AudioSource audio;
    private new AudioSource audio2;
    private AudioClip pickUp_ac;            //拾う
    private AudioClip shot_ac;              // 投げる音
    private AudioClip walk_ac;              //歩く
    private AudioClip jump_ac;              // ジャンプ
    private AudioClip bomb_ac;



    #endregion


    private void Awake()
    {
        audio = this.GetComponent<AudioSource>();
        audio2 = this.GetComponent<AudioSource>();
        pickUp_ac = (AudioClip)Resources.Load("Sound/SE/Move/pickUp");      //拾う音
        shot_ac = (AudioClip)Resources.Load("Sound/SE/Shooting/launcher");   //投げる音
        walk_ac = (AudioClip)Resources.Load("Sound/SE/Move/walk");          //歩く音
        jump_ac = (AudioClip)Resources.Load("Sound/SE/Jump/jump");           //ジャンプ音  
        bomb_ac = Resources.Load<AudioClip>("Sound/SE/Deth/ded2");
    }

    new void Start()
    {
        //状態異常の初期化
        ButState.Stan = false;

        //上昇ステータス
        statesUp.Invincible = false;
        statesUp.Substitution = false;

        controller = GetComponent<Contoroller2d>();
        PSM = GameObject.Find("PlaySceneManager").transform.GetComponent<PlaySceneManager>();
        rig = GetComponent<Rigidbody2D>();

        // プレイヤー同士の当たり判定をしない
        int P_layer = LayerMask.NameToLayer("Player");
        Physics2D.IgnoreLayerCollision(P_layer, P_layer);

    }

    private void Reset()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 落ちた時の対処
        if (this.transform.position.y <= -50)
        {
            Destroy(this.transform.gameObject);
        }

        //状態異常処理
        //麻痺・眠り
        if (ButState.Stan == true || ButState.Sleep == true)
        {
            return;
        }
        // Controllerの左スティックのAxisを取得            
        Vector2 input = new Vector2(XCI.GetAxis(XboxAxis.LeftStickX, ControlerNamber), XCI.GetAxis(XboxAxis.LeftStickY, ControlerNamber));
        if (input.x > .0f && controller_lock == false)
        {
            //歩く音
            //  if (direction == 0) { this.PlaySound(audio2, walk_ac, 0.2f); }

            direction = 1f;
        }
        else if (input.x < .0f && controller_lock == false)
        {
            //歩く音
            // if (direction == 0) { this.PlaySound(audio2, walk_ac, 0.2f); }

            direction = -1f;
        }
        else if (controller_lock == false)
        {
            //歩く音を止める
            //audio2.Stop();
            direction = 0f;
        }

        if (HaveWeapon && controller_lock == false)
        {
            //武器の位置を持ち変える
            WeaponPositionControll(input);
        }

        //キャラのy軸のdirection方向にscrollの力をかける
        rig.velocity = new Vector2(scroll * direction, rig.velocity.y);


        if (XCI.GetButtonDown(XboxButton.Y, ControlerNamber) && !jump && controller_lock == false)
        {
            // 大ジャンプ
            audio.volume = .2f;
            audio.PlayOneShot(jump_ac);
            rig.AddForce(Vector2.up * maxflap);
            jump = true;

            //if (XCI.GetButtonUp(XboxButton.Y, ControlerNamber) && !jump)
            //{
            //    // 小ジャンプ・・・したかった・・・(´・ω・｀)
            //    rig.AddForce(Vector2.up * minflap);
            //    jump = true;
            //}
        }

        // 回避をしたい
        if (XCI.GetAxis(XboxAxis.RightTrigger, ControlerNamber) < 0.0f && !Avoidance && controller_lock == false)
        {
            // アニメーションに差し替え予定？
            if (!Avoidance)
            {
                if (input.x < .0f)
                {
                    this.transform.position += new Vector3(-5f, 0f);
                }
                else if (input.x > .0f)
                {
                    this.transform.position += new Vector3(5f, 0f);
                }
                Avoidance = true;
            }
        }

        // 回避のクールタイム計測
        if (Avoidance && Avoidance_time <= Invincible_time)
        {
            Avoidance_time += .1f;
        }
        else
        {
            // 回避ができるようにする
            Avoidance = false;
            Avoidance_time = .0f;
        }

        //武器を持っている
        if (HaveWeapon == true)
        {
            //武器の位置を調整
            WeaponBlocController WBController = weapon.gameObject.GetComponent<WeaponBlocController>();
            Vector3 direction = Vector3.zero;

            if (velocity.x < 0.0f)
            {
                direction = Vector3.left;
                WBController.SetPosition = new Vector3(this.transform.position.x - this.transform.localScale.x, this.transform.position.y + this.transform.localScale.y * 2.5f, 0.0f);
            }
            else if (velocity.x > 0.0f)
            {
                direction = Vector3.right;
                WBController.SetPosition = new Vector3(this.transform.position.x + this.transform.localScale.x + 0.5f, this.transform.position.y + this.transform.localScale.y * 2.5f, 0.0f);
            }

            //武器を使用する
            if (XCI.GetButtonDown(XboxButton.B, ControlerNamber))
            {
                WeaponBlocController WB = weapon.GetComponent<WeaponBlocController>();

                WB.Attack(input);
            }

            // 武器を捨てる
            if (XCI.GetButton(XboxButton.X, ControlerNamber))
            {
                this.ChangeWeapon_Data = false;
                Destroy(weapon);
            }
        }

        // Ｒａｙ
        this.RayController();
    }


    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public int faceDir;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }


    /// <summary>
    /// rayを飛ばしてアイテムを取得する
    /// </summary>
    private void RayController()
    {
        // 武器をゲットするかも
        if (XCI.GetButtonDown(XboxButton.B, ControlerNamber))
        {
            //rayの開始地点
            Vector3 ray_initial = new Vector3(this.transform.position.x, this.transform.position.y - this.transform.localScale.y, this.transform.position.x);

            //rayを生成
            Ray2D ray = new Ray2D(ray_initial, Vector2.down);
            //rayを可視化する
            Debug.DrawRay(ray.origin, ray.direction * 0.5f, Color.green, 1.0f);

            //rayに当たったものを取得する
            RaycastHit2D hitFoot = Physics2D.Raycast(ray.origin, Vector2.down, ray.direction.y * 0.5f);

            //ステージから武器に変換
            if (hitFoot.transform.tag == "Stage")
            {
                this.GetWeapon(hitFoot, this.transform.position);
            }
        }
    }

    /// <summary>
    ///  武器を獲得する
    /// </summary>
    /// <param name="hitFoot">足元にあった武器</param>
    /// <param name="directionX">右か左か</param>
    private void GetWeapon(RaycastHit2D hitFoot, Vector2 pos)
    {
        GameObject block = hitFoot.collider.gameObject;
        BlockController block_cs = block.GetComponent<BlockController>();

        //武器を持っていなかったら
        if (HaveWeapon == false && block_cs.Weapon == true && controller_lock == false)
        {
            //拾う音
            this.PlaySound(audio, pickUp_ac, .2f);

            //床を武器として取得
            weapon = Object.Instantiate(block) as GameObject;
            weapon.transform.parent = this.transform;
            weapon.name = "WeaponBlock" + block.name.Substring(block.name.IndexOf("("));
            weapon.tag = tag.Trim();

            //武器画像を表示
            SpriteRenderer weapon_sprite = weapon.transform.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
            weapon_sprite.enabled = true;

            //武器のスクリプトに張り替える
            Destroy(weapon.GetComponent<BlockController>());
            weapon.GetComponent<WeaponBlocController>().enabled = true;

            //オーナー登録
            weapon.GetComponent<WeaponBlocController>().Owner_Data = this.name;
            //床から切り抜く
            block.GetComponent<BlockController>().ChangeWeapon();

            weapon.GetComponent<BoxCollider2D>().enabled = false;

            HaveWeapon = true;

            //プレイヤーの移動する向きに合わせて位置を調整
            this.WeaponPositionControll(pos);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ジャンプ制限
        if (collision.gameObject.CompareTag("Stage"))
        {
            EnteFlag = true;
            jump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (EnteFlag == false)
        {
            jump = true;
        }

        EnteFlag = false;
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDisable()
    {
        if (isQuitting) return;
        PSM.death_player[CNConvert(ControlerNamber)] = false;
    }

    // Controllerの番号をint型で取得
    private int CNConvert(XboxController controlerNum)
    {
        switch (controlerNum)
        {
            case XboxController.First:
                return 0;
            case XboxController.Second:
                return 1;
            case XboxController.Third:
                return 2;
            case XboxController.Fourth:
                return 3;
            default:
                break;
        }
        return 4;
    }

    public bool ChangeWeapon_Data
    {
        set
        {
            HaveWeapon = value;
        }
    }


    /// <summary>
    /// XBXcontrollerの番号を取得
    /// </summary>
    public XboxController GetControllerNamber
    {
        get
        {
            return ControlerNamber;
        }
        set
        {
            ControlerNamber = value;
        }
    }

    public float Directtion_Data
    {
        set
        {
            direction = value;
        }
    }

    public void WeaponPositionControll(Vector2 vec2)
    {
        if (HaveWeapon)
        {
            foreach (Transform child in this.transform)
            {
                if (vec2.x > .0f && vec2.y > .0f && child.name == "TopRight")       //右上
                {
                    weapon.transform.position = child.transform.position;
                }

                else if (vec2.x < .0f && vec2.y > .0f && child.name == "TopLeft")   //左下
                {
                    weapon.transform.position = child.transform.position;
                }
                else if (vec2.x == .0f && vec2.y == .0f && child.name == "Top")     //移動していない
                {
                    weapon.transform.position = child.transform.position;
                }
                else if (vec2.x > .0f && vec2.y < .0f && child.name == "DownRight") //右下
                {
                    weapon.transform.position = child.transform.position;
                }
                else if (vec2.x < .0f && vec2.y < .0f && child.name == "DownLeft")  //左下
                {
                    weapon.transform.position = child.transform.position;
                }
                else if (vec2.x == .0f && vec2.y < .0f && child.name == "Down")     //下
                {
                    weapon.transform.position = child.transform.position;
                }
            }
        }
    }

    /// <summary>
    /// 音を鳴らす
    /// </summary>
    /// <param name="clip">音</param>
    /// <param name="volume">ボリューム</param>
    private void PlaySound(AudioSource audiosource, AudioClip clip, float volume)
    {
        audiosource.volume = volume;
        audiosource.PlayOneShot(clip);
        //audiosource.volume = 1.0f;
    }

    public int PlayerNumber_data
    {
        get
        {
            return CNConvert(ControlerNamber);
        }
    }

    public bool ControllerLock_Data
    {
        set
        {
            controller_lock = value;
        }
    }

    public bool Jump_data
    {
        get
        {
            return jump;
        }
    }

    public bool Stan_Data
    {
        set
        {
            ButState.Stan = value;
        }
    }

    public bool Sleep_Data
    {
        set
        {
            ButState.Sleep = value;
        }
    }

    public bool Substitution_Data
    {
        get
        {
            return statesUp.Substitution;
        }
        set
        {
            statesUp.Substitution = value;
        }
    }

    public bool Invincible_Data
    {
        get
        {
            return statesUp.Invincible;
        }
        set
        {
            statesUp.Invincible = value;
        }
    }  
}
