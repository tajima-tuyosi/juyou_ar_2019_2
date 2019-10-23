using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameManagerにアタッチ。ボタンの機能に関するスクリプト
public class Button : MonoBehaviour
{
    private Game_Manager game_manager_script;

    void Start()
    {
        game_manager_script = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }


    //------------------------------------------------------------------------
    //----------------------コレクション一覧画面のボタン----------------------
    //------------------------------------------------------------------------

    //コレクションの取得状況を確認するボタン。canvas下にコレクションUIを表示する（Eventtriggerで管理）
    public void Collection_Check_Open_Event()
    {
        Debug.Log("コレクション確認ボタンが押されました");

    }


    public void Collection_Exit_Event()
    {
        Debug.Log("コレクション閉じるボタンが押されました");
    }



    //------------------------------------------------------------------------
    //---------------------------メニュー画面のボタン-------------------------
    //------------------------------------------------------------------------

    //メニューボタン
    public void Menu_Open_Event()
    {
        //Debug.Log("メニュー表示ボタンが押されました");
    }

    //ゲーム終了ボタン。
    public void Game_End_Event()
    {
        //Debug.Log("ゲーム終了ボタンが押されました");
        #if UNITY_EDITOR //Unityエディタでテスト時
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE//スタンドアロン実行時
            UnityEngine.Application.Quit(); 
        #elif UNITY_ANDROID || UNITY_IPHONE//アンドロイドかiPhone端末で実行時
            UnityEngine.Application.runInBackground = false;
            UnityEngine.Application.Quit();
        #endif
        UnityEngine.Application.runInBackground = false;
        UnityEngine.Application.Quit();

    }

    //メニュー->コレクションリセットボタン。コレクションの入手状況を初期に戻す
    public void Collection_Reset_Event()
    {
        //Debug.Log("コレクションリセットボタンが押されました");
        game_manager_script.Reset_Collection_Table();
    }

    //メニューから戻るボタン
    public void Menu_Exit_Event()
    {
        //Debug.Log("メニュー閉じるボタンが押されました");

    }


}
