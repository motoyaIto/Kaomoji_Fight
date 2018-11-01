using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class CharSelectControll : MonoBehaviour {

    [SerializeField, Header("コントローラー番号")]
    private XboxController ControlerNamber = XboxController.First;//何番目のコントローラーを適用するか

    [SerializeField]
    private Camera camera;      //カメラ
    private AudioSource sound01;
    bool cursor = false;
    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
    }
	
	// Update is called once per frame
	void Update () {

        if (camera.transform.position.x >= -17.9 && camera.transform.position.x <= -17.7)
        {
            cursor = true;
        }
        if (cursor == true)
        {
            
            /* if(全員決定したら)
             * {            
             *      camera.transform.position = new Vector3(17.8f, 0, 0);  //ステージセレクトに移動
             * }
             */
            //人数セレクトに戻る処理
            if (Input.GetKeyDown(KeyCode.Backspace) || XCI.GetButtonDown(XboxButton.A, ControlerNamber))
            {
                sound01.PlayOneShot(sound01.clip);
                camera.transform.position = new Vector3(0, 0, 0);  //人数セレクトに戻る
                cursor = false;
            }
        }
    }
}
