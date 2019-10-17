using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameManagerにアタッチ
//ボタンに関するスクリプト

public class Button : MonoBehaviour
{

    public GameObject reset_popup_UI;
    Game_Manager game_manager_script;



    void Start()
    {
        //canvas = GameObject.Find("Canvas");
        // UI_Display_Name = "UI_Display";
        game_manager_script = GameObject.Find("GameManager").GetComponent<Game_Manager>();
    }

    void Update()
    {
    }


    //コレクションの取得状況を確認するボタン。canvas下にコレクションUIを表示する
    public void Collection_Check_Open_Event()
    {
        Debug.Log("コレクション確認ボタンが押されました");
        //コレクションの所持判定をし直す

    }


    public void Collection_Exit_Event()
    {
        Debug.Log("コレクション閉じるボタンが押されました");
    }



    //------------------------------------------------------------------------
    //-------------------------------メニュー画面-----------------------------
    //------------------------------------------------------------------------

    //メニューボタン
    public void Menu_Open_Event()
    {
        //Debug.Log("メニュー表示ボタンが押されました");
    }

    public void Game_End_Event()
    {
        //Debug.Log("ゲーム終了ボタンが押されました");
#if UNITY_EDITOR //Unityエディタでテスト時
        UnityEditor.EditorApplication.isPlaying = false; //Unity上で終了したい時
#elif UNITY_STANDALONE//スタンドアロン実行時
            UnityEngine.Application.Quit(); 
#elif UNITY_ANDROID || UNITY_IPHONE//アンドロイドかiPhone端末で実行時
            UnityEngine.Application.runInBackground = false;
            UnityEngine.Application.Quit();
#endif
        UnityEngine.Application.runInBackground = false;
        UnityEngine.Application.Quit();

    }

    //メニュー->コレクションリセットボタン
    public void Collection_Reset_Event()
    {
        //コレクションの入手状況を初期に戻す
        //Debug.Log("コレクションリセットボタンが押されました");
        game_manager_script.Reset_Collection_Table();

        //リセットポップアップの表示
        GameObject reset_popup_prefab = (GameObject)Instantiate(reset_popup_UI); //オブジェクトを生成
        reset_popup_prefab.transform.SetParent(GameObject.Find("Menu_Back_Frame").transform, false); // 生成したオブジェクトを子クラスに
        reset_popup_prefab.SetActive(true);

    }

    //メニューから戻るボタン
    public void Menu_Exit_Event()
    {
        //Debug.Log("メニュー閉じるボタンが押されました");

    }


}
