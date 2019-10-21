using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//各コレクションの3Dオブジェクトにアタッチ

//コレクション一覧で表示されるアイコンなどを保存
//[System.Serializable]//この属性を使ってインスペクター上で表示
public class ItemTouch : MonoBehaviour
{
    private Text text;
    Game_Manager gm;
    public GameObject gamemanager;
    public GameObject getimage;

    //スクリーンショット画像差し込み用
    public GameObject content, Camera;
    private RectTransform camerafinder_rect;


    //public string itemName;
    //public Sprite itemIcon;

    public void eventTouch()
    {
        text = getimage.transform.GetChild(0).GetComponent<Text>();
        gm = gamemanager.GetComponent<Game_Manager>();

        camerafinder_rect = GameObject.Find("CameraFinder").GetComponent<RectTransform>();

        //getobject = GameObject.Find(this.gameObject.name);
        Debug.Log(this.gameObject.name + "ENENTTOUCH");
        Get_Collection_Process();
        gm.CaptureScreen(this.gameObject);

    }

    private void Get_Collection_Process()
    {
        gm.Collection_Table_Set(this.gameObject.name, true);
        content.GetComponent<Content>().Review_Collections_Get_Status();
        text.text = this.gameObject.name + "\nをコレクションに追加しました";
        //ポップアップの表示
        GameObject popup_prefab = (GameObject)Instantiate(getimage); //オブジェクトを生成
        popup_prefab.transform.SetParent(GameObject.Find("Canvas").transform, false); // 生成したオブジェクトをcanvasの子クラスに
        popup_prefab.SetActive(true);
    }

}
