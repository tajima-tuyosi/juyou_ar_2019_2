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
        CaptureScreen();


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

    public void CaptureScreen()
    {
        Camera cam = Camera.GetComponent<Camera>();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);
        RenderTexture prev = cam.targetTexture; //値を戻せるようにprevに保存
        cam.targetTexture = rt; //スクショ用変数を代入
        cam.Render(); // カメラの画像をrtに取得
        cam.targetTexture = prev;
        RenderTexture.active = rt;//rtのみアクティブ状態にする。
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply(); //ReadPixcelを反映

        //canvasとスクリーンの縮尺比を出す。
        var scale_ratio = Screen.width / GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x;
        var camerafinder_senter = new Vector2(Screen.width / 2 + camerafinder_rect.anchoredPosition.x * scale_ratio, Screen.height / 2 + camerafinder_rect.anchoredPosition.y * scale_ratio);
        //左下を軸に、横幅と縦幅を左上に向けて伸ばす
        var rect = new Rect(camerafinder_senter.x - camerafinder_rect.sizeDelta.x / 2 * scale_ratio, camerafinder_senter.y - camerafinder_rect.sizeDelta.y / 2 * scale_ratio, camerafinder_rect.sizeDelta.x * scale_ratio, camerafinder_rect.sizeDelta.y * scale_ratio);
        
        var pivot = new Vector2(0.5f, 0.5f);
        var sprite = Sprite.Create(screenShot, rect, pivot);

        content.GetComponent<Content>().Input_ScreenShot_Image(this.gameObject.name, sprite);
    }
}
