using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//各コレクションの3Dオブジェクトにアタッチ。タッチされた際の処理を記載
public class ItemTouch : MonoBehaviour
{
    private Text text;
    private Game_Manager gm;
    [SerializeField] GameObject gamemanager = default;
    [SerializeField] GameObject getimage = default;
    [SerializeField] GameObject content = default;


    //タッチされたときに呼ぶメソッド
    public void EventTouch()
    {
        text = getimage.transform.GetChild(0).GetComponent<Text>();
        gm = gamemanager.GetComponent<Game_Manager>();
        //Debug.Log(this.gameObject.name + "ENENTTOUCH");
        Get_Collection_Process();
    }


    //コレクションを取得した際の処理
    private void Get_Collection_Process()
    {
        gm.CaptureScreen(this.gameObject);//スクショ保存
        gm.Collection_Table_Set(this.gameObject.name, true);//テーブルに保存
        content.GetComponent<Content>().Review_Collections_Get_Status();//コレクション更新
        text.text = this.gameObject.name + "\nをコレクションに追加しました";
        //ポップアップの表示
        getimage.SetActive(true);
        getimage.GetComponent<Animator>().SetTrigger("Popup");
    }

}
