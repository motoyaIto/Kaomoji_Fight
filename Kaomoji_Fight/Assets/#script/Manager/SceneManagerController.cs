using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class SceneManagerController
{
    private static AsyncOperation ope = null;//シーンを格納

    //各シーン名
    public enum SceneName
    {
        LOGO,
        TITLE,
      
        PLAY,
        RESULT
    };

	// Use this for initialization
	public static void LoadScene ()
    {
        Cursor.lockState = CursorLockMode.Confined;
       
        //次のシーンを読み込む
        if (SceneManager.GetActiveScene().buildIndex < (int)SceneName.RESULT)
        {
            ope = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            ope = SceneManager.LoadSceneAsync(1);
        }

        //自動再生を無効にする
        ope.allowSceneActivation = false;
    }

    /// <summary>
    /// シーン切替
    /// </summary>
    public static void ChangeScene()
    {
        //再生する
        ope.allowSceneActivation = true;
        
    }


}
